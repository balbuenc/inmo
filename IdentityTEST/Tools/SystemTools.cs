using CoreERP.Model;
using CoreERP.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreERP.UI.Tools
{
    public class SystemTools
    {

        public async Task<string> GetSystemParam(string param)
        {
            //Get CoreERP Configuration from database
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44342");
            Configuration configuration;

            ConfigurationService configurationService = new ConfigurationService(httpClient);
            try
            {
                configuration = await configurationService.GetConfigurationDetails(param);

                return configuration.valor;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
