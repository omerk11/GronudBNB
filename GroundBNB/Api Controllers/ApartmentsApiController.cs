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

        public async Task<bool> Delete(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
