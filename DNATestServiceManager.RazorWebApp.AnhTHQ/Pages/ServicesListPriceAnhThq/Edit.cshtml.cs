using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DNATestServiceManager.Repositories.AnhTHQ.DBContext;
using DNATestServiceManager.Repositories.AnhTHQ.Models;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesListPriceAnhThq
{
    public class EditModel : PageModel
    {
        private readonly DNATestServiceManager.Repositories.AnhTHQ.DBContext.SU25_PRN222_SE1706_G6_DNATestServiceManagerContext _context;

        public EditModel(DNATestServiceManager.Repositories.AnhTHQ.DBContext.SU25_PRN222_SE1706_G6_DNATestServiceManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ServicePriceListAnhThq ServicePriceListAnhThq { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicepricelistanhthq =  await _context.ServicePriceListAnhThqs.FirstOrDefaultAsync(m => m.ServicePriceListAnhThqid == id);
            if (servicepricelistanhthq == null)
            {
                return NotFound();
            }
            ServicePriceListAnhThq = servicepricelistanhthq;
           ViewData["ServiceAnhThqid"] = new SelectList(_context.ServicesAnhThqs, "ServiceAnhThqid", "Category");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ServicePriceListAnhThq).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicePriceListAnhThqExists(ServicePriceListAnhThq.ServicePriceListAnhThqid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ServicePriceListAnhThqExists(int id)
        {
            return _context.ServicePriceListAnhThqs.Any(e => e.ServicePriceListAnhThqid == id);
        }
    }
}
