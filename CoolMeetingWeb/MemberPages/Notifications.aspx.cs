using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETC.EEG.CoolMeeting.Model;
using ETC.EEG.CoolMeeting.BLL;

namespace ETC.EEG.CoolMeeting.MemberPages
{
    public partial class Notifications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                repNewMeetings.DataSource =  BLLMeeting.GetRecentMeetings();
                repNewMeetings.DataBind();

                repCanceledMeetins.DataSource = BLLMeeting.GetCanceledMeetings();
                repCanceledMeetins.DataBind();
            }
        }
    }
}