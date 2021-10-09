using GroundBNB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Services
{
    public interface ISiteViewsService
    {
        void Increment();
        List<SiteViews> GetSiteViews();
    }
}
