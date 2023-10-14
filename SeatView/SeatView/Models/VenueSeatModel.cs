using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatView.Models
{
    public class VenueSeatModel
    {
        public string venueLayout { get; set; }
        public List<SeatModel> seats { get; set; }
    }
}