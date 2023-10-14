using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatView.Models
{
    public class SeatModel
    {
        public int id { get; set; }
        public string x_coord { get; set; }
        public string y_coord { get; set; }
        public string section { get; set; }
        public string row { get; set; }
        public string seatNum { get; set; }
        public int venueID { get; set; }
        public int mediaID { get; set; }
    }
}