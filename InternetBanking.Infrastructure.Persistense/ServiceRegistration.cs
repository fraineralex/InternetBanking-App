using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBanking.Infrastructure.Persistence
{
    //Main reason for creating this class is to follow the Single responsability
    public static class ServiceRegistration
    {
        // Extension methods | "Decorator"
        // This allows us to extend and create new functionallity following "Open-Closed Principle"
        public static void AddPersistanceInfrastructure(this IServiceCollection service, IConfiguration config)
        {
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                service.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                service.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }

            #region 'repositories'

            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<ITypeAccountRepository, TypeAccountRepository>();
            service.AddTransient<IBeneficiaryRepository, RecipientRepository>();
            service.AddTransient<IPaymentRepository, PaymentRepository>();

            #endregion
        }
    }
}
