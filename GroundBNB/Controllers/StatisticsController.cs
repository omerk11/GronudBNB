using GroundBNB.Data;
using GroundBNB.Models;
using GroundBNB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GroundBNB.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly SiteContext _context;
        private readonly ISiteViewsService _siteviews;

        public StatisticsController(SiteContext context, ISiteViewsService siteViews)
        {
            _context = context;
            _siteviews = siteViews;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            this._siteviews.Increment();
            List<SiteViews> siteViews = this._siteviews.GetSiteViews();
            return View(siteViews);
        }
    }
}
