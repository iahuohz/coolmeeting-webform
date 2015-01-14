using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ETC.EEG.CoolMeeting.Model;
using ETC.EEG.CoolMeeting.DAL;

namespace ETC.EEG.CoolMeeting.BLL
{
    /// <summary>
    /// 对人员(包括部门和员工)进行操作的执行结果类型
    /// </summary>
    public enum StaffOpResult
    {
        /// <summary>
        /// 操作成功完成
        /// </summary>
        Success = 1,

        /// <summary>
        /// 存在同名内容从而与当前操作冲突
        /// </summary>
        Duplicate = -1,

        /// <summary>
        /// 存在依赖关系从而与当前操作冲突
        /// </summary>
        DependanceExists = -2,

        /// <summary>
        /// 为员工附加登录账号信息时出现错误
        /// </summary>
        UserCreateError = -3,

        /// <summary>
        /// 修改密码时，原始密码错误
        /// </summary>
        PasswordIncorrect = -4
    }

    /// <summary>
    /// 对人员(包括部门和员工)操作的业务逻辑类
    /// </summary>
    public class BLLStaff
    {
        /// <summary>
        /// 获得所有部门信息列表
        /// </summary>
        /// <returns>部门信息集合</returns>
        public static List<Department> GetAllDepartments()
        {
            return DALDepartment.SelectAllDepartments();
        }

        /// <summary>
        /// 添加新部门
        /// </summary>
        /// <param name="departmentName">部门名称</param>
        /// <returns>操作结果。如果部门名称已经存在，则返回StaffOpResult.Duplicate</returns>
        public static StaffOpResult AddDepartment(string departmentName)
        {
            if (DALDepartment.IsDepartmentNameExists(departmentName, 0))
            {
                return StaffOpResult.Duplicate;
            }
            else
            {
                DALDepartment.InsertDepartment(departmentName);
                return StaffOpResult.Success;
            }
        }

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="department">待更新的部门对象实例</param>
        /// <returns>操作结果。如果部门名称已经存在，则返回StaffOpResult.Duplicate</returns>
        public static StaffOpResult UpdateDepartment(Department department)
        {
            if (DALDepartment.IsDepartmentNameExists(department.DepartmentName, department.DepartmentID))
            {
                return StaffOpResult.Duplicate;
            }
            else
            {
                DALDepartment.UpdateDepartment(department);
                return StaffOpResult.Success;
            }
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="departmentID">待删除的部门编号</param>
        /// <returns>操作结果。如果该部门下仍包含员工，则不可删除并返回StaffOpResult.DependanceExists</returns>
        public static StaffOpResult DeleteDepartment(int departmentID)
        {
            if (DALDepartment.IsEmployeeExists(departmentID))
            {
                return StaffOpResult.DependanceExists;
            }
            else
            {
                DALDepartment.DeleteDepartment(departmentID);
                return StaffOpResult.Success;
            }
        }

        /// <summary>
        /// 注册新员工，并为员工创建关联的登录账号信息。
        /// 员工注册后，还需通过审批才可登录
        /// </summary>
        /// <param name="employee">新员工对象实例</param>
        /// <returns>操作结果。
        /// 如果新员工注册信息中的登录用户名已经存在，则返回StaffOpResult.Duplicate；
        /// 如果创建关联的登录账号失败，则返回StaffOpResult.UserCreateError
        /// </returns>
        public static StaffOpResult Register(Employee employee)
        {
            MembershipCreateStatus status = MembershipCreateStatus.Success;
            Membership.CreateUser(employee.UserName, employee.Password, employee.Email, null, null, false, out status);
            if (status == MembershipCreateStatus.DuplicateUserName || status == MembershipCreateStatus.DuplicateEmail)
            {
                return StaffOpResult.Duplicate;
            }
            else if(status != MembershipCreateStatus.Success)
            {
                return StaffOpResult.UserCreateError;
            }
            DALEmployee.InsertEmployee(employee);
            return StaffOpResult.Success;
        }

        /// <summary>
        /// 返回所有未经审批的员工信息
        /// </summary>
        /// <returns>员工信息列表</returns>
        public static List<Employee> GetUnApprovedEmployees()
        {
            return DALEmployee.SelectEmployeesByStatus(EmployeeStatusType.Inactive);
        }

        /// <summary>
        /// 审批(通过)员工，从而使之可以登录
        /// </summary>
        /// <param name="employeeID">待审批的员工编号</param>
        /// <param name="userName">待审批的员工(账号)登录名</param>
        public static void ApproveEmployee(int employeeID, string userName)
        {
            // 在成员表中审批User
            MembershipUser user = Membership.GetUser(userName);
            user.IsApproved = true;
            Membership.UpdateUser(user);

            // 在员工表中修改Employee状态
            Employee employee = DALEmployee.SelectEmployeeByID(employeeID);
            employee.Status = EmployeeStatusType.Active;
            DALEmployee.UpdateEmployee(employee);
        }

        /// <summary>
        /// 永久删除员工及其登录账号
        /// </summary>
        /// <param name="employeeID">待删除的员工编号</param>
        /// <param name="userName">带删除的员工(账号)登录名</param>
        public static void DeleteEmployee(int employeeID, string userName)
        {
            // 在成员表中删除用户
            Membership.DeleteUser(userName);
            // 在员工表中删除员工
            DALEmployee.DeleteEmployee(employeeID);
        }

        /// <summary>
        /// 永久关闭员工信息并删除及其登录账号
        /// </summary>
        /// <param name="employeeID">员工编号</param>
        /// <param name="userName">员工(账号)登录名</param>
        public static void CloseAccount(int employeeID, string userName)
        {
            // 在成员表中删除用户
            Membership.DeleteUser(userName);

            // 在员工表中修改状态
            Employee employee = DALEmployee.SelectEmployeeByID(employeeID);
            employee.Status = EmployeeStatusType.Closed;
            DALEmployee.UpdateEmployee(employee);
        }

        /// <summary>
        /// 根据条件综合搜索，并分页返回员工信息列表
        /// </summary>
        /// <param name="employeeName">员工姓名查询条件。如果为Empty，则不针对此项筛选</param>
        /// <param name="userName">员工(账号)登录名查询条件，如果为Empty，则不针对此项筛选</param>
        /// <param name="status">状态查询条件</param>
        /// <param name="pageSize">分页查询中，每页包含的最大行数</param>
        /// <param name="pageIndex">分页查询中，带查询的页数编号。编号从1开始。</param>
        /// <param name="totalResults">输出参数，返回满足条件的总记录数</param>
        /// <returns>满足查询条件，并且位于当前页中的员工集合；如果没有满足条件的结果，则集合中元素个数为0</returns>
        public static List<Employee> SearchPagedEmployees(string employeeName, string userName, EmployeeStatusType status, 
            int pageSize, int pageIndex, out int totalResults)
        {
            employeeName = string.IsNullOrWhiteSpace(employeeName) ? null : "%" + employeeName + "%";
            userName = string.IsNullOrWhiteSpace(userName) ? null : "%" + userName + "%";
            return DALEmployee.SearchEmployeePaged(employeeName, userName, status, pageSize, pageIndex, out totalResults);
        }

        /// <summary>
        /// 获取指定员工(账号)登录名的员工
        /// </summary>
        /// <param name="userName">员工(账号)登录名</param>
        /// <returns>员工对象实例；如果没找到，则返回null</returns>
        public static Employee GetEmployeeByUserName(string userName)
        {
            return DALEmployee.SearchEmployeeByUserName(userName);
        }

        /// <summary>
        /// 获取指定部门下所有员工信息列表
        /// </summary>
        /// <param name="departmentID">指定部门编号</param>
        /// <returns>员工信息列表</returns>
        public static List<Employee> GetEmployeesByDepartment(int departmentID)
        {
            return DALEmployee.SearchEmployeesByDepartmentID(departmentID);
        }

        /// <summary>
        /// 获取当前系统登录用户对应的员工信息对象
        /// </summary>
        /// <returns>员工信息对象</returns>
        public static Employee GetEmployeeforLoggedInUser()
        {
            string userName = Membership.GetUser().UserName;
            //string userName = "Jerry";
            Employee employee = BLLStaff.GetEmployeeByUserName(userName);
            return employee;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword">原始密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>如果原始密码不正确，则返回StaffOpResult.PasswordIncorrect</returns>
        public static StaffOpResult ChangePassword(string oldPassword, string newPassword)
        {
            MembershipUser currentUser = Membership.GetUser();
            if (!Membership.ValidateUser(currentUser.UserName, oldPassword))
            {
                return StaffOpResult.PasswordIncorrect;
            }
            currentUser.ChangePassword(oldPassword, newPassword);
            return StaffOpResult.Success;
        }
    }
}