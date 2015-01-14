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
    public partial class AddRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string roomCode = txtRoomCode.Text;
            string roomName = txtRoomName.Text;
            int capacity = Convert.ToInt32(txtCapacity.Text);
            MeetingRoomStatus status = (MeetingRoomStatus)Convert.ToInt32(rblStatus.SelectedValue);
            string description = txtDescription.Text;

            MeetingRoom room = new MeetingRoom();
            room.RoomCode = roomCode;
            room.RoomName = roomName;
            room.Capacity = capacity;
            room.Status = status;
            room.Description = description;

            RoomOpResult result = BLLRoom.AddRoom(room);
            string script;
            if (result == RoomOpResult.Duplicate)
            {
                script = "<script type='text/javascript'>alert('会议室编号或名称已存在，请更换编号或名称!');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "duplicate", script);
            }
            else if (result == RoomOpResult.Success)
            {
                txtRoomCode.Text = "";
                txtRoomName.Text = "";
                txtCapacity.Text = "";
                txtDescription.Text = "";
                rblStatus.SelectedIndex = 0;
                script = "<script type='text/javascript'>alert('会议室:" + room.RoomName  + "添加成功!');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "success", script);
            }
        }
    }
}