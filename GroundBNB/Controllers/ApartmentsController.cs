using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroundBNB.Data;
using GroundBNB.Models;

namespace GroundBNB.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly SiteContext _context;

        public ApartmentsController(SiteContext context)
        {
            _context = context;
        }

        // GET: Apartments
        public async Task<IActionResult> Index(string sortOrder)
        {
            var apartments = from ap in _context.Apartments.Include(a => a.Reservations) select ap;
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
            apartments = apartments.OrderByDescending(ap => ap.AvgRating);
            return View(await apartments.AsNoTracking().ToListAsync());
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
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
    }
}
