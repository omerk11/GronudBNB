using GroundBNB.Data;
using GroundBNB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsApiController : ControllerBase
    {
        private readonly SiteContext _context;
        private readonly ISiteViewsService _siteviews;
        private readonly IApartmentViewsService _apartmentviews;

        public ApartmentsApiController(SiteContext context, ISiteViewsService siteViews, IApartmentViewsService apartmentViews)
        {
            _context = context;
            _siteviews = siteViews;
            _apartmentviews = apartmentViews;
        }

        [Route("Delete")]
        [HttpPost]

        public bool Delete([FromForm] int id)
        {

            var apartment = _context.Apartments.Find(id);
            if (!User.Identity.IsAuthenticated || 
                !(User.IsInRole("Admin") ||(apartment.ApartmentOwnerID.ToString() == User.Claims.FirstOrDefault(c => c.Type == "ID").ToString().Replace("ID: ", ""))))
            {
                return false;
            }
            if (apartment != null)
            {
                var res = _context.Reservations.Where(k => k.ApartmentID == apartment.ID);
                var views = _context.ApartmentViews.Where(k => k.ApartmentID == apartment.ID);
                if (res.Count() > 0)
                {
                    _context.Reservations.RemoveRange(res);
                }

                if (views.Count() > 0)
                {
                    _context.ApartmentViews.RemoveRange(views);
                }

                _context.Apartments.Remove(apartment);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}