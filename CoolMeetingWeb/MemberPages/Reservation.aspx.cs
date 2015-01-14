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
    public partial class Reservation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime now = DateTime.Now;

                // 设置日期和时间的初始值
                txtStartDate.Text = now.ToString("yyyy-MM-dd");
                txtEndDate.Text = txtStartDate.Text;
                txtStartTime.Text = now.AddHours(1).ToString("HH:mm");
                txtEndTime.Text = now.AddHours(2).ToString("HH:mm");

                // 设置日期、时间范围验证控件的值域
                valStartDate2.MinimumValue = now.ToString("yyyy-MM-dd");
                valStartDate2.MaximumValue = now.AddYears(1).ToString("yyyy-MM-dd");
                valEndDate2.MinimumValue = now.ToString("yyyy-MM-dd");
                valEndDate2.MaximumValue = now.AddYears(1).ToString("yyyy-MM-dd");

                BindRooms();
                BindDepartments();

                ddlDepartments_SelectedIndexChanged(null, EventArgs.Empty);
            }
        }

        private void BindRooms()
        {
            List<MeetingRoom> rooms = BLLRoom.GetActiveRooms();
            ddlRooms.DataSource = rooms;
            ddlRooms.DataValueField = "RoomID";
            ddlRooms.DataTextField = "RoomName";
            ddlRooms.DataBind();
        }

        private void BindDepartments()
        {
            List<Department> departments = BLLStaff.GetAllDepartments();
            ddlDepartments.DataSource = departments;
            ddlDepartments.DataTextField = "DepartmentName";
            ddlDepartments.DataValueField = "DepartmentID";
            ddlDepartments.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string meetingName = txtMeetingName.Text;
            int numberofParticipants = Convert.ToInt32(txtNumberofParticipants.Text);
            DateTime start = DateTime.Parse(txtStartDate.Text + " " + txtStartTime.Text);
            DateTime end = DateTime.Parse(txtEndDate.Text + " " + txtEndTime.Text);
            int roomID = Convert.ToInt32(ddlRooms.SelectedValue);
            string description = txtDescription.Text;

            string script;
            if (start >= end)           // 检查结束时间是否大于起始时间
            {
                script = "<script type='text/javascript'>alert('会议结束时间必须大于起始时间');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "dateerror", script);
                return;
            }

            Meeting meeting = new Meeting();
            meeting.MeetingName = meetingName;
            meeting.NumberofParticipants = numberofParticipants;
            meeting.StartTime = start;
            meeting.EndTime = end;
            meeting.Description = description;
            meeting.Room = BLLRoom.GetRoomByID(roomID);
            meeting.Reservationist = BLLStaff.GetEmployeeforLoggedInUser();
            meeting.Status = MeetingStatus.Normal;

            meeting.Participants = new List<Employee>();
            foreach (ListItem item in lbSelectedEmployees.Items)
            {
                int employeeID = Convert.ToInt32(item.Value);
                meeting.Participants.Add(new Employee { EmployeeID = employeeID });
            }

            MeetingOpResults result = BLLMeeting.ReserveMeeting(meeting);
            script = string.Format("<script>alert('会议预订成功！');window.location.href='MyReservations.aspx';</script>");
            switch (result)
            {
                case MeetingOpResults.NotEnoughCapacity:
                    script = string.Format("<script>alert('所选会议室容量为{0},无法容纳{1}人');</script>",
                        meeting.Room.Capacity, meeting.NumberofParticipants);
                    break;
                case MeetingOpResults.ReservationTooLate:
                    script = string.Format("<script>alert('距会议开始时间{0}已不足30分钟，请推迟时间');</script>",
                        meeting.StartTime.ToString("HH:mm"));
                    break;
                case MeetingOpResults.RoomScheduleNotAvailable:
                    script = string.Format("<script>alert('该会议室已被预订，请更改时间或重新选择会议室');</script>");
                    break;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "result", script);
        }

        protected void ddlDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            int departmentID = Convert.ToInt32(ddlDepartments.SelectedValue);
            List<Employee> list = BLLStaff.GetEmployeesByDepartment(departmentID);
            lbEmployees.DataSource = list;
            lbEmployees.DataTextField = "EmployeeName";
            lbEmployees.DataValueField = "EmployeeID";
            lbEmployees.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in lbEmployees.Items)
            {
                if (item.Selected)
                {
                    bool bFound = false;
                    foreach(ListItem selectedItem in lbSelectedEmployees.Items)
                    {
                        if (item.Value == selectedItem.Value)
                        {
                            bFound = true;
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        lbSelectedEmployees.Items.Add(new ListItem(item.Text, item.Value));
                    }
                }
            }
            lbEmployees.ClearSelection();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            int[] ids = lbSelectedEmployees.GetSelectedIndices();
            List<ListItem> list = new List<ListItem>();
            foreach (int id in ids)
            {
                list.Add(lbSelectedEmployees.Items[id]);
            }
            foreach(ListItem item in list)
            {
                lbSelectedEmployees.Items.Remove(item);
            }
        }
    }
}