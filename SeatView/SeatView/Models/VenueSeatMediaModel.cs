using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatView.Models
{
    public class VenueSeatMediaModel
    {
        public VenueModel venue { get; set; }
        public List<SeatMediaModel> seatMedias { get; set; }
    }
}