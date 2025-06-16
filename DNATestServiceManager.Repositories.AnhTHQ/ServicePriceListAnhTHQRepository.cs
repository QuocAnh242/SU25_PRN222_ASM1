using DNATestServiceManager.Repositories.AnhTHQ.Basic;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestServiceManager.Repositories.AnhTHQ
{
    public class ServicePriceListAnhTHQRepository : GenericRepository<ServicePriceListAnhTHQRepository>
    {
        public ServicePriceListAnhTHQRepository() { }
        public async Task<List<ServicePriceListAnhThq>> GetAllAsync()
        {
            return await _context.ServicePriceListAnhThqs
           .Include(s => s.ServiceAnhThqid)
           .ToListAsync();
        }
    }
}
