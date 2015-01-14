using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETC.EEG.CoolMeeting.BLL;

namespace ETC.EEG.CoolMeeting
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOrigin.Text;
            string newPassword = txtNew.Text;
            StaffOpResult result = BLLStaff.ChangePassword(oldPassword, newPassword);
            string script = "";
            if (result == StaffOpResult.PasswordIncorrect)
            {
                script = "<script type='text/javascript'>alert('原始密码错误！');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "error", script);
            }
            else
            {
                script = "<script type='text/javascript'>alert('密码修改成功！');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "success", script);
            }
        }
    }
}