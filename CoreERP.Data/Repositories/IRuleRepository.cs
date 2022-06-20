using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IRuleRepository
    {
        Task<IEnumerable<Rule>> GetAllRules();

        Task<Rule> GetRuleDetails(int id);

        Task<bool> InsertRule(Rule rule);

        Task<bool> UpdateRule(Rule rule);

        Task<bool> DeleteRule(int id);
    }
}
