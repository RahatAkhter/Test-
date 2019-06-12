using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpangleERP.GateIn
{
    public class Gate_InClass
    {
        public int GateIn_Id { get; set; }
        public string G_Date { get; set; }
        public string G_Time { get; set; }
        public int ReceivedBy { get; set; }
        public int CheckBy { get; set; }
        public string DriverName { get; set; }
        public string ReferenceBy { get; set; }
        public int G_Status { get; set; }
        public string PO_Number { get; set; }
        public string VehicleNo { get; set; }
    }
}