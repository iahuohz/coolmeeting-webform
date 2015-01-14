using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ETC.EEG.CoolMeeting.Model;

namespace ETC.EEG.CoolMeeting.DAL
{
    public class DALEmployee
    {
        private const string INSERT_EMPLOYEE = "INSERT INTO Employee(EmployeeName, UserName, Phone, Email, Status, DepartmentID) " +
            "VALUES(@EmployeeName, @UserName, @Phone, @Email, @Status, @DepartmentID)";
        private const string SELECT_EMPLOYEES_BY_STATUS = "SELECT EmployeeID, EmployeeName, UserName, Phone, Email, Status, B.DepartmentID, B.DepartmentName " +
            "FROM Employee AS A INNER JOIN Department AS B ON A.DepartmentID=B.DepartmentID WHERE Status=@Status";
        private const string SELECT_EMPLOYEE_BY_ID = "SELECT EmployeeID, EmployeeName, UserName, Phone, Email, Status, B.DepartmentID, B.DepartmentName " +
            "FROM Employee AS A INNER JOIN Department AS B ON A.DepartmentID=B.DepartmentID WHERE EmployeeID=@EmployeeID";
        private const string UPDATE_EMPLOYEE = "UPDATE Employee SET EmployeeName=@EmployeeName, Phone=@Phone, Email=@Email, " +
            "DepartmentID=@DepartmentID, Status=@Status WHERE EmployeeID=@EmployeeID";
        private const string DELETE_EMPLOYEE = "DELETE FROM Employee WHERE EmployeeID=@EmployeeID";
        private const string SEARCH_EMPLOYEE_PAGED =
                  @"DECLARE @tmp TABLE
                    (
	                    RowID int IDENTITY(1,1),
	                    EmployeeID int,
	                    EmployeeName nvarchar(50),
	                    UserName nvarchar(50),
	                    Phone nvarchar(50),
	                    Email nvarchar(50),
	                    [Status] int,
	                    DepartmentID int,
	                    DepartmentName nvarchar(50)
                    )
                    INSERT INTO @tmp(EmployeeID, EmployeeName, UserName, Phone, Email, [Status], DepartmentID, DepartmentName) 
                    SELECT EmployeeID, EmployeeName, UserName, Phone, Email, Status, B.DepartmentID, B.DepartmentName
                    FROM Employee AS A INNER JOIN Department AS B ON A.DepartmentID=B.DepartmentID WHERE 1=1";
        private const string SEARCH_EMPLOYEE_BY_USERNAME = @"SELECT EmployeeID, EmployeeName, UserName, Phone, Email, Status, B.DepartmentID, B.DepartmentName " +
                    "FROM Employee AS A INNER JOIN Department AS B ON A.DepartmentID=B.DepartmentID WHERE UserName=@UserName";
        private const string SEARCH_EMPLOYEES_BY_DEPARTMENTID = @"SELECT EmployeeID, EmployeeName, UserName, Phone, Email, Status, B.DepartmentID, B.DepartmentName " +
                    "FROM Employee AS A INNER JOIN Department AS B ON A.DepartmentID=B.DepartmentID WHERE A.DepartmentID=@DepartmentID";

        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="employee">包含新员工信息的对象实例</param>
        public static void InsertEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(INSERT_EMPLOYEE, conn);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@UserName", employee.UserName);
                cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Status", (int)employee.Status);
                cmd.Parameters.AddWithValue("@DepartmentID", employee.RelatedDepartment.DepartmentID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 获得所有指定状态的员工信息列表
        /// </summary>
        /// <param name="employeeStatusType">员工状态</param>
        /// <returns>满足条件的员工集合；如果没有满足条件的结果，则集合中元素个数为0</returns>
        public static List<Employee> SelectEmployeesByStatus(EmployeeStatusType employeeStatusType)
        {
            List<Employee> list = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SELECT_EMPLOYEES_BY_STATUS,conn);
                cmd.Parameters.AddWithValue("@Status", (int)employeeStatusType);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                    employee.EmployeeName = dr["EmployeeName"].ToString();
                    employee.UserName = dr["UserName"].ToString();
                    employee.Phone = dr["Phone"].ToString();
                    employee.Email = dr["Email"].ToString();
                    employee.Status = (EmployeeStatusType)Convert.ToInt32(dr["Status"]);
                    Department department = new Department();
                    department.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                    department.DepartmentName = dr["DepartmentName"].ToString();
                    list.Add(employee);
                }
            }
            return list;
        }

        /// <summary>
        /// 返回指定编号的员工的信息
        /// </summary>
        /// <param name="employeeID">待查询的员工的编号</param>
        /// <returns>满足条件的员工；如果没有，则返回null</returns>
        public static Employee SelectEmployeeByID(int employeeID)
        {
            Employee employee = null;
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SELECT_EMPLOYEE_BY_ID, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    employee = new Employee();
                    employee.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                    employee.EmployeeName = dr["EmployeeName"].ToString();
                    employee.UserName = dr["UserName"].ToString();
                    employee.Phone = dr["Phone"].ToString();
                    employee.Email = dr["Email"].ToString();
                    employee.Status = (EmployeeStatusType)Convert.ToInt32(dr["Status"]);
                    // 填充与该员工相关联的部门(即：所属部门)信息
                    Department department = new Department();
                    department.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                    department.DepartmentName = dr["DepartmentName"].ToString();
                    employee.RelatedDepartment = department;
                }
            }
            return employee;
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="employee">带更新的员工对象实例</param>
        public static void UpdateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(UPDATE_EMPLOYEE, conn);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Status", (int)employee.Status);
                cmd.Parameters.AddWithValue("@DepartmentID", employee.RelatedDepartment.DepartmentID);
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="employeeID">待删除的员工编号</param>
        public static void DeleteEmployee(int employeeID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(DELETE_EMPLOYEE, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 根据条件综合搜索，并分页返回员工信息列表
        /// </summary>
        /// <param name="employeeName">员工姓名查询条件。如果为null，则不针对此项筛选</param>
        /// <param name="userName">员工(账号)登录名查询条件，如果为null，则不针对此项筛选</param>
        /// <param name="status">状态查询条件</param>
        /// <param name="pageSize">分页查询中，每页包含的最大行数</param>
        /// <param name="pageIndex">分页查询中，带查询的页数编号。编号从1开始。</param>
        /// <param name="totalResults">输出参数，返回满足条件的总记录数</param>
        /// <returns>满足查询条件，并且位于当前页中的员工集合；如果没有满足条件的结果，则集合中元素个数为0</returns>
        public static List<Employee> SearchEmployeePaged(string employeeName, string userName, EmployeeStatusType status, int pageSize, int pageIndex, out int totalResults)
        {
            string sql = SEARCH_EMPLOYEE_PAGED;
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (employeeName != null)
            {
                sql += " AND EmployeeName LIKE @EmployeeName";
                parameters.Add(new SqlParameter("@EmployeeName", employeeName));
            }
            if (userName != null)
            {
                sql += " AND UserName LIKE @UserName";
                parameters.Add(new SqlParameter("@UserName", userName));
            }
            sql += " AND Status=@Status";
            parameters.Add(new SqlParameter("@Status", (int)status));
            
            sql += " SELECT @TotalResults=@@ROWCOUNT";                  // 获得总行数
            SqlParameter paramTotal = new SqlParameter("@TotalResults", SqlDbType.Int);
            paramTotal.Direction = ParameterDirection.Output;
            parameters.Add(paramTotal);

            // 获取本页内的数据
            sql += " SELECT * FROM @tmp";
            sql += " WHERE RowID>" + pageSize * (pageIndex - 1);
            sql += " AND RowID<=" + pageSize * pageIndex;

            List<Employee> list = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters.ToArray());
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                    employee.EmployeeName = dr["EmployeeName"].ToString();
                    employee.UserName = dr["UserName"].ToString();
                    employee.Phone = dr["Phone"].ToString();
                    employee.Email = dr["Email"].ToString();
                    employee.Status = (EmployeeStatusType)Convert.ToInt32(dr["Status"]);
                    Department department = new Department();
                    department.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                    department.DepartmentName = dr["DepartmentName"].ToString();
                    employee.RelatedDepartment = department;
                    list.Add(employee);
                }
                dr.Close();
                totalResults = Convert.ToInt32(paramTotal.Value);
            }
            return list;
        }
        
        /// <summary>
        /// 根据员工(账号)登录名查询员工信息
        /// </summary>
        /// <param name="userName">员工(账号)登录名</param>
        /// <returns>满足条件的员工；如果没有，则返回null</returns>
        public static Employee SearchEmployeeByUserName(string userName)
        {
            Employee employee = null;
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SEARCH_EMPLOYEE_BY_USERNAME, conn);
                cmd.Parameters.AddWithValue("@UserName", userName);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    employee = new Employee();
                    employee.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                    employee.EmployeeName = dr["EmployeeName"].ToString();
                    employee.UserName = dr["UserName"].ToString();
                    employee.Phone = dr["Phone"].ToString();
                    employee.Email = dr["Email"].ToString();
                    employee.Status = (EmployeeStatusType)Convert.ToInt32(dr["Status"]);
                    Department department = new Department();
                    department.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                    department.DepartmentName = dr["DepartmentName"].ToString();
                    employee.RelatedDepartment = department;
                }
            }
            return employee;
        }

        /// <summary>
        /// 返回指定部门下所有员工信息列表
        /// </summary>
        /// <param name="departmentID">指定的部门编号</param>
        /// <returns>满足条件的员工集合；如果没有满足条件的结果，则集合中元素个数为0</returns>
        public static List<Employee> SearchEmployeesByDepartmentID(int departmentID)
        {
            List<Employee> list = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SEARCH_EMPLOYEES_BY_DEPARTMENTID, conn);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                    employee.EmployeeName = dr["EmployeeName"].ToString();
                    employee.UserName = dr["UserName"].ToString();
                    employee.Phone = dr["Phone"].ToString();
                    employee.Email = dr["Email"].ToString();
                    employee.Status = (EmployeeStatusType)Convert.ToInt32(dr["Status"]);
                    Department department = new Department();
                    department.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                    department.DepartmentName = dr["DepartmentName"].ToString();
                    employee.RelatedDepartment = department;
                    list.Add(employee);
                }
            }
            return list;
        }
    }
}