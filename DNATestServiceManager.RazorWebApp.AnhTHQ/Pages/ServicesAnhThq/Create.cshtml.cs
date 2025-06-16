using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNATestServiceManager.Repositories.AnhTHQ.DBContext;
using DNATestServiceManager.Repositories.AnhTHQ.Models;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesAnhThq
{
    public class CreateModel : PageModel
    {
        private readonly DNATestServiceManager.Repositories.AnhTHQ.DBContext.SU25_PRN222_SE1706_G6_DNATestServiceManagerContext _context;

        public CreateModel(DNATestServiceManager.Repositories.AnhTHQ.DBContext.SU25_PRN222_SE1706_G6_DNATestServiceManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ServiceAnhThq ServicesAnhThq { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ServicesAnhThqs.Add(ServicesAnhThq);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
