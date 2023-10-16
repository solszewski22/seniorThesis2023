using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SeatView.Models
{
    public class VenueModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }

        [DisplayName("Layout")]     // display name on .cshtml pages for the layoutURL attribute
        public string layoutURL { get; set; }
        public int ownerID { get; set; }
    }
}