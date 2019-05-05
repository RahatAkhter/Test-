using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpangleERP.HR_Module
{
    public class AttendenceData
    {
        public string emp_name { get; set; }
        public string Img { get; set; }
        public string timein { get; set; }
        public string time_out { get; set; }
        public string late { get; set; }
        public string half { get; set; }
        public string date { get; set; }
        public int status {get;set;}
        public string Overtime { get; set; }
    }
}