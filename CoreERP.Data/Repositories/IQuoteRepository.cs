using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IQuoteRepository
    {
        Task<IEnumerable<Quote>> GetAllQuotes();

        Task<Quote> GetQuoteDetails(int id);

        Task<bool> InsertQuote(Quote quote);

        Task<bool> UpdateQuote(Quote quote);

        Task<bool> DeleteQuote(int id);
    }
}
