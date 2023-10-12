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
                return View("OwnerLoginView", ownerModel);
            }
            else
            {
                return View("LoginFailedView");
            }            
        }
    }
}