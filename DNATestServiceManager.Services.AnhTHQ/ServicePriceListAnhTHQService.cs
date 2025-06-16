using DNATestServiceManager.Repositories.AnhTHQ;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestServiceManager.Services.AnhTHQ
{
    public class ServicePriceListAnhTHQService 
    {
        private readonly ServicePriceListAnhTHQRepository _repository;

        public ServicePriceListAnhTHQService() => _repository ??= new
            ServicePriceListAnhTHQRepository();

        public async Task<List<ServicePriceListAnhThq>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        } 
    }
}
