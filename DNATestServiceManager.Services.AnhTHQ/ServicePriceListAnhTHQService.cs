using DNATestServiceManager.Repositories.AnhTHQ;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DNATestServiceManager.Services.AnhTHQ
{
    public class ServicePriceListAnhTHQService : IServicePriceListAnhTHQService
    {
        private readonly ServicePriceListAnhTHQRepository _repository;

        public ServicePriceListAnhTHQService()
        {
            _repository ??= new ServicePriceListAnhTHQRepository();
        }

        // Get all
        public async Task<List<ServicePriceListAnhThq>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Get by ID
        public async Task<ServicePriceListAnhThq> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Add
        public async Task AddAsync(ServicePriceListAnhThq item)
        {
            await _repository.AddAsync(item);
        }

        // Update
        public async Task UpdateAsync(ServicePriceListAnhThq item)
        {
            await _repository.UpdateAsync(item);
        }

        // Delete
        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

    }
}
