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

        private ServicesImplement service;

        public VenueController()
        {
            service = new ServicesImplement();
        }
        
        public ActionResult Index(int id)
        {
            // default will display the venue information for one specific venue (id)
            VenueModel venue = service.retrieveOneVenue(id);
            return View("Details", venue);
        }
        public ActionResult editLayout(int id, string url)
        {
            // a method to display a layout image and a list of seats at the venue
            Session["venueID"] = id;
            Session["layoutURL"] = url;
            return displayLayoutInfo();
        }
        public ActionResult displayLayoutInfo()
        {
            int venueID = (int)Session["venueID"];
            string url = (string)Session["layoutURL"];

            // get a list of SeatModels
            List<SeatModel> seats = service.getSeatsForVenue(venueID);

            // create a dual model to display the layout image and list of seats
            VenueSeatModel dualModel = new VenueSeatModel();
            dualModel.venueLayout = url;
            dualModel.seats = seats;

            return View("LayoutInfoView", dualModel);
        }
        public ActionResult UpdateSeat(int id)
        {
            // a method to update limited information about a seat and return a filled in form
            SeatModel seat = service.retrieveOneSeat(id);
            return View("SeatFormView", seat);
        }
        public ActionResult processSeatUpdate(ImageModel imgModel, SeatModel seatModel)
        {
            // processes the seat update by saving the image the updating the seat and media tables
            SeatModel seat = service.retrieveOneSeat(seatModel.id);
            ImageModel img = service.retrieveOneMedia(seat.mediaID);
            string filePath = Path.Combine(Server.MapPath(img.mediaURL));

            // if the file exists in the images folder, delete it
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                file.Delete();
            }

            if (imgModel.imageFileName != null)
            {
                saveMedia(imgModel);
                // update seat and media tables in database
                imgModel.id = seatModel.mediaID;
                service.updateMedia(imgModel);

            }

            // update the seat
            if (service.updateSeat(seatModel))
            {
                return displayLayoutInfo();
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult DeleteSeat(int id)
        {
            SeatModel seat = service.retrieveOneSeat(id);
            ImageModel img = service.retrieveOneMedia(seat.mediaID);
            string filePath = Path.Combine(Server.MapPath(img.mediaURL));

            // if the file exists in the images folder, delete it
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                file.Delete();
            }

            if (service.deleteSeat(id))
            {
                // if the delete was successful, display the remaining seats
                return displayLayoutInfo();
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult InsertSeat()
        {
            SeatModel seat = new SeatModel();
            seat.venueUrl = Session["layoutURL"].ToString();
            return View("AddSeatView", seat);
        }
        public ActionResult processSeatInsert(SeatModel seat, ImageModel img)
        {
            // add a new seat and it's media to the database
            // save the image to the seat view folder of images
            saveMedia(img);

            // insert the media into the database
            int newMediaID = service.insertMedia(img);

            // note the newly added id of the image in the database to be set for the seat entity
            seat.mediaID = newMediaID;
            seat.venueID = (int)Session["venueID"];
            seat.venueUrl = (string)Session["layoutURL"];

            // insert the seat into the database
            if (service.insertSeat(seat))
            {
                // successful addition to the database
                return displayLayoutInfo();
            }
            else
            {
                return View("Error");
            }

        }
        public void saveMedia(ImageModel imgModel)
        {
            // seperate function to save a media image to the 'Images' folder
            string filename = Path.GetFileNameWithoutExtension(imgModel.imageFileName.FileName);
            string extension = Path.GetExtension(imgModel.imageFileName.FileName);

            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            imgModel.mediaURL = "~/Images/Seats/" + filename;

            filename = Path.Combine(Server.MapPath("~/Images/Seats/" + filename));
            imgModel.imageFileName.SaveAs(filename);
        }
        public ActionResult UserIndex()
        {
            OwnerVenueModel dualModel = new OwnerVenueModel();
            dualModel.venues = service.retrieveAllVenues();
            return View("UserView", dualModel);
        }

        [HttpPost]
        public ActionResult searchVenue(string searchString)
        {
            // search for a specfic venue based on the passed in string
            OwnerVenueModel dualModel = new OwnerVenueModel();
            dualModel.venues = service.retrieveSearchVenues(searchString);
            return View("UserView", dualModel);

        }
        public ActionResult chosenVenue(int id)
        {
            Session["userVenueID"] = id;
            return displayVenueDetails();
        }
        public ActionResult displayVenueDetails()
        {
            int id = (int)Session["userVenueID"];
            // get a list of SeatModels
            VenueModel venue = service.retrieveOneVenue(id);
            List<SeatModel> seats = service.getSeatsForVenue(venue.id);

            // create a dual model to display the layout image and list of seats
            VenueSeatModel dualModel = new VenueSeatModel();
            dualModel.venueLayout = venue.layoutURL;
            dualModel.seats = seats;

            return View("VenueUserDetailsView", dualModel);
        }
        public ActionResult chooseFromLayout()
        {
            VenueModel venue = service.retrieveOneVenue((int)Session["userVenueID"]);
            return View("SeatByLayoutView", venue);
        }
        public ActionResult requestSeatView(int x_coord, int y_coord, int id)
        {
            List<SeatModel> seats = service.retrieveSeats(x_coord, y_coord, id);

            List<SeatMediaModel> seatMedias = new List<SeatMediaModel>();
            for(int i = 0; i < seats.Count(); i++)
            {
                SeatMediaModel seatMedia = new SeatMediaModel();
                ImageModel seatImg = service.retrieveOneMedia(seats[i].mediaID);

                seatMedia.media = seatImg;
                seatMedia.seat = seats[i];
                seatMedias.Add(seatMedia);
            }

            return View("MediaView", seatMedias);
        }  
        public ActionResult searchSeat(string searchString)
        {
            // search for specific seat based on passed in string
            VenueSeatModel dualModel = new VenueSeatModel();

            VenueModel venue = service.retrieveOneVenue((int)Session["userVenueID"]);
            dualModel.venueLayout = venue.layoutURL;

            List<SeatModel> seats = service.retrieveSearchSeats(searchString);
            dualModel.seats = seats;

            return View("VenueUserDetailsView", dualModel);
        }
        public ActionResult clearSeatSearch()
        {
            return displayVenueDetails();
        }
        public ActionResult clearVenueSearch()
        {
            return UserIndex();
        }
    }
}