using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface ISendMessageLogRepository
    {
        Task<IEnumerable<SendMessageLog>> GetAllSendMessageLogs();

        Task<SendMessageLog> GetSendMessageLogDetails(int id);

        Task<bool> InsertSendMessageLog(SendMessageLog message_log);

        Task<bool> UpdateSendMessageLog(SendMessageLog message_log);

        Task<bool> DeleteSendMessageLog(int id);
    }
}
