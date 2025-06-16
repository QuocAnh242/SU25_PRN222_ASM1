using DNATestServiceManager.Repositories.AnhTHQ;
using DNATestServiceManager.Repositories.AnhTHQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestServiceManager.Services.AnhTHQ
{
    public class SystemUserAccountService
    {
        private readonly SystemUserAccountRepository _repository;
        public SystemUserAccountService() => _repository = new SystemUserAccountRepository();

        public async Task<UserAccount> GetUserAccountAsync(string userName, string password)
        {
            return await _repository.GetUserAccountAsync(userName, password);
        }


    }
}
