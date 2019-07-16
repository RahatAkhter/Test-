using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpangleERP.Sales
{
    public class ClsDistributor
    {
       public int Distributor_id { get; set; }
        public string dist_name { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string Zone { get; set; }
        public string Address { get; set; }
    }
}