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
    public partial class MeetingDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindMeeting();
            }
        }

        private void BindMeeting()
        {
            int meetingID = Convert.ToInt32(Request["meetingid"]);
            Meeting m = BLLMeeting.GetMeetingDetails(meetingID);
            if (m == null)
                return;

            lblMeetingName.Text = m.MeetingName;
            lblRoomName.Text = m.Room.RoomName;
            lblNumberofParticipants.Text = m.NumberofParticipants.ToString();
            lblReservationist.Text = m.Reservationist.EmployeeName;
            lblStartTime.Text = m.StartTime.ToString("yyyy-MM-dd HH:mm");
            lblEndTime.Text = m.EndTime.ToString("yyyy-MM-dd HH:mm");
            lblDescription.Text = m.Description;

            repParticipants.DataSource = m.Participants;
            repParticipants.DataBind();
        }
    }
}