using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETC.EEG.CoolMeeting.Model;
using ETC.EEG.CoolMeeting.DAL;

namespace ETC.EEG.CoolMeeting.BLL
{
    public enum MeetingOpResults
    {
        Success = 1,
        ReservationTooLate = -1,                    // 预定时间太迟（必须提前30分钟以上预定会议)
        RoomScheduleNotAvailable = -2,              // 会议室在这个时间段已经被占用
        NotEnoughCapacity = -3,                     // 会议室容量不足以容纳人数
        MeetingCanNotCancel = -4                    // 会议已经开始，不能撤销会议
    }

    public class BLLMeeting
    {
        public static MeetingOpResults ReserveMeeting(Meeting meeting)
        {
            // 检查预定时间
            if (DateTime.Now.AddMinutes(30) > meeting.StartTime)
            {
                return MeetingOpResults.ReservationTooLate;
            }
            else
            {
                meeting.ReservationTime = DateTime.Now;
            }

            // 检查会议室容量
            if (meeting.Room.Capacity < meeting.NumberofParticipants)
            {
                return MeetingOpResults.NotEnoughCapacity;
            }

            // 检查会议室档期是否冲突
            if (DALMeeting.IsRoomOccupiedDuringTimeSpan(meeting))
            {
                return MeetingOpResults.RoomScheduleNotAvailable;
            }
            
            // 插入会议数据
            int meetingID = DALMeeting.InsertMeeting(meeting);

            // 插入参会人员数据
            foreach (Employee employee in meeting.Participants)
            {
                DALMeeting.InsertMeetingParticipant(meetingID, employee.EmployeeID);
            }

            return MeetingOpResults.Success;
        }

        public static List<Meeting> GetReservationsByReservationistID(int reservationistID)
        {
            return DALMeeting.GetReservationsByReservationistID(reservationistID);
        }

        public static List<Meeting> GetMeetingsForEmployee(int employeeID)
        {
            return DALMeeting.GetMeetingsForEmployee(employeeID);
        }

        public static Meeting GetMeetingDetails(int meetingID)
        {
            Meeting m =  DALMeeting.GetMeetingByID(meetingID);
            m.Participants = DALMeeting.GetParticipantsByMeetingID(meetingID);
            return m;
        }

        public static MeetingOpResults CancelMeeting(int meetingID, string reason)
        {
            Meeting m = DALMeeting.GetMeetingByID(meetingID);
            if (m.StartTime <= DateTime.Now)
            {
                return MeetingOpResults.MeetingCanNotCancel;
            }
            DALMeeting.UpdateMeetingStatus(MeetingStatus.Canceled, reason,  meetingID);
            return MeetingOpResults.Success;
        }

        public static List<Meeting> SearchPagedMeetings(string meetingName, string roomName, 
            string reservationFromDate, string reservationToDate, string meetingFromDate, string meetingToDate, 
            int pageSize, int pageIndex, out int totalResults)
        {
            meetingName = string.IsNullOrWhiteSpace(meetingName) ? null : "%" + meetingName + "%";
            roomName = string.IsNullOrWhiteSpace(roomName) ? null : "%" + roomName + "%";
            reservationFromDate = string.IsNullOrWhiteSpace(reservationFromDate) ? null : reservationFromDate;
            reservationToDate = string.IsNullOrWhiteSpace(reservationToDate) ? null : reservationToDate;
            meetingFromDate = string.IsNullOrWhiteSpace(meetingFromDate) ? null : meetingFromDate;
            meetingToDate = string.IsNullOrWhiteSpace(meetingToDate) ? null : meetingToDate;
            return DALMeeting.SearchMeetingPaged(meetingName, roomName, reservationFromDate, reservationToDate,
                meetingFromDate, meetingToDate, pageSize, pageIndex, out totalResults);
        }

        public static List<Meeting> GetRecentMeetings()
        {
            Employee emp = BLLStaff.GetEmployeeforLoggedInUser();
            return DALMeeting.SearchRecentMeetingByParticipantAndDate(emp.EmployeeID, DateTime.Now.AddDays(-7));
        }

        public static List<Meeting> GetCanceledMeetings()
        {
            Employee emp = BLLStaff.GetEmployeeforLoggedInUser();
            return DALMeeting.SearchCanceledMeetingsByParticipantAndDate(emp.EmployeeID, DateTime.Now);
        }
    }
}