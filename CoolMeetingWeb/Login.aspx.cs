using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ETC.EEG.CoolMeeting
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            if (Membership.ValidateUser(userName, password))
            {
                FormsAuthentication.RedirectFromLoginPage(userName, false);
            }
            else
            {
                lblLoginResult.Visible = true;
            }
        }
    }
}