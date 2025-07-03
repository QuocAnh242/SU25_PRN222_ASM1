using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DNATestServiceManager.Repositories.AnhTHQ.DBContext;
using DNATestServiceManager.Repositories.AnhTHQ.Models;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesListPriceAnhThq
{
    public class IndexModel : PageModel
    {
        private readonly DNATestServiceManager.Repositories.AnhTHQ.DBContext.SU25_PRN222_SE1706_G6_DNATestServiceManagerContext _context;

        public IndexModel(DNATestServiceManager.Repositories.AnhTHQ.DBContext.SU25_PRN222_SE1706_G6_DNATestServiceManagerContext context)
        {
            _context = context;
        }

        public IList<ServicePriceListAnhThq> ServicePriceListAnhThq { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ServicePriceListAnhThq = await _context.ServicePriceListAnhThqs
                .Include(s => s.ServiceAnhThq).ToListAsync();
        }
    }
}
