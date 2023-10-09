using SeatView.Models;
using SeatView.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatView.Services.Business
{
    // a service that queries another class to perform an authentication method
    public class SecurityService
    {
        SecurityDAO daoService = new SecurityDAO();

        // if the username and password are found, return true (the result of FindByOwner())
        public bool authenticate(OwnerModel owner)
        {
            return daoService.FindByOwner(owner);
        }
    }
}