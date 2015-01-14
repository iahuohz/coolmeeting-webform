using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ETC.EEG.CoolMeeting
{
    public partial class BasePage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.IsAuthenticated)
                {
                    divSigned.Visible = true;
                    divUnSigned.Visible = false;
                    if (HttpContext.Current.User.IsInRole("Admin"))
                    {
                        divAdminMenu.Visible = true;
                        divEmployeeMenu.Visible = false;
                    }
                    else
                    {
                        divAdminMenu.Visible = false;
                        divEmployeeMenu.Visible = true;
                    }
                }
                else
                {
                    divSigned.Visible = false;
                    divUnSigned.Visible = true;
                    divAdminMenu.Visible = false;
                    divEmployeeMenu.Visible = false;
                }
            }
        }
    }
}