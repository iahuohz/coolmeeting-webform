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
    public partial class EditRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRoom();
            }
        }

        private void BindRoom()
        {
            int roomID = Convert.ToInt32(Request["roomid"]);
            MeetingRoom room = BLLRoom.GetRoomByID(roomID);
            if (room != null)
            {
                if (room.Status == MeetingRoomStatus.Deleted)       // 如果会议室已被删除，则不允许进行任何修改
                {
                    txtRoomCode.Enabled = false;
                    txtRoomName.Enabled = false;
                    txtCapacity.Enabled = false;
                    rblStatus.Enabled = false;
                    txtRoomCode.Enabled = false;
                }

                txtRoomCode.Text = room.RoomCode;
                txtRoomName.Text = room.RoomName;
                txtCapacity.Text = room.Capacity.ToString();
                rblStatus.SelectedValue = Convert.ToInt32(room.Status).ToString();
                txtDescription.Text = room.Description;
            }
            else
            {
                btnSubmit.Enabled = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int roomID = Convert.ToInt32(Request["roomid"]);
            string roomCode = txtRoomCode.Text;
            string roomName = txtRoomName.Text;
            int capacity = Convert.ToInt32(txtCapacity.Text);
            MeetingRoomStatus status = (MeetingRoomStatus)Convert.ToInt32(rblStatus.SelectedValue);
            string description = txtDescription.Text;

            MeetingRoom room = new MeetingRoom();
            room.RoomID = roomID;
            room.RoomCode = roomCode;
            room.RoomName = roomName;
            room.Capacity = capacity;
            room.Status = status;
            room.Description = description;

            RoomOpResult result = BLLRoom.UpdateRoom(room);
            string script;
            if (result == RoomOpResult.Duplicate)
            {
                script = "<script type='text/javascript'>alert('会议室编号或名称已存在，请更换编号或名称!');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "duplicate", script);
            }
            else if (result == RoomOpResult.Success)
            {
                script = "<script type='text/javascript'>alert('会议室:" + room.RoomName + "修改成功!');window.location.href='AdminRooms.aspx';</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "success", script);
            }
        }
    }
}