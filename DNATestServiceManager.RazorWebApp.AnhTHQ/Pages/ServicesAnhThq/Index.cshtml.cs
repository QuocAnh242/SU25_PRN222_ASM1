using DNATestServiceManager.Repositories.AnhTHQ.Models;
using DNATestServiceManager.Services.AnhTHQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesAnhThq
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IServicesAnhTHQService _serviceAnhThq;

        public IndexModel(IServicesAnhTHQService serviceAnhThq)
        {
            _serviceAnhThq = serviceAnhThq;
        }

        public IList<ServiceAnhThq> ServicesAnhThq { get; set; } = new List<ServiceAnhThq>();

        [BindProperty(SupportsGet = true)]
        public string ServiceName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ServiceType { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? BasePrice { get; set; }

        public async Task OnGetAsync()
        {
            ServicesAnhThq = await _serviceAnhThq.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ServicesAnhThq = await _serviceAnhThq.SearchAsync(ServiceName, ServiceType, BasePrice);
            return Page();
        }
    }
}
