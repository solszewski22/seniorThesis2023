﻿using SeatView.Models;
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
        public ActionResult Login(OwnerModel ownerModel)
        {
            // authenticate whether the entered credentials are found in the database
            ServicesImplement securityService = new ServicesImplement();
            bool loginSuccess = securityService.authenticate(ownerModel);

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
                return View("LoginFailedView");
            }
        }

        // helper method that can be referred to for login or main venues page
        public ActionResult displayVenues()
        {
            // if the user is logged in (meaning their id has been saved and authentication has taken place)
            if(Session["id"] != null)
            {
                int idNum = (int)Session["id"];
                ServicesImplement venueService = new ServicesImplement();
                List<VenueModel> venues = new List<VenueModel>();

                // create a dual model to pass in name of owner and a list of their venues
                OwnerVenueModel dualModel = new OwnerVenueModel();
                dualModel.ownerName = Session["firstName"].ToString();
                dualModel.venues = venueService.retrieveVenues(idNum);

                return View("OwnerLoginView", dualModel);
            }
            else
            {
                // redirect to the login page if the user has not been authenticated
                return RedirectToAction("LoginView");
            }
        }

        // display the new venue form
        public ActionResult newVenue()
        {
            return View("InsertVenueView");
        }

        // process the insert of a new venue
        public ActionResult processVenueInsert(VenueModel newVenue)
        {
            // add to the database
            ServicesImplement venue = new ServicesImplement();
            int ownerID = (int)Session["id"];

            // if the insert into the database comes back without an error...
            if (venue.insertVenue(newVenue, ownerID))
            {
                // display the list of venues for the owner
                return displayVenues();
            }
            else
            {
                return View("LoginFailed");
            }
        }
    }
}