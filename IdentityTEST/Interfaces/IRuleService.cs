using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IRuleService
    {
        Task<IEnumerable<Rule>> GetAllRules();

        Task<Rule> GetRuleDetails(int id);

        Task SaveRule(Rule rule);


        Task DeleteRule(int id);
    }
}
