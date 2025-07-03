using DNATestServiceManager.Repositories.AnhTHQ.DBContext;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using DNATestServiceManager.Services.AnhTHQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesAnhThq
{
    [Authorize(Roles = "1,2,3")]
    public class DetailsModel : PageModel
    {

        private readonly IServicesAnhTHQService _servicesAnhTHQService;

        public DetailsModel(IServicesAnhTHQService servicesAnhTHQService)
        {
            _servicesAnhTHQService = servicesAnhTHQService;
        }

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
    }
}
