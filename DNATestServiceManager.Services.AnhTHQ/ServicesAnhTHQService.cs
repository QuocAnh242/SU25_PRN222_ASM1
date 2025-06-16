using DNATestServiceManager.Repositories.AnhTHQ;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestServiceManager.Services.AnhTHQ
{
    public class ServicesAnhTHQService : IServicesAnhTHQService
    {
        private readonly ServicesAnhTHQRepository _repository;

        public ServicesAnhTHQService() => _repository ??= new
            ServicesAnhTHQRepository();

        public async Task<List<ServiceAnhThq>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ServiceAnhThq> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<ServiceAnhThq>> SearchAsync(string serviceName, string serviceType, string category)
        {
            return await _repository.SearchAsync(serviceName, serviceType, category);
        }

        public async Task<int> CreateAsync(ServiceAnhThq servicesAnhThq)
        {
            return await _repository.CreateAsync(servicesAnhThq);
        }

        public async Task<int> UpdateAsync(ServiceAnhThq serviceAnhThq)
        {
            return await _repository.UpdateAsync(serviceAnhThq);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

    }
}
