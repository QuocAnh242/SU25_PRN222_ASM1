using DNATestServiceManager.Repositories.AnhTHQ.Models;
using DNATestServiceManager.Services.AnhTHQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesAnhThq
{
    [Authorize(Roles = "1,2")]
    public class EditModel : PageModel
    {
        private readonly IServicesAnhTHQService _servicesAnhTHQService;

        public EditModel(IServicesAnhTHQService servicesAnhTHQService)
        {
            _servicesAnhTHQService = servicesAnhTHQService;
        }

        [BindProperty]
        public ServiceAnhThq ServicesAnhThq { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var service = await _servicesAnhTHQService.GetByIdAsync(id.Value);

            if (service == null || service.ServiceAnhThqid == 0)
                return NotFound();

            ServicesAnhThq = service;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _servicesAnhTHQService.UpdateAsync(ServicesAnhThq);

            if (result == 0)
                return NotFound();

            return RedirectToPage("./Index");
        }
    }
}
