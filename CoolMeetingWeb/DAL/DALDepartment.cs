using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ETC.EEG.CoolMeeting.Model;

namespace ETC.EEG.CoolMeeting.DAL
{
    public class DALDepartment
    {
        private const string SELECT_ALL_DEPARTMENTS = "SELECT DepartmentID, DepartmentName FROM Department";
        private const string INSERT_DEPARTMENT = "INSERT INTO Department(DepartmentName) VALUES(@DepartmentName)";
        private const string DELETE_DEPARTMENT = "DELETE FROM Department WHERE DepartmentID=@DepartmentID";
        private const string UPDATE_DEPARTMENT = "UPDATE Department SET DepartmentName=@DepartmentName WHERE DepartmentID=@DepartmentID";
        private const string DEPARTMENTNAME_EXISTS = @"SELECT TOP 1 DepartmentID FROM Department 
            WHERE DepartmentName=@DepartmentName AND DepartmentID<>@DepartmentID";
        private const string EMPLOYEE_EXISTS = "SELECT TOP 1 EmployeeID FROM Employee WHERE DepartmentID=@DepartmentID";

        /// <summary>
        /// 添加新部门
        /// </summary>
        /// <param name="departmentName">新部门名称</param>
        public static void InsertDepartment(string departmentName)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(INSERT_DEPARTMENT, conn);
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 返回所有部门信息列表
        /// </summary>
        /// <returns></returns>
        public static List<Department> SelectAllDepartments()
        {
            List<Department> list = new List<Department>();
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SELECT_ALL_DEPARTMENTS, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Department dept = new Department();
                    dept.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                    dept.DepartmentName = dr["DepartmentName"].ToString();
                    list.Add(dept);
                }
            }
            return list;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentID">待删除的部门编号</param>
        public static void DeleteDepartment(int departmentID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(DELETE_DEPARTMENT, conn);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 更新部门信息
        /// 仅允许更改部门名称
        /// </summary>
        /// <param name="department">待更新的部门对象实例，应包含部门编号和名称信息</param>
        public static void UpdateDepartment(Department department)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(UPDATE_DEPARTMENT, conn);
                cmd.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 判断指定的departmentName是否存在，在查找时忽略指定ID的部门(excludedDepartmentID)
        /// </summary>
        /// <param name="departmentName">待查找的部门名称</param>
        /// <param name="excludedDepartmentID">应排除的DepartmentID</param>
        /// <returns>如果查找到除指定excludedDepartmentID外的同名部门，则返回true；否则返回false</returns>
        public static bool IsDepartmentNameExists(string departmentName, int excludedDepartmentID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(DEPARTMENTNAME_EXISTS, conn);
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                cmd.Parameters.AddWithValue("@DepartmentID", excludedDepartmentID);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                return false;
            }
        }


        /// <summary>
        /// 判断指定部门下是否包含员工
        /// </summary>
        /// <param name="departmentID">待判断的部门编号</param>
        /// <returns>如果指定部门下包含1个以上员工，则返回true；否则返回false</returns>
        public static bool IsEmployeeExists(int departmentID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(EMPLOYEE_EXISTS, conn);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                return false;
            }
        }
    }
}