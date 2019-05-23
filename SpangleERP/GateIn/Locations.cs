using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpangleERP.WareHouse
{
    public class Locations
    {
        public int loc_id { get; set; }
        public string loc_name { get; set; }
        public int loc_space { get; set; }
        public int wh_id { get; set; }
    }
}