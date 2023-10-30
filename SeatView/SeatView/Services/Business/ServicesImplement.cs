using SeatView.Models;
using SeatView.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatView.Services.Business
{
    // a service that queries another class to perform an authentication method
    public class ServicesImplement
    {
        // if the username and password are found, return true (the result of FindByOwner())
        public bool authenticate(OwnerModel owner)
        {
            SecurityDAO daoService = new SecurityDAO();
            return daoService.FindByOwner(owner);
        }

        // a method that creates an object of an insert service attempts to add a new owner
        // method returns true if the insert is successful
        public bool insertOwner(OwnerModel owner)
        {
            InsertDAO daoInsert = new InsertDAO();
            return daoInsert.InsertOwner(owner);
        }

        // method that calls the VenueDAO to query a list of venues
        // return the list that is queried from the database to be passed to the controller
        public List<VenueModel> retrieveVenues(int ownerID)
        {
            VenueDAO daoVenue = new VenueDAO();
            return daoVenue.getAllVenues(ownerID);
        }

        // method that call VenueDAO to query the database for one venue based on passed in id
        public VenueModel retrieveOneVenue(int venueID)
        {
            VenueDAO daoVenue = new VenueDAO();
            return daoVenue.getVenueByID(venueID);
        }

        // method to insert or update a new venue into the database
        // call an instance of VenueDAO and call addOrUpdateVenue() -- return true/false if the insert/update was successful
        public bool insertOrUpdateVenue(VenueModel venue, int ownerID)
        {
            VenueDAO daoVenue = new VenueDAO();
            return daoVenue.addOrUpdateVenue(venue, ownerID);
        }

        // method to delete a venue int the database
        public bool deleteVenue(int venueID)
        {
            VenueDAO daoVenue = new VenueDAO();
            return daoVenue.deleteVenue(venueID);
        }

        // method to retrieve all the seats in one venue by id
        public List<SeatModel> getSeatsForVenue(int venueID)
        {
            SeatDAO daoSeat = new SeatDAO();
            return daoSeat.getSeats(venueID);
        }

        // method to retrieve one seat by a seatID
        public SeatModel retrieveOneSeat(int seatID)
        {
            SeatDAO daoSeat = new SeatDAO();
            return daoSeat.getSeatByID(seatID);
        }

        // method to retrieve one media by a mediaID
        public ImageModel retrieveOneMedia(int mediaID)
        {
            MediaDAO daoMedia = new MediaDAO();
            return daoMedia.getMediaByID(mediaID);
        }

        // method to delete one seat by given seatID
        public bool deleteSeat(int seatID)
        {
            SeatDAO daoSeat = new SeatDAO();
            return daoSeat.deleteSeat(seatID);
        }

        // method updates a media entity
        public bool updateMedia(ImageModel imgModel)
        {
            MediaDAO daoMedia = new MediaDAO();
            return daoMedia.updateMedia(imgModel); 
        }
    
        // method updates a seat entity
        public bool updateSeat(SeatModel seatModel)
        {
            SeatDAO daoSeat = new SeatDAO();
            return daoSeat.updateSeat(seatModel);
        }
        
        // method to insert a image entity
        public int insertMedia(ImageModel imgModel)
        {
            MediaDAO daoMedia = new MediaDAO();
            return daoMedia.insertMedia(imgModel);
        }
    
        // method to insert a seat entity
        public bool insertSeat(SeatModel seatModel)
        {
            SeatDAO daoSeat = new SeatDAO();
            return daoSeat.insertSeat(seatModel);
        }
    }
}