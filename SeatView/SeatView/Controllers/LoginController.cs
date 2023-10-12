using SeatView.Models;
using SeatView.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeatView.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("LoginView");
        }

        // a method that returns an action (more specifically a view or page) after database verifies that 
        // the username and password are present
        public ActionResult Login(VenueModel venueModel, OwnerModel ownerModel)
        {
            // authenticate whether the entered credentials are found in the database
            ServicesImplement securityService = new ServicesImplement();
            bool loginSuccess = securityService.authenticate(ownerModel);

            // if those credentials are correct...
            if (loginSuccess)
            {
                // create a venue service that will retrieve all the venues for a specific owner
                ServicesImplement venueService = new ServicesImplement();

                // create a model that consists of two models so that both can be passed to the view
                OwnerVenueModel dualModel = new OwnerVenueModel();
                dualModel.owner = ownerModel;
                dualModel.venues = venueService.retrieveVenues(venueModel, ownerModel);

                return View("OwnerLoginView", dualModel);
            }
            else
            {
                return View("LoginFailedView");
            }
        }
    }
}