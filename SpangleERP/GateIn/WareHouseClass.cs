using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpangleERP.WareHouse
{
    public class WareHouseClass
    {
        public int wh_id { get; set; }
        public string wh_name { get; set; }
        public int wh_head { get; set; }
        public decimal investment { get; set; }
        public decimal totalspace { get; set; }
        public int total_emp { get; set; }
        public string city { get; set; }
    }
}