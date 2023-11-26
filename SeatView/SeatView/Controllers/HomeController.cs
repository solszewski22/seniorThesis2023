using SeatView.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeatView.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["id"] = null;
            return View();
        }
        public ActionResult UserLogin()
        {
            return View("SharedLoginView");
        }

        public ActionResult UserLoginFailure()
        {
            OwnerModel ownerModel = new OwnerModel();
            ownerModel.username = TempData["username"].ToString();
            ownerModel.password = TempData["password"].ToString();

            return View("SharedLoginView", ownerModel);
        }
    }
}