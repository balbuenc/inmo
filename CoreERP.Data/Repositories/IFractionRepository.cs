using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IFractionRepository
    {
        Task<IEnumerable<Fraction>> GetAllFractions();

        Task<Fraction> GetFractionDetails(int id);

        Task<bool> InsertFraction(Fraction fraction);

        Task<bool> UpdateFraction(Fraction fraction);

        Task<bool> DeleteFraction(int id);
    }
}
