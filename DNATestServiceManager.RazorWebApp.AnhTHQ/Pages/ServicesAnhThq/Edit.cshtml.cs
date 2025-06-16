using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DNATestServiceManager.Repositories.AnhTHQ.DBContext;
using DNATestServiceManager.Repositories.AnhTHQ.Models;

namespace DNATestServiceManager.RazorWebApp.AnhTHQ.Pages.ServicesAnhThq
{
    public class EditModel : PageModel
    {
        private readonly SU25_PRN222_SE1706_G6_DNATestServiceManagerContext _context;

        public EditModel(SU25_PRN222_SE1706_G6_DNATestServiceManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ServiceAnhThq ServicesAnhThq { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            ServicesAnhThq = await _context.ServicesAnhThqs.FirstOrDefaultAsync(m => m.ServiceAnhThqid == id);

            if (ServicesAnhThq == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Attach(ServicesAnhThq).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicesAnhThqExists(ServicesAnhThq.ServiceAnhThqid))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }

        private bool ServicesAnhThqExists(int id)
        {
            return _context.ServicesAnhThqs.Any(e => e.ServiceAnhThqid == id);
        }
    }
}
