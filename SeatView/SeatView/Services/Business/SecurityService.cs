using SeatView.Models;
using SeatView.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatView.Services.Business
{
    public class SecurityService
    {
        SecurityDAO daoService = new SecurityDAO();

        public bool authenticate(OwnerModel owner)
        {
            return daoService.FindByOwner(owner);
        }
    }
}