using GroundBNB.Data;
using GroundBNB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GroundBNB.Services
{
    public class SiteViewsService:ISiteViewsService
    {
        private readonly SiteContext _context;
        public SiteViewsService(SiteContext _context)
        {
            this._context = _context;
        }

        public void Increment()
        {
            DateTime today = DateTime.Now.Date;
            SiteViews entity = this._context.SiteViews.Where(x => x.Date.Date.Equals(today)).FirstOrDefault();
            if (entity != null)
            {
                entity.Views += 1;
                this._context.Attach<SiteViews>(entity);
                this._context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                entity = new SiteViews
                {
                    Views = 1,
                    Date = today
                };
                this._context.SiteViews.Add(entity);
                this._context.Entry(entity).State = EntityState.Added;
            }

            this._context.SaveChanges();
        }

        public List<SiteViews> GetSiteViews()
        {
            return this._context.SiteViews.ToList();
        }


    }
}
