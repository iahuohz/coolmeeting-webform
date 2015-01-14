using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ETC.EEG.CoolMeeting.Model;

namespace ETC.EEG.CoolMeeting.DAL
{
    public class DALMeetingRoom
    {
        private const string INSERT_ROOM = @"INSERT INTO MeetingRoom(RoomCode, RoomName, Capacity, Status, Description) 
                                            VALUES(@RoomCode, @RoomName, @Capacity, @Status, @Description)";
        private const string SELECT_ALL_ROOMS = "SELECT RoomID, RoomCode, RoomName, Capacity, Status, Description FROM MeetingRoom";
        private const string SELECT_ROOM_BY_CODE_NAME = "SELECT TOP 1 RoomID FROM MeetingRoom WHERE (RoomCode=@RoomCode OR RoomName=@RoomName) AND RoomID<>@RoomID";
        private const string SELECT_ROOM_BY_ID = "SELECT RoomID, RoomCode, RoomName, Capacity, Status, Description FROM MeetingRoom WHERE RoomID=@RoomID";
        private const string UPDATE_ROOM = @"UPDATE MeetingRoom SET RoomCode=@RoomCode,RoomName=@RoomName,Capacity=@Capacity,
                                            [Status]=@Status,Description=@Description WHERE RoomID=@RoomID";
        private const string SELECT_ACTIVE_ROOMS = "SELECT RoomID, RoomCode, RoomName, Capacity, Status, Description FROM MeetingRoom WHERE Status=1";

        public static void InsertRoom(MeetingRoom room)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(INSERT_ROOM, conn);
                cmd.Parameters.AddWithValue("@RoomCode", room.RoomCode);
                cmd.Parameters.AddWithValue("@RoomName", room.RoomName);
                cmd.Parameters.AddWithValue("@Capacity", room.Capacity);
                cmd.Parameters.AddWithValue("@Status", (int)room.Status);
                cmd.Parameters.AddWithValue("@Description", room.Description);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<MeetingRoom> SelectAllRooms()
        {
            List<MeetingRoom> list = new List<MeetingRoom>();
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SELECT_ALL_ROOMS, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    MeetingRoom room = new MeetingRoom();
                    room.RoomID = Convert.ToInt32(dr["RoomID"]);
                    room.RoomCode = dr["RoomCode"].ToString();
                    room.RoomName = dr["RoomName"].ToString();
                    room.Capacity = Convert.ToInt32(dr["Capacity"]);
                    room.Status = (MeetingRoomStatus)Convert.ToInt32(dr["Status"]);
                    room.Description = dr["Description"].ToString();
                    list.Add(room);
                }
            }
            return list;
        }

        public static bool IsRoomExists(string roomCode, string roomName, int excludedRoomID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SELECT_ROOM_BY_CODE_NAME, conn);
                cmd.Parameters.AddWithValue("@RoomCode", roomCode);
                cmd.Parameters.AddWithValue("@RoomName", roomName);
                cmd.Parameters.AddWithValue("@RoomID", excludedRoomID);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    return true;
                }
                return false;
            }
        }

        public static MeetingRoom SelectRoomByID(int roomID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SELECT_ROOM_BY_ID, conn);
                cmd.Parameters.AddWithValue("@RoomID", roomID);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MeetingRoom room = new MeetingRoom();
                    room.RoomID = Convert.ToInt32(dr["RoomID"]);
                    room.RoomCode = dr["RoomCode"].ToString();
                    room.RoomName = dr["RoomName"].ToString();
                    room.Capacity = Convert.ToInt32(dr["Capacity"]);
                    room.Status = (MeetingRoomStatus)Convert.ToInt32(dr["Status"]);
                    room.Description = dr["Description"].ToString();

                    return room;
                }
                return null;
            }
        }

        public static void UpdateRoom(MeetingRoom room)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(UPDATE_ROOM, conn);
                cmd.Parameters.AddWithValue("@RoomCode", room.RoomCode);
                cmd.Parameters.AddWithValue("@RoomName", room.RoomName);
                cmd.Parameters.AddWithValue("@Capacity", room.Capacity);
                cmd.Parameters.AddWithValue("@Status", (int)room.Status);
                cmd.Parameters.AddWithValue("@Description", room.Description);
                cmd.Parameters.AddWithValue("@RoomID", room.RoomID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal static List<MeetingRoom> SelectActiveRooms()
        {
            List<MeetingRoom> list = new List<MeetingRoom>();
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SELECT_ACTIVE_ROOMS, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MeetingRoom room = new MeetingRoom();
                    room.RoomID = Convert.ToInt32(dr["RoomID"]);
                    room.RoomCode = dr["RoomCode"].ToString();
                    room.RoomName = dr["RoomName"].ToString();
                    room.Capacity = Convert.ToInt32(dr["Capacity"]);
                    room.Status = (MeetingRoomStatus)Convert.ToInt32(dr["Status"]);
                    room.Description = dr["Description"].ToString();
                    list.Add(room);
                }
            }
            return list;
        }
    }
}