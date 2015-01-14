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
    public partial class MyReservations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindReservations();
            }
        }

        private void BindReservations()
        {
            Employee reservationist = BLLStaff.GetEmployeeforLoggedInUser();
            List<Meeting> list = BLLMeeting.GetReservationsByReservationistID(reservationist.EmployeeID);
            repReservations.DataSource = list;
            repReservations.DataBind();
        }
    }
}