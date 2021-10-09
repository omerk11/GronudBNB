using GroundBNB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Services
{
    public interface IApartmentViewsService
    {
        List<ApartmentViews> GetApartmentsViews(int id);
        void Increment(int id);
    }
}
