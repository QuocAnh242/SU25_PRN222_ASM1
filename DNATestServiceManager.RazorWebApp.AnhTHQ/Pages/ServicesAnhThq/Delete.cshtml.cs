using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DNATestServiceManager.Repositories.AnhTHQ.DBContext;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using DNATestServiceManager.Services.AnhTHQ;
using Microsoft.AspNetCore.Authorization;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesAnhThq
{
    [Authorize(Roles = "1")]
    public class DeleteModel : PageModel
    {
        private readonly IServicesAnhTHQService _servicesAnhTHQService;

        public DeleteModel(IServicesAnhTHQService servicesAnhTHQService)
        {
            _servicesAnhTHQService = servicesAnhTHQService;
        }

        [BindProperty]
        public ServiceAnhThq ServicesAnhThq { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicesanhthq = await _servicesAnhTHQService.GetByIdAsync(id);

            if (servicesanhthq == null)
            {
                return NotFound();
            }
            else
            {
                ServicesAnhThq = servicesanhthq;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicesanhthq = await _servicesAnhTHQService.GetByIdAsync(id);
            if (servicesanhthq != null)
            {
                ServicesAnhThq = servicesanhthq;
                await _servicesAnhTHQService.DeleteAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
