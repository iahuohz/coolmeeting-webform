using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETC.EEG.CoolMeeting.Model
{
    /// <summary>
    /// 部门信息实体类
    /// </summary>
    public class Department
    {
        private int departmentID;
        private string departmentName;
        
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DepartmentID
        {
            get { return departmentID; }
            set { departmentID = value; }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }
    }
}
