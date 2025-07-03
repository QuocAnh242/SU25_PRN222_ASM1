using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using DNATestServiceManager.Services.AnhTHQ;
using Microsoft.AspNetCore.Authorization;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesAnhThq
{
    [Authorize(Roles = "1")]
    public class DeleteModel : PageModel
    {
        private readonly IServicesAnhTHQService _servicesAnhTHQService;
        private readonly IServicePriceListAnhTHQService _servicePriceListAnhTHQService;

        public DeleteModel(
            IServicesAnhTHQService servicesAnhTHQService,
            IServicePriceListAnhTHQService servicePriceListAnhTHQService)
        {
            _servicesAnhTHQService = servicesAnhTHQService;
            _servicePriceListAnhTHQService = servicePriceListAnhTHQService;
        }

        [BindProperty]
        public ServiceAnhThq ServicesAnhThq { get; set; } = default!;

        public int RelatedPriceCount { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var servicesanhthq = await _servicesAnhTHQService.GetByIdAsync(id);
            if (servicesanhthq == null) return NotFound();

            ServicesAnhThq = servicesanhthq;

            // Kiểm tra xem có ServicePriceList liên quan không
            var relatedPrices = await _servicePriceListAnhTHQService.GetAllAsync();
            RelatedPriceCount = relatedPrices.Count(x => x.ServiceAnhThqid == id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var relatedPrices = await _servicePriceListAnhTHQService.GetAllAsync();
            var count = relatedPrices.Count(x => x.ServiceAnhThqid == id);

            if (count > 0)
            {
                ServicesAnhThq = await _servicesAnhTHQService.GetByIdAsync(id);
                RelatedPriceCount = count;
                ModelState.AddModelError(string.Empty, $"Không thể xóa vì còn {count} giá dịch vụ liên quan.");
                return Page();
            }

            await _servicesAnhTHQService.DeleteAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
