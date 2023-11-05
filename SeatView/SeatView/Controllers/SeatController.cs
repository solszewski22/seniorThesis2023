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
            SeatModel seat = service.retrieveOneSeat(id);

            // get matching media for the seat
            ImageModel seatMedia = service.retrieveOneMedia(seat.mediaID);

            // create dualModel for the View
            SeatMediaModel dualModel = new SeatMediaModel();
            dualModel.media = seatMedia;
            dualModel.seat = seat;

            return View("SeatDetails", dualModel);
        }
        public ActionResult UserSeatIndex(int id)
        {
            SeatModel seat = service.retrieveOneSeat(id);

            // get matching media for the seat
            ImageModel seatMedia = service.retrieveOneMedia(seat.mediaID);

            // create dualModel for the View
            SeatMediaModel dualModel = new SeatMediaModel();
            dualModel.media = seatMedia;
            dualModel.seat = seat;

            return View("UserSeatDetails", dualModel);
        }    
    }
}