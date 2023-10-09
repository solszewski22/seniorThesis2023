using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatView.Models
{
    // the physical object of an owner; each time an owner is queried from the database its information will reside here
    public class OwnerModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string company { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}