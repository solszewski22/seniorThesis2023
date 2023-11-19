using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatView.Models
{
    public class SeatMediaModel
    {
        public ImageModel media { get; set; }
        public SeatModel seat { get; set; }
        public Boolean userAccessed { get; set; }
    }
}