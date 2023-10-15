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

        // a method to display a layout image and a list of seats at the venue
        public ActionResult editLayout(int id, string url)
        {
            Session["venueID"] = id;
            Session["layoutURL"] = url;

            return displayLayoutInfo();
        }

        public ActionResult displayLayoutInfo()
        {
            int venueID = (int)Session["venueID"];
            string url = (string)Session["layoutURL"];

            // get a list of SeatModels
            ServicesImplement seatService = new ServicesImplement();
            List<SeatModel> seats = seatService.getSeatsForVenue(venueID);

            // create a dual model to display the layout image and list of seats
            VenueSeatModel dualModel = new VenueSeatModel();
            dualModel.venueLayout = url;
            dualModel.seats = seats;

            return View("LayoutInfoView", dualModel);
        }

        //public ActionResult InsertSeat()
        //{
        //    return View("SeatFormView");
        //}
    }
}