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
    public class DetailsModel : PageModel
    {
        private readonly DNATestServiceManager.Repositories.AnhTHQ.DBContext.SU25_PRN222_SE1706_G6_DNATestServiceManagerContext _context;

        public DetailsModel(DNATestServiceManager.Repositories.AnhTHQ.DBContext.SU25_PRN222_SE1706_G6_DNATestServiceManagerContext context)
        {
            _context = context;
        }

        public ServicePriceListAnhThq ServicePriceListAnhThq { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicepricelistanhthq = await _context.ServicePriceListAnhThqs.FirstOrDefaultAsync(m => m.ServicePriceListAnhThqid == id);
            if (servicepricelistanhthq == null)
            {
                return NotFound();
            }
            else
            {
                ServicePriceListAnhThq = servicepricelistanhthq;
            }
            return Page();
        }
    }
}
