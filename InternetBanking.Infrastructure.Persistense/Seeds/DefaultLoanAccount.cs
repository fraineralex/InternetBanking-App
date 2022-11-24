using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Infrastructure.Persistence.Seeds

{
    public static class DefaultLoanAccount
    {
        public static async Task SeedAsync(ITypeAccountRepository _typeAccountRepository)
        {

            TypeAccount account = new();
            account.Name = "Préstamo";

            var accounts = await _typeAccountRepository.GetAllAsync();

            if (accounts.All(e => e.Name != account.Name))
            {
                var newAccount = await _typeAccountRepository.AddAsync(account);
            }
        }
    }
}
