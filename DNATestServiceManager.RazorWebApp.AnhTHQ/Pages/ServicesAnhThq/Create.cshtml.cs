using DNATestServiceManager.Repositories.AnhTHQ.Models;
using DNATestServiceManager.Services.AnhTHQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesAnhThq
{
    [Authorize(Roles = "1")]
    public class CreateModel : PageModel
    {
        private readonly IServicesAnhTHQService _servicesAnhTHQService;

        public CreateModel(IServicesAnhTHQService servicesAnhTHQService)
        {
            _servicesAnhTHQService = servicesAnhTHQService;
        }

        [BindProperty]
        public ServiceAnhThq ServicesAnhThq { get; set; } = default!;

        public void OnGet()
        {
            var userName = User.Identity?.Name ?? "Unknown";

            ServicesAnhThq = new ServiceAnhThq
            {
                CreatedBy = userName,
                ModifiedBy = userName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userName = User.Identity?.Name ?? "Unknown";
            ServicesAnhThq.CreatedBy = userName;
            ServicesAnhThq.ModifiedBy = userName;
            ServicesAnhThq.CreatedDate = DateTime.Now;
            ServicesAnhThq.ModifiedDate = DateTime.Now;

            await _servicesAnhTHQService.CreateAsync(ServicesAnhThq);

            return RedirectToPage("./Index");
        }
    }
}
