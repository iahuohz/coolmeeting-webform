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
    public partial class AdminRooms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRooms();
            }
        }

        private void BindRooms()
        {
            List<MeetingRoom> list = BLLRoom.GetAllRooms();
            repRooms.DataSource = list;
            repRooms.DataBind();
        }
    }
}