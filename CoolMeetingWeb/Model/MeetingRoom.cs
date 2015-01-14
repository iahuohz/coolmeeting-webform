using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETC.EEG.CoolMeeting.Model
{
    /// <summary>
    /// 会议室状态类型
    /// </summary>
    public enum MeetingRoomStatus
    {
        /// <summary>
        /// 会议室已删除，永久不再使用
        /// </summary>
        Deleted = -1,

        /// <summary>
        /// 会议室维护中，暂时不可使用
        /// </summary>
        Inactive = 0,

        /// <summary>
        /// 会议室状态正常，可供预订
        /// </summary>
        Active = 1
    }

    /// <summary>
    /// 会议室状态的文字描述定义，用于将状态的类型映射为对应的文字描述
    /// </summary>
    public class MeetingRoomStatusDescription
    {
        private static Dictionary<int, string> status = new Dictionary<int, string>();
        static MeetingRoomStatusDescription()
        {
            status.Add((int)MeetingRoomStatus.Deleted, "已删除");
            status.Add((int)MeetingRoomStatus.Inactive, "已禁用");
            status.Add((int)MeetingRoomStatus.Active, "已启用");
        }
        public static Dictionary<int, string> Status
        {
            get { return status; }
        }
    }

    /// <summary>
    /// 会议室信息
    /// </summary>
    public class MeetingRoom
    {
        private int roomID;
        private string roomCode;
        private string roomName;
        private int capacity;
        private MeetingRoomStatus status;
        private string description;

        /// <summary>
        /// 会议室编号(流水号)
        /// </summary>
        public int RoomID
        {
            get { return roomID; }
            set { roomID = value; }
        }

        /// <summary>
        /// 会议室房间号
        /// </summary>
        public string RoomCode
        {
            get { return roomCode; }
            set { roomCode = value; }
        }

        /// <summary>
        /// 会议室名称
        /// </summary>
        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public MeetingRoomStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// 描述或其它信息
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
