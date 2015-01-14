using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ETC.EEG.CoolMeeting.DAL
{
    public class DBUtil
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["CoolMeetingDB"].ConnectionString;
    }
}
