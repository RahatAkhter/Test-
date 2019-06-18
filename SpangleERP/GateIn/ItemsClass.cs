using System;
using SpangleERP.invent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpangleERP.WareHouse
{
    public class ItemsClass
    {
        public int items_id { get; set; }
        public string items_name { get; set; }
    
        public int cat_id { get; set; }
        public string cat_name { get; set; }
        public int type { get; set; }
    }
   
}