using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ETC.EEG.CoolMeeting.Model;
using ETC.EEG.CoolMeeting.BLL;

namespace ETC.EEG.CoolMeeting
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDepartments();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;

            Employee employee = new Employee();
            employee.EmployeeName = employeeName;
            employee.UserName = userName;
            employee.Password = password;
            employee.Phone = phone;
            employee.Email = email;
            Department department = new Department();
            department.DepartmentID = Convert.ToInt32(ddlDepartments.SelectedValue);
            department.DepartmentName = ddlDepartments.SelectedItem.Text;
            employee.RelatedDepartment = department;
            employee.Status = EmployeeStatusType.Inactive;

            StaffOpResult result = BLLStaff.Register(employee);
            if (result == StaffOpResult.Duplicate)
            {
                lblResult.Text = "用户名或电子邮件已经存在，请修改!";
            }
            else if (result == StaffOpResult.UserCreateError)
            {
                lblResult.Text = "创建用户账号失败，请联系管理员!";
            }
            else
            {
                txtEmployeeName.Text = "";
                txtUserName.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";
                ddlDepartments.SelectedIndex = 0;
                lblResult.Text = "注册成功！";
            }
        }

        private void BindDepartments()
        {
            ddlDepartments.DataSource =  BLLStaff.GetAllDepartments();
            ddlDepartments.DataTextField = "DepartmentName";
            ddlDepartments.DataValueField = "DepartmentID";
            ddlDepartments.DataBind();
        }
    }
}