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
        public ActionResult Login(OwnerModel ownerModel)
        {
            SecurityService securityService = new SecurityService();
            bool loginSuccess = securityService.authenticate(ownerModel);
            if (loginSuccess)
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