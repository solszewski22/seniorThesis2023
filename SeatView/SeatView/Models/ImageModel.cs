using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatView.Models
{
    public class ImageModel
    {
        public int id { get; set; }
        public string mediaURL { get; set; }

        public HttpPostedFileBase imageFileName { get; set; }
    }
}