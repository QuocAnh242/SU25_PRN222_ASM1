using DNATestServiceManager.Repositories.AnhTHQ.Basic;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNATestServiceManager.Repositories.AnhTHQ
{
    public class ServicePriceListAnhTHQRepository : GenericRepository<ServicePriceListAnhThq>
    {
        public ServicePriceListAnhTHQRepository() { }

        // Get all with related Service
        public async Task<List<ServicePriceListAnhThq>> GetAllAsync()
        {
            return await _context.ServicePriceListAnhThqs
                .Include(s => s.ServiceAnhThq)
                .ToListAsync();
        }

        // Get by ID
        public async Task<ServicePriceListAnhThq> GetByIdAsync(int id)
        {
            return await _context.ServicePriceListAnhThqs
                .Include(s => s.ServiceAnhThq)
                .FirstOrDefaultAsync(x => x.ServicePriceListAnhThqid == id);
        }

        // Add
        public async Task AddAsync(ServicePriceListAnhThq entity)
        {
            await _context.ServicePriceListAnhThqs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Update
        public async Task UpdateAsync(ServicePriceListAnhThq entity)
        {
            _context.ServicePriceListAnhThqs.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _context.ServicePriceListAnhThqs.FindAsync(id);
            if (entity != null)
            {
                _context.ServicePriceListAnhThqs.Remove(entity);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

    }
}
