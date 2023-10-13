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
    }
}