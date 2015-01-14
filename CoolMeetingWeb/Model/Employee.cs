using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETC.EEG.CoolMeeting.Model
{
    /// <summary>
    /// 员工(账号)状态定义
    /// </summary>
    public enum EmployeeStatusType
    {
        /// <summary>
        /// 该账号已经关闭，永久不再使用
        /// </summary>
        Closed = -1,

        /// <summary>
        /// 该账号刚刚注册，但还未经过管理员审核通过
        /// </summary>
        Inactive = 0,

        /// <summary>
        /// 该账号已经通过管理员审核
        /// </summary>
        Active = 1
    }

    /// <summary>
    /// 员工信息类
    /// </summary>
    public class Employee
    {
        private int employeeID;
        private string userName;
        private string password;
        private string employeeName;
        private string phone;
        private string email;
        private EmployeeStatusType status;
        private Department relatedDepartment;

        /// <summary>
        /// 员工编号
        /// </summary>
        public int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }

        /// <summary>
        /// 员工对应的登录账号名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        /// <summary>
        /// 员工(账号)状态
        /// </summary>
        public EmployeeStatusType Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// 员工所属部门
        /// </summary>
        public Department RelatedDepartment
        {
            get { return relatedDepartment; }
            set { relatedDepartment = value; }
        }
    }
}
