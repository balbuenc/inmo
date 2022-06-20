using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAllMessages();

        Task<Message> GetMessageDetails(int id);

        Task<bool> InsertMessage(Message message);

        Task<bool> UpdateMessage(Message message);

        Task<bool> DeleteMessage(int id);
    }
}
