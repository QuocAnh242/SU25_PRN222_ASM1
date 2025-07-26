using DNATestServiceManager.Repositories.AnhTHQ.Models;
using DNATestServiceManager.Services.AnhTHQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesListPriceAnhThq
{
    [Authorize(Roles = "1")]
    public class CreateModel : PageModel
    {
        private readonly IServicePriceListAnhTHQService _servicePriceListAnhThqService;
        private readonly IServicesAnhTHQService _servicesAnhTHQService;

        public CreateModel(
            IServicePriceListAnhTHQService servicePriceListAnhThqService,
            IServicesAnhTHQService servicesAnhTHQService)
        {
            _servicePriceListAnhThqService = servicePriceListAnhThqService;
            _servicesAnhTHQService = servicesAnhTHQService;
        }

        [BindProperty]
        public ServicePriceListAnhThq ServicePriceListAnhThq { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadServiceListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadServiceListAsync(); 
                return Page();
            }

            await _servicePriceListAnhThqService.AddAsync(ServicePriceListAnhThq);
            return RedirectToPage("./Index");
        }

        private async Task LoadServiceListAsync()
        {
            var services = await _servicesAnhTHQService.GetAllAsync();

            ViewData["ServiceAnhThqid"] = new SelectList(
                services,
                nameof(ServiceAnhThq.ServiceAnhThqid),
                nameof(ServiceAnhThq.ServiceName)
            );
        }
    }
}
