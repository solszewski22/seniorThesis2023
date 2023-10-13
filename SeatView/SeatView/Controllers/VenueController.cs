using SeatView.Models;
using SeatView.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeatView.Controllers
{
    public class VenueController : Controller
    {
        // GET: Venue
        
        // default will display the venue information for one specific venue (id)
        public ActionResult Index(int id)
        {
            ServicesImplement venueService = new ServicesImplement();
            VenueModel venue = venueService.retrieveOneVenue(id);

            return View("Details", venue);
        }
    }
}