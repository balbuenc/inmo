using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    interface IAreaService
    {
        Task<IEnumerable<Area>> GetAllAreas();

        Task<Area> GetAreaDetails(int id);

        Task SaveArea(Area area);


        Task DeleteArea(int id);
    }
}
