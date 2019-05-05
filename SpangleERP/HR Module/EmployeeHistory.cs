using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpangleERP.HR_Module
{
    public class EmployeeHistory
    {
        public int hist_id { get; set; }
        public string date_of_exit { get; set; }
        public int emp_id { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }
        public string emp_name { get; set; }
        public string designation { get; set; }
        public string documrnt { get; set; }
    }
}