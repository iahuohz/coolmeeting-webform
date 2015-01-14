using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETC.EEG.CoolMeeting.BLL;
using ETC.EEG.CoolMeeting.Model;

namespace ETC.EEG.CoolMeeting.MemberPages
{
    public partial class CancelMeeting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblMeetingName.Text = Request["meetingname"];
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int meetingID = Convert.ToInt32(Request["meetingid"]);
            string reason = txtDescription.Text;
            MeetingOpResults result = BLLMeeting.CancelMeeting(meetingID, reason);
            string script = "";
            if (result == MeetingOpResults.MeetingCanNotCancel)
            {
                script = "<script>alert('会议已经结束或正在进行中，不能撤销!');</script>";
            }
            else
            {
                script = "<script>alert('会议撤销成功！');window.location.href='MyReservations.aspx';</script>";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "result", script);
        }
    }
}