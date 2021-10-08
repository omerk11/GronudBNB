using GroundBNB.Data;
using GroundBNB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Services
{
    public class ApartmentViewsService:IApartmentViewsService
    {
        private readonly SiteContext _context;

        public ApartmentViewsService(SiteContext _context)
        {
            this._context = _context;
        }

        public void Increment(int id)
        {
            DateTime today = DateTime.Now.Date;
            ApartmentViews entity = this._context.ApartmentViews.Where(x => x.Date.Date.Equals(today) && x.ApartmentID == id).FirstOrDefault();
            if (entity != null)
            {
                entity.Views += 1;
                this._context.Attach<ApartmentViews>(entity);
                this._context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                entity = new ApartmentViews
                {
                    Views = 1,
                    Date = today,
                    ApartmentID = id
                };
                this._context.ApartmentViews.Add(entity);
                this._context.Entry(entity).State = EntityState.Added;
            }

            this._context.SaveChanges();
        }

        public List<ApartmentViews> GetApartmentsViews(int id)
        {
            return this._context.ApartmentViews.Where(x => x.ApartmentID == id).ToList();
        }

    }

}
