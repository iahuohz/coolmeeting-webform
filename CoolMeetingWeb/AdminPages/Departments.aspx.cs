using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETC.EEG.CoolMeeting.Model;
using ETC.EEG.CoolMeeting.BLL;

namespace ETC.EEG.CoolMeeting.AdminPages
{
    public partial class Departments : System.Web.UI.Page
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
            string departmentName = txtDepartmentName.Text;
            StaffOpResult result = BLLStaff.AddDepartment(departmentName);
            string script;
            if (result == StaffOpResult.Duplicate)
            {
                script = "<script type='text/javascript'>alert('部门：" + departmentName + " 已经存在！');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "duplicate", script);
            }
            else
            {
                BindDepartments();                  // 重新绑定以显示新增部门数据
                txtDepartmentName.Text = "";
                script = "<script type='text/javascript'>alert('部门：" + departmentName + " 添加成功！');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "success", script);
            }
        }

        protected void gvDepartments_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDepartments.EditIndex = e.NewEditIndex;
            BindDepartments();
        }

        protected void gvDepartments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDepartments.EditIndex = -1;           // 退出编辑状态
            BindDepartments();
        }

        protected void gvDepartments_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int deptID = Convert.ToInt32(e.Keys[0]);            // 获取被编辑的行所对应Department的编号。需在GridView中预先设置DataKeyNames
            GridViewRow gvr = gvDepartments.Rows[e.RowIndex];   // 获取当前被编辑的行
            TextBox txtDepartmentName = gvr.FindControl("txtDepartmentName") as TextBox;    // 找到部门名称编辑框
            string newName = txtDepartmentName.Text;

            Department department = new Department();
            department.DepartmentID = deptID;
            department.DepartmentName = newName;

            StaffOpResult result = BLLStaff.UpdateDepartment(department);
            string script;
            if (result == StaffOpResult.Duplicate)
            {
                script = "<script type='text/javascript'>alert('部门：" + newName + " 已经存在！');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "duplicate", script);
                e.Cancel = true;                        // 不再执行后续操作，回到编辑状态
            }
            else
            {
                script = "<script type='text/javascript'>alert('部门修改成功！');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "success", script);
                
                gvDepartments.EditIndex = -1;           // 退出编辑状态
                BindDepartments();
            }
        }

        protected void gvDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int deptID = Convert.ToInt32(e.Keys[0]);
            StaffOpResult result = BLLStaff.DeleteDepartment(deptID);
            string script;
            if (result == StaffOpResult.DependanceExists)
            {
                script = "<script type='text/javascript'>alert('该部门下仍有员工，不能删除！');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "duplicate", script);
                e.Cancel = true;  
            }
            else
            {
                script = "<script type='text/javascript'>alert('部门删除成功！');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "success", script);
                BindDepartments();
            }
        }

        private void BindDepartments()
        {
            List<Department> list = BLLStaff.GetAllDepartments();
            gvDepartments.DataSource = list;
            gvDepartments.DataBind();
        }
    }
}