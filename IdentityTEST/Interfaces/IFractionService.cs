using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IFractionService
    {
        Task<IEnumerable<Fraction>> GetAllFractions();

        Task<Fraction> GetFractionDetails(int id);

        Task SaveFraction(Fraction fraction);


        Task DeleteFraction(int id);
    }
}
