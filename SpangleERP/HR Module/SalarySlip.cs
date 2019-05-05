using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpangleERP.HR_Module
{
    public class SalarySlip : All_Employees 
    {
        public int latedays { get; set; }
        public int Worked_days { get; set; }
        public int salary_pack { get; set; }
        public int HalfDays { get; set; }
        public float basic { get; set; }
        public float petrol { get; set; }
        public int mob { get; set; }
        public float lunch { get; set; }
        public float medical { get; set; }
        public float house_rent { get; set; }
        public float Utility { get; set; }
        public float Driver_Fuel { get; set; }
        public float car { get; set; }
        public float OverTime { get; set; }
        public float IncomeTax { get; set; }
        public float ProvidentFund { get; set; }
        public float leave_daysDeduction { get; set; }
        public float GrossEarning { get; set; }
        public float GrossDeduction { get; set; }
        public float NetPay { get; set; }
        public int bonus { get; set; }
        public int ms { get; set; }
        public string month_year { get; set; }
       
    }
}