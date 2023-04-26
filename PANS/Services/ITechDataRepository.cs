using PANS.Entities;
using System.Collections.Generic;

namespace PANS.Services
{
    internal interface ITechDataRepository
    {
        List<TechData> GetData();
    }
}