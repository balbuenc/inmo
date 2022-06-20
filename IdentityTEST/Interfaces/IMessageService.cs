using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetAllMessages();

        Task<Message> GetMessageDetails(int id);

        Task SaveMessage(Message message);


        Task DeleteMessage(int id);
    }
}
