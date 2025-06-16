using DNATestServiceManager.Repositories.AnhTHQ.Models;

namespace DNATestServiceManager.Services.AnhTHQ
{
    public interface IServicesAnhTHQService
    {
        Task<List<ServiceAnhThq>> GetAllAsync();
        Task<ServiceAnhThq> GetByIdAsync(int id);
        Task<List<ServiceAnhThq>> SearchAsync(string serviceName, string serviceType, string category);
        Task<int> CreateAsync(ServiceAnhThq servicesAnhThq);
        Task<int> UpdateAsync(ServiceAnhThq serviceAnhThq);
        Task<int> DeleteAsync(int id);
    }
}