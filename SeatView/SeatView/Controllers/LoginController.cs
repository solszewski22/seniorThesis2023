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