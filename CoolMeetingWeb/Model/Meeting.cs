using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETC.EEG.CoolMeeting.Model
{
    /// <summary>
    /// 会议状态类型定义
    /// </summary>
    public enum MeetingStatus
    {
        /// <summary>
        /// 会议已取消，不可恢复
        /// </summary>
        Canceled = -1,

        /// <summary>
        /// 正常状态
        /// </summary>
        Normal = 1
    }

    /// <summary>
    /// 会议信息
    /// </summary>
    public class Meeting
    {
        private int meetingID;
        private string meetingName;
        private int numberOfParticipants;
        private DateTime startTime;
        private DateTime endTime;
        private DateTime reservationTime;
        private DateTime canceledTime;
        private string description;
        private MeetingStatus status;

        private Employee reservationist;
        private MeetingRoom room;
        private List<Employee> participants;

        /// <summary>
        /// 会议编号
        /// </summary>
        public int MeetingID
        {
            get { return meetingID; }
            set { meetingID = value; }
        }

        /// <summary>
        /// 会议名称
        /// </summary>
        public string MeetingName
        {
            get { return meetingName; }
            set { meetingName = value; }
        }

        /// <summary>
        /// 参会人数
        /// </summary>
        public int NumberofParticipants
        {
            get { return numberOfParticipants; }
            set { numberOfParticipants = value; }
        }

        /// <summary>
        /// 预计开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        /// <summary>
        /// 预计结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        /// <summary>
        /// 会议预定时间
        /// </summary>
        public DateTime ReservationTime
        {
            get { return reservationTime; }
            set { reservationTime = value; }
        }

        /// <summary>
        /// 取消会议时间(如果会议被取消)
        /// </summary>
        public DateTime CanceledTime
        {
            get { return canceledTime; }
            set { canceledTime = value; }
        }

        /// <summary>
        /// 如果会议正常，则表示详细信息；如果会议被取消，则表示取消原因
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// 会议状态
        /// </summary>
        public MeetingStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// 会议的预定者
        /// </summary>
        public Employee Reservationist
        {
            get { return reservationist; }
            set { reservationist = value; }
        }

        /// <summary>
        /// 预定的会议室
        /// </summary>
        public MeetingRoom Room
        {
            get { return room; }
            set { room = value; }
        }

        /// <summary>
        /// 参会人员
        /// </summary>
        public List<Employee> Participants
        {
            get { return participants; }
            set { participants = value; }
        }
    }
}
