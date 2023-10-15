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
        public ActionResult Index(int id)
        {
            ServicesImplement seatService = new ServicesImplement();
            SeatModel seat = seatService.retrieveOneSeat(id);

            // get matching media for the seat
            MediaModel seatMedia = seatService.retrieveOneMedia(seat.mediaID);

            // create dualModel for the View
            SeatMediaModel dualModel = new SeatMediaModel();
            dualModel.media = seatMedia;
            dualModel.seat = seat;

            return View("SeatDetails", dualModel);
        }
    }
}