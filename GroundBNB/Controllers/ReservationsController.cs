﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroundBNB.Data;
using GroundBNB.Models;
using Microsoft.AspNetCore.Authorization;
using GroundBNB.Services;

namespace GroundBNB.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly SiteContext _context;
        private readonly ISiteViewsService _siteviews;

        public ReservationsController(SiteContext context, ISiteViewsService siteViews)
        {
            _context = context;
            _siteviews = siteViews;
        }

        // GET: Reservations

        public async Task<IActionResult> Index()
        {
            this._siteviews.Increment();

            if (User.IsInRole("Admin"))
            {
                var siteContext = _context.Reservations.Include(r => r.Apartment).Include(r => r.Guest);
                return View(await siteContext.ToListAsync());
            }
            else if (User.Identity.IsAuthenticated)
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "ID");
                var reservations = from res in _context.Reservations where res.GuestID.ToString() == userID.Value select res;
                return View(await reservations.ToListAsync());

            }
            else
            {
                return Redirect("/login");
            }
        }


        // GET: Reservations/Create
        public IActionResult Create(DateTime? startDate, DateTime? endDate, int apID)
        {
            ViewData["ApartmentID"] = new SelectList(_context.Apartments, "ID", "City");
            ViewData["GuestID"] = new SelectList(_context.Users, "ID", "Email");
            if (startDate != null)
            {
                ViewData["StartDateFilter"] = startDate.Value.ToString("yyyy-MM-dd");
            }
            if (endDate != null)
            {
                ViewData["EndDateFilter"] = endDate.Value.ToString("yyyy-MM-dd");
            }
            ViewData["ApartmentID"] = apID;
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Rating,Review,NumberOfGuests,StartDate,EndDate,PurchseDate,ApartmentID,GuestID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentID"] = new SelectList(_context.Apartments, "ID", "City", reservation.ApartmentID);
            ViewData["GuestID"] = new SelectList(_context.Users, "ID", "Email", reservation.GuestID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["ApartmentID"] = new SelectList(_context.Apartments, "ID", "City", reservation.ApartmentID);
            ViewData["GuestID"] = new SelectList(_context.Users, "ID", "Email", reservation.GuestID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Rating,Review,NumberOfGuests,StartDate,EndDate,PurchseDate,ApartmentID,GuestID")] Reservation reservation)
        {
            if (id != reservation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ID))
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
            ViewData["ApartmentID"] = new SelectList(_context.Apartments, "ID", "City", reservation.ApartmentID);
            ViewData["GuestID"] = new SelectList(_context.Users, "ID", "Email", reservation.GuestID);
            return View(reservation);
        }
        public async Task<IActionResult> RateReservation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["ApartmentID"] = new SelectList(_context.Apartments, "ID", "City", reservation.ApartmentID);
            ViewData["GuestID"] = new SelectList(_context.Users, "ID", "Email", reservation.GuestID);
            return View(reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RateReservation(int id, Reservation reservation)
        {
            var originalRes = await _context.Reservations.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    originalRes.Rating = reservation.Rating;
                    originalRes.Review = reservation.Review;
                    _context.Update(originalRes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MyReservation));
            }
            ViewData["ApartmentID"] = new SelectList(_context.Apartments, "ID", "City", reservation.ApartmentID);
            ViewData["GuestID"] = new SelectList(_context.Users, "ID", "Email", reservation.GuestID);
            return View(reservation);
        }

        public async Task<IActionResult> ReservationsForMyApartments()
        {
            //this._siteviews.Increment(); is this needed?

            var userID = User.Claims.FirstOrDefault(c => c.Type == "ID");
            //.ThenInclude(ap => ap.ApartmentOwner)
            var reservations = from res in _context.Reservations.Include(res => res.Guest).Include(res => res.Apartment) where res.Apartment.ApartmentOwnerID.ToString() == userID.Value select res;
            return View(await reservations.ToListAsync());

        }
        // GET: Reservations/Delete/5
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Apartment)
                .Include(r => r.Guest)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }


        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> MyReservation(string searchAp, string searchCity, DateTime? date, bool showPastRes = false)
        {
            var userID = User.Claims.FirstOrDefault(c => c.Type == "ID");
            var reservations = from res in _context.Reservations.Include(a => a.Apartment) where res.GuestID.ToString() == userID.Value select res;

            ViewData["ApFilter"] = searchAp;
            ViewData["CityFilter"] = searchCity;
            ViewData["PastRes"] = showPastRes;
            if (date != null)
            {
                ViewData["DateFilter"] = date.Value.ToString("yyyy-MM-dd");
            }

            if (!String.IsNullOrEmpty(searchAp))
            {
                reservations = reservations.Where(res => res.Apartment.Title.Contains(searchAp) || res.Apartment.Description.Contains(searchAp));
            }
            if (!String.IsNullOrEmpty(searchCity))
            {
                reservations = reservations.Where(res => res.Apartment.City.Contains(searchCity));
            }
            if (date != null)
            {
                reservations = reservations.Where(res => (res.StartDate <= date) && (res.EndDate >= date));
            }
            reservations = reservations.OrderBy(res => res.StartDate);

            if (!showPastRes)
            {
                reservations = reservations.Where(res => res.EndDate > DateTime.Now);
            }
            return View(await reservations.AsNoTracking().ToListAsync());
        }


        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ID == id);
        }
    }
}
