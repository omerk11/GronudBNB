using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroundBNB.Data;
using GroundBNB.Models;
using GroundBNB.Services;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace GroundBNB.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly SiteContext _context;
        private readonly ISiteViewsService _siteviews;
        private readonly IApartmentViewsService _apartmentviews;

        public ApartmentsController(SiteContext context, ISiteViewsService siteViews, IApartmentViewsService apartmentViews)
        {
            _context = context;
            _siteviews = siteViews;
            _apartmentviews = apartmentViews;
        }

        // GET: Apartments
        public async Task<IActionResult> Index(string sortOrder, string searchName, string searchCity, DateTime? startDate, DateTime? endDate, bool myAps = false)
        {
            var apartments = from ap in _context.Apartments.Include(a => a.Reservations) select ap;
            if(myAps)
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "ID");
                apartments = from ap in apartments where ap.ApartmentOwnerID.ToString() == userID.Value select ap;
            }

            //Calculate rating for each apartment
            Dictionary<int, float> rating = new Dictionary<int, float>();
            Dictionary<int, int> reviewCounter = new Dictionary<int, int>();
            foreach (Apartment ap in apartments)
            {
                rating[ap.ID] = 0;
                reviewCounter[ap.ID] = 0;
                foreach (Reservation res in ap.Reservations)
                {
                    if (res.Rating.HasValue)
                    {
                        rating[ap.ID] += res.Rating.Value;
                        reviewCounter[ap.ID] += 1;
                    }
                }
                if (reviewCounter[ap.ID] != 0)
                {
                    rating[ap.ID] /= reviewCounter[ap.ID];
                }
            }
            apartments.ToList().ForEach(ap => ap.AvgRating = rating[ap.ID]);
            _context.SaveChanges();
            ViewData["ApartmentReviewCounter"] = reviewCounter;

            //Sort and search apartments
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["NumOfGuestsSortParm"] = sortOrder == "guests" ? "guests_desc" : "guests";
            ViewData["RoomsSortParm"] = sortOrder == "rooms" ? "rooms_desc" : "rooms";
            ViewData["RatingSortParm"] = sortOrder == "rating" ? "rating_desc" : "rating";
            ViewData["NameFilter"] = searchName;
            ViewData["CityFilter"] = searchCity;
            if(startDate!= null)
            {
                ViewData["StartDateFilter"] = startDate.Value.ToString("yyyy-MM-dd");
            }
            if (endDate != null)
            {
                ViewData["EndDateFilter"] = endDate.Value.ToString("yyyy-MM-dd");
            }

            if (!String.IsNullOrEmpty(searchName))
            {
                apartments = apartments.Where(ap => ap.Title.Contains(searchName) || ap.Description.Contains(searchName));
            }
            if (!String.IsNullOrEmpty(searchCity))
            {
                apartments = apartments.Where(ap => ap.City.Contains(searchCity));
            }
            if (startDate != null && endDate != null)
            {
                apartments = apartments.Where(ap => !(ap.Reservations.FirstOrDefault(r => startDate.Value < r.EndDate) != null
                                                && ap.Reservations.FirstOrDefault(r => endDate.Value > r.StartDate) != null));
            }

            switch (sortOrder)
            {
                case "price":
                    apartments = apartments.OrderBy(ap => ap.PricePerDay);
                    break;
                case "price_desc":
                    apartments = apartments.OrderByDescending(ap => ap.PricePerDay);
                    break;
                case "guests":
                    apartments = apartments.OrderBy(ap => ap.MaxNumOfGuests);
                    break;
                case "guests_desc":
                    apartments = apartments.OrderByDescending(ap => ap.MaxNumOfGuests);
                    break;
                case "rooms":
                    apartments = apartments.OrderBy(ap => ap.NumOfRooms);
                    break;
                case "rooms_desc":
                    apartments = apartments.OrderByDescending(ap => ap.NumOfRooms);
                    break;
                case "rating":
                    apartments = apartments.OrderBy(ap => ap.AvgRating);
                    break;
                case "rating_desc":
                default:
                    apartments = apartments.OrderByDescending(ap => ap.AvgRating);
                    break;
            }

            return View(await apartments.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> MyApartments(string sortOrder, string searchName, string searchCity, bool myAps = true)
        {
            var apartments = from ap in _context.Apartments.Include(a => a.Reservations) select ap; ;
            if (myAps)
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "ID");
                apartments = from ap in apartments where ap.ApartmentOwnerID.ToString() == userID.Value select ap;
            }

            //Calculate rating for each apartment
            Dictionary<int, float> rating = new Dictionary<int, float>();
            Dictionary<int, int> reviewCounter = new Dictionary<int, int>();
            foreach (Apartment ap in apartments)
            {
                rating[ap.ID] = 0;
                reviewCounter[ap.ID] = 0;
                foreach (Reservation res in ap.Reservations)
                {
                    if (res.Rating.HasValue)
                    {
                        rating[ap.ID] += res.Rating.Value;
                        reviewCounter[ap.ID] += 1;
                    }
                }
                if (reviewCounter[ap.ID] != 0)
                {
                    rating[ap.ID] /= reviewCounter[ap.ID];
                }
            }
            apartments.ToList().ForEach(ap => ap.AvgRating = rating[ap.ID]);
            _context.SaveChanges();
            ViewData["ApartmentReviewCounter"] = reviewCounter;

            return View(await apartments.AsNoTracking().ToListAsync());
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            this._siteviews.Increment();
            this._apartmentviews.Increment(id.Value);
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.ApartmentOwner)
                .Include(a=> a.ApartmentsViews)
                .Include(a => a.Reservations)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            ViewData["ApartmentOwnerID"] = new SelectList(_context.Users, "ID", "Email");
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,NumOfRooms,PricePerDay,City,Street,Floor,ApartmentNumber,MaxNumOfGuests,ApartmentOwnerID")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentOwnerID"] = new SelectList(_context.Users, "ID", "Email", apartment.ApartmentOwnerID);
            return View(apartment);
        }

        // GET: Apartments/Create_New
        public IActionResult Create_New()
        {
            ViewData["ApartmentOwnerID"] = new SelectList(_context.Users, "ID", "Email");
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_New([Bind("ID,Title,Description,NumOfRooms,PricePerDay,City,Street,Floor,ApartmentNumber,MaxNumOfGuests,ApartmentOwnerID")] Apartment apartment)
        {
           
            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentOwnerID"] = new SelectList(_context.Users, "ID", "Email", apartment.ApartmentOwnerID);
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }
            ViewData["ApartmentOwnerID"] = new SelectList(_context.Users, "ID", "Email", apartment.ApartmentOwnerID);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,NumOfRooms,PricePerDay,City,Street,Floor,ApartmentNumber,MaxNumOfGuests,ApartmentOwnerID")] Apartment apartment)
        {
            if (id != apartment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentOwnerID"] = new SelectList(_context.Users, "ID", "Email", apartment.ApartmentOwnerID);
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.ApartmentOwner)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartments.Any(e => e.ID == id);
        }

        [Authorize]
        public async Task<IActionResult> MyApartment()
        {
            var userID = User.Claims.FirstOrDefault(c => c.Type == "ID");
            var apartments = from ap in _context.Apartments.Include(a => a.Reservations) where ap.ApartmentOwnerID.ToString() == userID.Value select ap;


            //Calculate rating for each apartment
            return View(await apartments.AsNoTracking().ToListAsync());
        }
    }
}


