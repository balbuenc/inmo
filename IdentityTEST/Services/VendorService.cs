using CoreERP.Model;
using CoreERP.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreERP.UI.Services
{
    public class VendorService : IVendorService
    {
        private readonly HttpClient _httpClient;

        public VendorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task DeleteVendor(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vendor>> GetAllVendors()
        {
            throw new NotImplementedException();
        }

        public Task<Vendor> GetVendorDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveVendor(Vendor vendor)
        {
            throw new NotImplementedException();
        }
    }
}
