using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IQuoteService
    {
        Task<IEnumerable<Quote>> GetAllQuotes();

        Task<Quote> GetQuoteDetails(int id);

        Task SaveQuote(Quote quote);


        Task DeleteQuote(int id);
    }
}
