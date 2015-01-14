using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ETC.EEG.CoolMeeting.Model;
using ETC.EEG.CoolMeeting.BLL;

namespace ETC.EEG.CoolMeeting.AdminPages
{
    public partial class ApproveAccounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindEmployees();
            }
        }

        private void BindEmployees()
        {
            List<Employee> list = BLLStaff.GetUnApprovedEmployees();
            repAccounts.DataSource = list;
            repAccounts.DataBind();
        }

        protected void repAccounts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int employeeID = Convert.ToInt32(e.CommandArgument);
            Label lblUserName = e.Item.FindControl("lblUserName") as Label;
            string userName = lblUserName.Text;

            if (e.CommandName == "Approve")
            {
                BLLStaff.ApproveEmployee(employeeID, userName);
            }
            else if (e.CommandName == "Delete")
            {
                BLLStaff.DeleteEmployee(employeeID, userName);
            }
            BindEmployees();
        }
    }
}