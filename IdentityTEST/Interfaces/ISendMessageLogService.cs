using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface ISendMessageLogService
    {
        Task<IEnumerable<SendMessageLog>> GetAllSendMessageLogs();

        Task<SendMessageLog> GetSendMessageLogDetails(int id);

        Task SaveSendMessageLog(SendMessageLog message_log);


        Task DeleteSendMessageLog(int id);
    }
}
