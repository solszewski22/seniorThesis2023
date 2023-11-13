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
    public class LoginController : Controller
    {
        // GET: Login
        private ServicesImplement service;

        public LoginController()
        {
            service = new ServicesImplement();
        }
        //public ActionResult Index()
        //{
        //    return View("LoginView");
        //}

        public ActionResult Login(OwnerModel ownerModel)
        {
            // a method that returns an action (more specifically a view or page) after database verifies that 
            // the username and password are present
            // authenticate whether the entered credentials are found in the database
            bool loginSuccess = service.authenticate(ownerModel);

            // if those credentials are correct...
            if (loginSuccess)
            {
                // store the id of the owner (saves over the switch between views and controllers)
                Session["id"] = ownerModel.id;
                Session["firstName"] = ownerModel.firstName;

                // call helper method to display the list of venues for the owner
                return displayVenues();
            }
            else
            {
                return View("LoginView");
            }
        }
        public ActionResult displayVenues()
        {
            // helper method that can be referred to for login or main venues page
            // if the user is logged in (meaning their id has been saved and authentication has taken place)
            if (Session["id"] != null)
            {
                int idNum = (int)Session["id"];
                List<VenueModel> venues = new List<VenueModel>();

                // create a dual model to pass in name of owner and a list of their venues
                OwnerVenueModel dualModel = new OwnerVenueModel();
                dualModel.ownerName = Session["firstName"].ToString();
                dualModel.venues = service.retrieveVenues(idNum);

                return View("OwnerLoginView", dualModel);
            }
            else
            {
                // redirect to the login page if the user has not been authenticated
                return RedirectToAction("LoginView");
            }
        }
        public ActionResult InsertVenue()
        {
            // display the new venue form
            // pass in an empty venue to avoid null system errors
            VenueModel emptyVenue = new VenueModel();
            return View("VenueFormView", emptyVenue);
        }

        [HttpPost]
        public ActionResult processVenueInsert(ImageModel imgModel, VenueModel venueModel)
        {
            // saves an image to the Images folder and calls helper method to insert data into the database
            VenueModel venue = service.retrieveOneVenue(venueModel.id);
            string filePath = Path.Combine(Server.MapPath(venue.layoutURL));

            // if the file exists in the images folder, delete it
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                file.Delete();
            }

            // save image into images folder in SeatView project
            // build the filename and path to store the image in the folder if applicable
            if (imgModel.imageFileName != null)
            { 
                // delete image to be replaced -- or replace
                string filename = Path.GetFileNameWithoutExtension(imgModel.imageFileName.FileName);
                string extension = Path.GetExtension(imgModel.imageFileName.FileName);

                // concatenate the filename
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                imgModel.mediaURL = "~/Images/Layouts/" + filename;

                // set layoutURL in incoming venueModel
                venueModel.layoutURL = imgModel.mediaURL;

                // get the exact local path to the file in the Images folder
                filename = Path.Combine(Server.MapPath("~/Images/Layouts/" + filename));

                // save the image to the project's Images folder
                imgModel.imageFileName.SaveAs(filename);
            }

            // save the image path and new venue into database
            return processVenueRequest(venueModel);
        }
        public ActionResult UpdateVenue(int id)
        {
            // display venue form with editable fields for a specific venue by id
            VenueModel venue = service.retrieveOneVenue(id);
            return View("VenueFormView", venue);
        }
        public ActionResult processVenueRequest(VenueModel newVenue)
        {
            // process the insert/Update of a new venue
            int ownerID = (int)Session["id"];

            // if the sqlQuery into the database comes back without an error...
            if (service.insertOrUpdateVenue(newVenue, ownerID))
            {
                // display the list of venues for the owner
                return displayVenues();
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult DeleteVenue(int id)
        {
            // delete the file in the Images folder first
            // get the layoutURL for the specific venue
            VenueModel venue = service.retrieveOneVenue(id);

            // get the full path to the image to delete
            string filePath = Path.Combine(Server.MapPath(venue.layoutURL));

            // if the file exists in the images folder, delete it
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                file.Delete();
            }

            if (service.deleteVenue(id))
            {
                // successful delete
                return displayVenues();
            }
            else
            {
                return View("Error");
            }
        }
    }
}