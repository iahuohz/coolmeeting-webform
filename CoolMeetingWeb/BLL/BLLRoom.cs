using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETC.EEG.CoolMeeting.Model;
using ETC.EEG.CoolMeeting.DAL;

namespace ETC.EEG.CoolMeeting.BLL
{
    public enum RoomOpResult
    {
        Success = 1,
        Duplicate = -1
    }

    public class BLLRoom
    {
        public static RoomOpResult AddRoom(MeetingRoom room)
        {
            if (DALMeetingRoom.IsRoomExists(room.RoomCode, room.RoomName, room.RoomID))
            {
                return RoomOpResult.Duplicate;
            }
            else
            {
                DALMeetingRoom.InsertRoom(room);
                return RoomOpResult.Success;
            }
        }

        public static List<MeetingRoom> GetAllRooms()
        {
            return DALMeetingRoom.SelectAllRooms();
        }

        public static MeetingRoom GetRoomByID(int roomID)
        {
            return DALMeetingRoom.SelectRoomByID(roomID);
        }

        public static RoomOpResult UpdateRoom(MeetingRoom room)
        {
            if (DALMeetingRoom.IsRoomExists(room.RoomCode, room.RoomName, room.RoomID))
            {
                return RoomOpResult.Duplicate;
            }
            else
            {
                DALMeetingRoom.UpdateRoom(room);
                return RoomOpResult.Success;
            }
        }

        public static List<MeetingRoom> GetActiveRooms()
        {
            return DALMeetingRoom.SelectActiveRooms();
        }
    }
}