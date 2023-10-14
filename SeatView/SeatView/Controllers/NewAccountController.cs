using SeatView.Models;
using SeatView.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeatView.Controllers
{
    public class NewAccountController : Controller
    {
        // GET: NewAccount
        public ActionResult Index()
        {
            return View("NewAccountView");
        }

        // a method that calls a service to query the database and insert a new owner
        public ActionResult AccountCreation(OwnerModel ownerModel)
        {
            // if the insert is successful, login in the user to the 'OwnerLoginView'
            // else show a login failure message/page
            ServicesImplement insertService = new ServicesImplement();
            if (insertService.insertOwner(ownerModel))
            {
                // create a dualModel to pass to the View -- allows for the display of name and list of venues
                List<VenueModel> venues = new List<VenueModel>();
                OwnerVenueModel dualModel = new OwnerVenueModel();
                dualModel.ownerName = ownerModel.firstName;
                dualModel.venues = venues;
                return View("OwnerLoginView", dualModel);
            }
            else
            {
                return View("LoginFailedView");
            }            
        }
    }
}