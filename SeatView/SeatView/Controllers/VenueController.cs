using SeatView.Models;
using SeatView.Services.Business;
using System;
using System.Collections.Generic;
using System.IO;
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

        // a method to update limited information about a seat and return a filled in form
        public ActionResult UpdateSeat(int id)
        {
            ServicesImplement seatService = new ServicesImplement();
            SeatModel seat = seatService.retrieveOneSeat(id);

            return View("SeatFormView", seat);
        }

        // processes the seat update by saving the image the updating the seat and media tables
        public ActionResult processSeatUpdate(ImageModel imgModel, SeatModel seatModel)
        {
            string filename = Path.GetFileNameWithoutExtension(imgModel.imageFileName.FileName);
            string extension = Path.GetExtension(imgModel.imageFileName.FileName);

            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            imgModel.mediaURL = "~/Images/Seats/" + filename;

            filename = Path.Combine(Server.MapPath("~/Images/Seats/" + filename));
            imgModel.imageFileName.SaveAs(filename);

            // update seat and media tables in database
            ServicesImplement seatService = new ServicesImplement();
            imgModel.id = seatModel.mediaID;
            if (seatService.updateMedia(imgModel))
            {
                // successfully media update
                if (seatService.updateSeat(seatModel))
                {
                    return displayLayoutInfo();
                }
                else
                {
                    return View("LoginFailed");
                }
            }
            else
            {
                return View("LoginFailed");
            }
        }

        public ActionResult DeleteSeat(int id)
        {
            ServicesImplement seatService = new ServicesImplement();
            if (seatService.deleteSeat(id))
            {
                // if the delete was successful, display the remaining seats
                return displayLayoutInfo();
            }
            else
            {
                return View("LoginFailed");
            }
        }

    }
}