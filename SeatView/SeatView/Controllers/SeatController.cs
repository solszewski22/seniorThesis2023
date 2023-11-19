using SeatView.Models;
using SeatView.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeatView.Controllers
{
    public class SeatController : Controller
    {
        // GET: Seat

        private ServicesImplement service;

        public SeatController()
        {
            service = new ServicesImplement();
        }
        public ActionResult Index(int id)
        {
            SeatMediaModel dualModel = new SeatMediaModel();
            dualModel.userAccessed = false;

            return renderView(dualModel, id);
        }
        public ActionResult UserSeatIndex(int id)
        {
            SeatMediaModel dualModel = new SeatMediaModel();
            dualModel.userAccessed = true;

            return renderView(dualModel, id);
        }

        public ActionResult renderView(SeatMediaModel dualModel, int id)
        {
            SeatModel seat = service.retrieveOneSeat(id);

            ImageModel seatMedia = service.retrieveOneMedia(seat.mediaID);

            dualModel.media = seatMedia;
            dualModel.seat = seat;

            return View("SeatDetails", dualModel);
        }
    }
}