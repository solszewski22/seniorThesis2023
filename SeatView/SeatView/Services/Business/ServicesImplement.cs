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
        private SecurityDAO daoSecurity;
        private InsertDAO daoInsert;
        private VenueDAO daoVenue;
        private SeatDAO daoSeat;
        private MediaDAO daoMedia;

        public ServicesImplement()
        {
            daoSecurity = new SecurityDAO();
            daoInsert = new InsertDAO();
            daoVenue = new VenueDAO();
            daoSeat = new SeatDAO();
            daoMedia = new MediaDAO();
        }

        // owners
        public bool authenticate(OwnerModel owner)
        {
            // if the username and password are found, return true (the result of FindByOwner())
            return daoSecurity.FindByOwner(owner);
        }
        public bool insertOwner(OwnerModel owner)
        {
            // a method that creates an object of an insert service attempts to add a new owner
            // method returns true if the insert is successful
            return daoInsert.InsertOwner(owner);
        }


        // venues
        public List<VenueModel> retrieveVenues(int ownerID)
        {
            // method that calls the VenueDAO to query a list of venues
            // return the list that is queried from the database to be passed to the controller
            return daoVenue.getAllVenues(ownerID);
        }
        public List<VenueModel> retrieveAllVenues()
        {
            return daoVenue.getAllVenues();
        }
        public VenueModel retrieveOneVenue(int venueID)
        {
            // method that call VenueDAO to query the database for one venue based on passed in id
            return daoVenue.getVenueByID(venueID);
        }
        public List<VenueModel> retrieveSearchVenues(string searchString)
        {
            return daoVenue.getVenueBySearch(searchString);
        }
        public bool insertOrUpdateVenue(VenueModel venue, int ownerID)
        {
            // method to insert or update a new venue into the database
            // call an instance of VenueDAO and call addOrUpdateVenue() -- return true/false if the insert/update was successful
            return daoVenue.addOrUpdateVenue(venue, ownerID);
        }
        public bool deleteVenue(int venueID)
        {
            // method to delete a venue int the database
            return daoVenue.deleteVenue(venueID);
        }


        // seats
        public List<SeatModel> getSeatsForVenue(int venueID)
        {
            // method to retrieve all the seats in one venue by id
            return daoSeat.getSeats(venueID);
        }
        public SeatModel retrieveOneSeat(int seatID)
        {
            // method to retrieve one seat by a seatID
            return daoSeat.getSeatByID(seatID);
        }
        public bool deleteSeat(int seatID)
        {
            // method to delete one seat by given seatID
            return daoSeat.deleteSeat(seatID);
        }
        public bool updateSeat(SeatModel seatModel)
        {
            // method updates a seat entity
            return daoSeat.updateSeat(seatModel);
        }
        public bool insertSeat(SeatModel seatModel)
        {
            // method to insert a seat entity
            return daoSeat.insertSeat(seatModel);
        }
        public List<SeatModel> retrieveSeats(int x_coord, int y_coord, int venueID)
        {
            return daoSeat.getSeatsByCoordinates(x_coord, y_coord, venueID);
        }
        public List<SeatModel> retrieveSearchSeats(string searchString)
        {
            return daoSeat.getSeatsBySearch(searchString);
        }


        // medias
        public ImageModel retrieveOneMedia(int mediaID)
        {
            // method to retrieve one media by a mediaID
            return daoMedia.getMediaByID(mediaID);
        }
        public bool updateMedia(ImageModel imgModel)
        {
            // method updates a media entity
            return daoMedia.updateMedia(imgModel); 
        }
        public int insertMedia(ImageModel imgModel)
        {
            // method to insert a image entity
            return daoMedia.insertMedia(imgModel);
        }
    }
}