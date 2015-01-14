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
    public partial class SearchEmployees : System.Web.UI.Page
    {
        private const int pageSize = 10;               // 每页显示10条记录

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                divNoResults.Visible = false;
                divResults.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPagedEmployees(1);
        }

        /// <summary>
        /// 分页绑定数据
        /// </summary>
        /// <param name="currentPage">要显示的当前页码数</param>
        private void BindPagedEmployees(int pageIndex)
        {
            string employeeName = txtEmployeeName.Text;
            string userName = txtUserName.Text;
            EmployeeStatusType status = (EmployeeStatusType)Convert.ToInt32(rblStatus.SelectedValue);
            int totalResults;               // 返回的员工总行数
            List<Employee> results = BLLStaff.SearchPagedEmployees(employeeName, userName, status,
                pageSize, pageIndex, out totalResults);
            int totalPages = totalResults / pageSize + (totalResults % pageSize == 0 ? 0 : 1);  // 计算总分页数

            if (totalPages == 0)            // 如果没有返回记录
            {
                divNoResults.Visible = true;
                divResults.Visible = false;
            }
            else
            {
                divNoResults.Visible = false;
                divResults.Visible = true;

                lblTotalResults.Text = totalResults.ToString();     // 显示总行数
                lblTotalPages.Text = totalPages.ToString();         // 显示总分页数
                lblCurrentPage.Text = pageIndex.ToString();         // 显示当前页码数
            }

            repEmployees.DataSource = results;
            repEmployees.DataBind();
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            BindPagedEmployees(1);
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            int currentPage = Convert.ToInt32(lblCurrentPage.Text);
            int totalPages = Convert.ToInt32(lblTotalPages.Text);
            if (currentPage == 1)           // 如果已经是第一页，则继续显示第一页的结果即可
            {
                BindPagedEmployees(currentPage);
            }
            else
            {
                currentPage = currentPage - 1;
                BindPagedEmployees(currentPage);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int currentPage = Convert.ToInt32(lblCurrentPage.Text);
            int totalPages = Convert.ToInt32(lblTotalPages.Text);
            if (currentPage == totalPages)  // 如果已经是最后一页，则继续显示最后一页结果即可
            {
                BindPagedEmployees(currentPage);
            }
            else
            {
                currentPage = currentPage + 1;
                BindPagedEmployees(currentPage);
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            int totalPages = Convert.ToInt32(lblTotalPages.Text);
            BindPagedEmployees(totalPages);
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            int pageNum = Convert.ToInt32(txtPageNumber.Text);
            int totalPages = Convert.ToInt32(lblTotalPages.Text);
            if (pageNum <= 1 || pageNum > totalPages)
            {
                BindPagedEmployees(1);
            }
            else
            {
                BindPagedEmployees(pageNum);
            }
        }

        protected void repEmployees_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CloseAccount")
            {
                int employeeID = Convert.ToInt32(e.CommandArgument);
                Label lblUserName = e.Item.FindControl("lblUserName") as Label;
                string userName = lblUserName.Text;

                BLLStaff.CloseAccount(employeeID, userName);

                BindPagedEmployees(1);
            }
        }
    }
}