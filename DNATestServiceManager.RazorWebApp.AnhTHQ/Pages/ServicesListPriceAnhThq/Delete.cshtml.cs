using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using DNATestServiceManager.Services.AnhTHQ;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesListPriceAnhThq
{
    public class DeleteModel : PageModel
    {
        private readonly IServicePriceListAnhTHQService _service;

        public DeleteModel(IServicePriceListAnhTHQService service)
        {
            _service = service;
        }

        [BindProperty]
        public ServicePriceListAnhThq ServicePriceListAnhThq { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var priceItem = await _service.GetByIdAsync(id.Value);
            if (priceItem == null)
                return NotFound();

            ServicePriceListAnhThq = priceItem;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            int deletedCount = await _service.DeleteAsync(id.Value);
            if (deletedCount == 0)
                return NotFound(); // Không xóa được

            return RedirectToPage("./Index");
        }
    }
}
