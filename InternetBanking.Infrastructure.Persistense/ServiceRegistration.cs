using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Infrastructure.Persistence.Repositories;

namespace InternetBanking.Infrastructure.Persistence
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ILoansRepository, LoansRepository>();
            services.AddTransient<IPaymentsRepository, PaymentsRepository>();
            services.AddTransient<IBeneficiariesRepository, BeneficiariesRepository>();
            services.AddTransient<ISavingsAccountsRepository, SavingsAccountsRepository>();
            services.AddTransient<ICreditCardsRepository, CreditCardsRepository>();
            services.AddTransient<IPersonalTransfersRepository, PersonalTransfersRepository>();
            services.AddTransient<ICashAdvancesRepository, CashAdvancesRepository>();
            #endregion
        }
    }
}
