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
            // TODO: Make sure only speciic roles can delete apartments
            var apartment = _context.Apartments.Find(id);
            if (apartment != null)
            {
                _context.Reservations.RemoveRange(_context.Reservations.Where(k => k.ApartmentID == apartment.ID));
                _context.Apartments.Remove(apartment);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}