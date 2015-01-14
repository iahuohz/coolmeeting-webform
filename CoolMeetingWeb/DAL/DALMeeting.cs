using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ETC.EEG.CoolMeeting.Model;

namespace ETC.EEG.CoolMeeting.DAL
{
    public class DALMeeting
    {
        private const string IS_ROOM_OCCUPIED_DURING = @"SELECT TOP 1 RoomID FROM Meeting WHERE RoomID=@RoomID AND 
                    ((StartTime BETWEEN @StartTime AND @EndTime) OR (EndTime BETWEEN @StartTime AND @EndTime))";
        private const string INSERT_MEETING = @"INSERT INTO Meeting(MeetingName, RoomID, ReservationistID, NumberofParticipants, 
                    StartTime, EndTime, ReservationTime, Description, Status) VALUES(@MeetingName, @RoomID, @ReservationistID,
                    @NumberofParticipants, @StartTime, @EndTime, @ReservationTime, @Description, @Status);
                    SELECT @MeetingID=@@IDENTITY";
        private const string INSERT_MEETING_PARTICIPANT = @"INSERT INTO MeetingParticipants(MeetingID, EmployeeID) VALUES(@MeetingID, @EmployeeID)";
        private const string SELECT_MEETINGS_BY_RESERVATIONISTID = @"SELECT A.MeetingID,A.MeetingName, A.RoomID, B.RoomName, A.ReservationistID, 
                    C.EmployeeName, A.NumberofParticipants, A.StartTime, A.EndTime, A.ReservationTime, A.Description, A.Status 
                    FROM Meeting AS A INNER JOIN MeetingRoom AS B ON A.RoomID=B.RoomID
                    INNER JOIN Employee AS C ON A.ReservationistID=C.EmployeeID
                    WHERE A.ReservationistID=@ReservationistID AND A.Status=1 AND A.StartTime>@Current
                    ORDER BY ReservationTime DESC";
        private const string SELECT_MEETINGS_FOR_EMPLOYEEID = @"SELECT A.MeetingID,A.MeetingName, A.RoomID, B.RoomName, A.ReservationistID, 
                    A.NumberofParticipants, A.StartTime, A.EndTime, A.ReservationTime, A.Description, A.Status 
                    FROM Meeting AS A INNER JOIN MeetingRoom AS B ON A.RoomID=B.RoomID
                    INNER JOIN MeetingParticipants AS C ON A.MeetingID=C.MeetingID
                    INNER JOIN Employee AS D ON D.EmployeeID=C.EmployeeID
                    WHERE D.EmployeeID=@EmployeeID AND A.Status=1 AND A.StartTime>@Current
                    ORDER BY A.StartTime";
        private const string SELECT_MEETING_BY_ID = @"SELECT A.MeetingID,A.MeetingName, A.RoomID, B.RoomName, A.ReservationistID, 
                    C.EmployeeName, A.NumberofParticipants, A.StartTime, A.EndTime, A.ReservationTime, A.Description, A.Status 
                    FROM Meeting AS A INNER JOIN MeetingRoom AS B ON A.RoomID=B.RoomID
                    INNER JOIN Employee AS C ON A.ReservationistID=C.EmployeeID
                    WHERE MeetingID=@MeetingID";
        private const string SELECT_EMPLOYEES_FOR_MEETING = @"SELECT A.EmployeeID, A.EmployeeName, A.UserName, A.Email, A.Phone 
                    FROM Employee AS A INNER JOIN MeetingParticipants AS B ON A.EmployeeID=B.EmployeeID
                    WHERE B.MeetingID=@MeetingID";
        private const string UPDATE_MEETING_STATUS = @"UPDATE Meeting SET Status=@Status, Description=@Description WHERE MeetingID=@MeetingID";

        private const string SEARCH_MEETING_PAGED = @"DECLARE @tmp TABLE
                    (
	                    RowID int IDENTITY(1,1),
	                    MeetingID int,
	                    MeetingName nvarchar(50),
	                    RoomName nvarchar(50),
	                    ReservationistName nvarchar(50),
	                    StartTime datetime,
	                    EndTime datetime,
	                    ReservationTime datetime
                    )
                    INSERT INTO @tmp(MeetingID, MeetingName, RoomName, ReservationistName, StartTime, EndTime, ReservationTime)
                    SELECT A.MeetingID, A.MeetingName, B.RoomName, C.EmployeeName, A.StartTime, A.EndTime, A.ReservationTime
                    FROM Meeting AS A INNER JOIN MeetingRoom AS B ON A.RoomID=B.RoomID
                    INNER JOIN Employee AS C ON A.ReservationistID=C.EmployeeID WHERE 1=1";

        private const string SEARCH_RECENT_MEETING_BY_PARTICIPANT = @"SELECT A.MeetingID, A.MeetingName, B.RoomName, A.StartTime, A.EndTime
                    FROM Meeting AS A INNER JOIN MeetingRoom AS B ON A.RoomID=B.RoomID
                    INNER JOIN MeetingParticipants AS C ON A.MeetingID=C.MeetingID
                    WHERE A.Status<>-1 AND C.EmployeeID=@EmployeeID AND A.StartTime>=@StartTime";
        private const string SEARCH_CANCELED_MEETING = @"SELECT A.MeetingID, A.MeetingName, B.RoomName, A.StartTime, A.EndTime, A.Description
                    FROM Meeting AS A INNER JOIN MeetingRoom AS B ON A.RoomID=B.RoomID
                    INNER JOIN MeetingParticipants AS C ON A.MeetingID=C.MeetingID
                    WHERE A.Status=-1 AND C.EmployeeID=@EmployeeID AND A.StartTime>=@StartTime";

        public static bool IsRoomOccupiedDuringTimeSpan(Meeting meeting)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(IS_ROOM_OCCUPIED_DURING, conn);
                cmd.Parameters.AddWithValue("@RoomID", meeting.Room.RoomID);
                cmd.Parameters.AddWithValue("@StartTime", meeting.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", meeting.EndTime);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                return false;
            }
        }

        public static int InsertMeeting(Meeting meeting)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(INSERT_MEETING, conn);
                cmd.Parameters.AddWithValue("@MeetingName", meeting.MeetingName);
                cmd.Parameters.AddWithValue("@RoomID", meeting.Room.RoomID);
                cmd.Parameters.AddWithValue("@ReservationistID", meeting.Reservationist.EmployeeID);
                cmd.Parameters.AddWithValue("@NumberofParticipants", meeting.NumberofParticipants);
                cmd.Parameters.AddWithValue("@StartTime", meeting.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", meeting.EndTime);
                cmd.Parameters.AddWithValue("@ReservationTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@Description", meeting.Description);
                cmd.Parameters.AddWithValue("@Status", meeting.Status);
                
                SqlParameter param = new SqlParameter("@MeetingID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                conn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToInt32(param.Value);
            }
        }

        public static void InsertMeetingParticipant(int meetingID, int employeeID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(INSERT_MEETING_PARTICIPANT, conn);
                cmd.Parameters.AddWithValue("@MeetingID", meetingID);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Meeting> GetReservationsByReservationistID(int reservationistID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SELECT_MEETINGS_BY_RESERVATIONISTID, conn);
                cmd.Parameters.AddWithValue("@ReservationistID", reservationistID);
                cmd.Parameters.AddWithValue("@Current", DateTime.Now);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<Meeting> list = new List<Meeting>();
                while (dr.Read())
                {
                    Meeting m = new Meeting();
                    m.MeetingID = Convert.ToInt32(dr["MeetingID"]);
                    m.MeetingName = dr["MeetingName"].ToString();
                    m.NumberofParticipants = Convert.ToInt32(dr["NumberofParticipants"]);
                    m.StartTime = Convert.ToDateTime(dr["StartTime"]);
                    m.EndTime = Convert.ToDateTime(dr["EndTime"]);
                    m.ReservationTime = Convert.ToDateTime(dr["ReservationTime"]);
                    m.Description = dr["Description"].ToString();
                    m.Status = Convert.ToInt32(dr["Status"]) == 1 ? MeetingStatus.Normal : MeetingStatus.Canceled;
                    m.Room = new MeetingRoom();
                    m.Room.RoomID = Convert.ToInt32(dr["RoomID"]);
                    m.Room.RoomName = dr["RoomName"].ToString();
                    m.Reservationist = new Employee();
                    m.Reservationist.EmployeeID = reservationistID;
                    m.Reservationist.EmployeeName = dr["EmployeeName"].ToString();
                    list.Add(m);
                }
                return list;
            }
        }

        public static List<Meeting> GetMeetingsForEmployee(int employeeID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SELECT_MEETINGS_FOR_EMPLOYEEID, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@Current", DateTime.Now);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<Meeting> list = new List<Meeting>();
                while (dr.Read())
                {
                    Meeting m = new Meeting();
                    m.MeetingID = Convert.ToInt32(dr["MeetingID"]);
                    m.MeetingName = dr["MeetingName"].ToString();
                    m.NumberofParticipants = Convert.ToInt32(dr["NumberofParticipants"]);
                    m.StartTime = Convert.ToDateTime(dr["StartTime"]);
                    m.EndTime = Convert.ToDateTime(dr["EndTime"]);
                    m.ReservationTime = Convert.ToDateTime(dr["ReservationTime"]);
                    m.Description = dr["Description"].ToString();
                    m.Status = Convert.ToInt32(dr["Status"]) == 1 ? MeetingStatus.Normal : MeetingStatus.Canceled;
                    m.Room = new MeetingRoom();
                    m.Room.RoomID = Convert.ToInt32(dr["RoomID"]);
                    m.Room.RoomName = dr["RoomName"].ToString();
                    list.Add(m);
                }
                return list;
            }
        }

        public static Meeting GetMeetingByID(int meetingID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SELECT_MEETING_BY_ID, conn);
                cmd.Parameters.AddWithValue("@MeetingID", meetingID);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Meeting m = null;
                while (dr.Read())
                {
                    m = new Meeting();
                    m.MeetingID = Convert.ToInt32(dr["MeetingID"]);
                    m.MeetingName = dr["MeetingName"].ToString();
                    m.NumberofParticipants = Convert.ToInt32(dr["NumberofParticipants"]);
                    m.StartTime = Convert.ToDateTime(dr["StartTime"]);
                    m.EndTime = Convert.ToDateTime(dr["EndTime"]);
                    m.ReservationTime = Convert.ToDateTime(dr["ReservationTime"]);
                    m.Description = dr["Description"].ToString();
                    m.Status = Convert.ToInt32(dr["Status"]) == 1 ? MeetingStatus.Normal : MeetingStatus.Canceled;
                    m.Room = new MeetingRoom();
                    m.Room.RoomID = Convert.ToInt32(dr["RoomID"]);
                    m.Room.RoomName = dr["RoomName"].ToString();
                    m.Reservationist = new Employee();
                    m.Reservationist.EmployeeID = Convert.ToInt32(dr["ReservationistID"]);
                    m.Reservationist.EmployeeName = dr["EmployeeName"].ToString();
                }
                return m;
            }    
        }

        public static List<Employee> GetParticipantsByMeetingID(int meetingID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SELECT_EMPLOYEES_FOR_MEETING, conn);
                cmd.Parameters.AddWithValue("MeetingID", meetingID);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<Employee> list = new List<Employee>();
                while(dr.Read())
                {
                    Employee e = new Employee();
                    e.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                    e.EmployeeName = dr["EmployeeName"].ToString();
                    e.UserName = dr["UserName"].ToString();
                    e.Email = dr["Email"].ToString();
                    e.Phone = dr["Phone"].ToString();
                    list.Add(e);
                }
                return list;
            }
        }

        public static void UpdateMeetingStatus(MeetingStatus meetingStatus, string reason, int meetingID)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(UPDATE_MEETING_STATUS, conn);
                cmd.Parameters.AddWithValue("@MeetingID", meetingID);
                cmd.Parameters.AddWithValue("@Description", reason);
                cmd.Parameters.AddWithValue("@Status", (int)meetingStatus);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Meeting> SearchMeetingPaged(string meetingName, string roomName, 
            string reservationFromDate, string reservationToDate, string meetingFromDate, string meetingToDate, 
            int pageSize, int pageIndex, out int totalResults)
        {
            string sql = SEARCH_MEETING_PAGED;
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (meetingName != null)
            {
                sql += " AND MeetingName LIKE @MeetingName";
                parameters.Add(new SqlParameter("@MeetingName", meetingName));
            }
            if (roomName != null)
            {
                sql += " AND RoomName LIKE @RoomName";
                parameters.Add(new SqlParameter("@RoomName", roomName));
            }
            if (reservationFromDate != null)
            {
                sql += " AND ReservationTime>=@ReservationFromDate";
                parameters.Add(new SqlParameter("@ReservationFromDate", reservationFromDate));
            }
            if (reservationToDate != null)
            {
                sql += " AND ReservationTime<=@ReservationToDate";
                parameters.Add(new SqlParameter("@ReservationToDate", reservationToDate));
            }
            if (meetingFromDate != null)
            {
                sql += " AND StartTime>=@MeetingFromDate";
                parameters.Add(new SqlParameter("@MeetingFromDate", meetingFromDate));
            }
            if (meetingToDate != null)
            {
                sql += " AND EndTime<=@MeetingToDate";
                parameters.Add(new SqlParameter("@MeetingToDate", meetingToDate));
            }

            sql += " SELECT @TotalResults=@@ROWCOUNT";                  // 获得总行数
            SqlParameter paramTotal = new SqlParameter("@TotalResults", SqlDbType.Int);
            paramTotal.Direction = ParameterDirection.Output;
            parameters.Add(paramTotal);

            // 获取本页内的数据
            sql += " SELECT * FROM @tmp";
            sql += " WHERE RowID>" + pageSize * (pageIndex - 1);
            sql += " AND RowID<=" + pageSize * pageIndex;

            List<Meeting> list = new List<Meeting>();
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters.ToArray());
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Meeting m = new Meeting();
                    m.MeetingID = Convert.ToInt32(dr["MeetingID"]);
                    m.MeetingName = dr["MeetingName"].ToString();
                    m.StartTime = Convert.ToDateTime(dr["StartTime"]);
                    m.EndTime = Convert.ToDateTime(dr["EndTime"]);
                    m.ReservationTime = Convert.ToDateTime(dr["ReservationTime"]);
                    m.Room = new MeetingRoom();
                    m.Room.RoomName = dr["RoomName"].ToString();
                    m.Reservationist = new Employee();
                    m.Reservationist.EmployeeName = dr["ReservationistName"].ToString();
                    list.Add(m);
                }
                dr.Close();
                totalResults = Convert.ToInt32(paramTotal.Value);
            }
            return list;
        }

        public static List<Meeting> SearchRecentMeetingByParticipantAndDate(int employeeID, DateTime recent)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SEARCH_RECENT_MEETING_BY_PARTICIPANT, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@StartTime", recent);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<Meeting> list = new List<Meeting>();
                while (dr.Read())
                {
                    Meeting m = new Meeting();
                    m.MeetingID = Convert.ToInt32(dr["MeetingID"]);
                    m.MeetingName = dr["MeetingName"].ToString();
                    m.StartTime = Convert.ToDateTime(dr["StartTime"]);
                    m.EndTime = Convert.ToDateTime(dr["EndTime"]);
                    m.Room = new MeetingRoom();
                    m.Room.RoomName = dr["RoomName"].ToString();
                    list.Add(m);
                }
                return list;
            } 
        }

        public static List<Meeting> SearchCanceledMeetingsByParticipantAndDate(int employeeID, DateTime recent)
        {
            using (SqlConnection conn = new SqlConnection(DBUtil.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SEARCH_CANCELED_MEETING, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@StartTime", recent);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<Meeting> list = new List<Meeting>();
                while (dr.Read())
                {
                    Meeting m = new Meeting();
                    m.MeetingID = Convert.ToInt32(dr["MeetingID"]);
                    m.MeetingName = dr["MeetingName"].ToString();
                    m.StartTime = Convert.ToDateTime(dr["StartTime"]);
                    m.EndTime = Convert.ToDateTime(dr["EndTime"]);
                    m.Description = dr["Description"].ToString();
                    m.Room = new MeetingRoom();
                    m.Room.RoomName = dr["RoomName"].ToString();
                    list.Add(m);
                }
                return list;
            }
        }
    }
}