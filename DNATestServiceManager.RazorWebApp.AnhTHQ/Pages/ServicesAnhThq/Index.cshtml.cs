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
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IServicesAnhTHQService _serviceAnhThq;

        public IndexModel(IServicesAnhTHQService serviceAnhThq)
        {
            _serviceAnhThq = serviceAnhThq;
        }

        public IList<ServiceAnhThq> ServicesAnhThq { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ServicesAnhThq = await _serviceAnhThq.GetAllAsync();
        }
    }
}
