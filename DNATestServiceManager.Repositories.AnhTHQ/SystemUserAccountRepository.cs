using DNATestServiceManager.Repositories.AnhTHQ.Basic;
using DNATestServiceManager.Repositories.AnhTHQ.DBContext;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestServiceManager.Repositories.AnhTHQ
{
    public class SystemUserAccountRepository : GenericRepository<UserAccount>
    {
        public SystemUserAccountRepository() { }
        public SystemUserAccountRepository(SU25_PRN222_SE1706_G6_DNATestServiceManagerContext context) => _context= context;
    
        public async Task<UserAccount> GetUserAccountAsync(String username, string password)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(
                u=> u.UserName == username && u.Password== password);
        }
    }
}
