﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatView.Models
{
    public class OwnerVenueModel
    {
        public string ownerName { get; set; }
        public List<VenueModel> venues { get; set; }
    }
}