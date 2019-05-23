using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpangleERP.GateIn
{
    public class InventoryInClass
    {
        public int invent_id { get; set; }
        public int quantity { get; set; }
        public int whid { get; set; }
        public int grn_id { get; set; }
        public int items_id { get; set; }
        public string status { get; set; }
    }
}