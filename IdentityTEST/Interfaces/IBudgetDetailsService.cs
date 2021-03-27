using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IBudgetDetailsService
    {
        Task<IEnumerable<BudgetDetails>> GetAllBudgetDetails();

        Task<BudgetDetails> GetBudgetDetails(int id);

        Task SaveBudgetDetails(BudgetDetails budget);


        Task DeleteBudgetDetails(int id);
    }
}
