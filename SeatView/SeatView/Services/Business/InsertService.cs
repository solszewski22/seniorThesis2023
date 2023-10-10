using SeatView.Models;
using SeatView.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatView.Services.Business
{
    public class InsertService
    {
        // a method that creates an object of an insert service attempts to add a new owner
        // method returns true if the insert is successful
        public bool insertOwner(OwnerModel owner)
        {
            InsertDAO daoInsert = new InsertDAO();

            return daoInsert.InsertOwner(owner);
        }
    }
}