using DNATestServiceManager.Repositories.AnhTHQ.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DNATestServiceManager.Services.AnhTHQ
{
    public interface IServicePriceListAnhTHQService
    {
        Task<List<ServicePriceListAnhThq>> GetAllAsync();
        Task<ServicePriceListAnhThq> GetByIdAsync(int id);
        Task AddAsync(ServicePriceListAnhThq item);
        Task UpdateAsync(ServicePriceListAnhThq item);
        Task<int> DeleteAsync(int id);
    }
}
