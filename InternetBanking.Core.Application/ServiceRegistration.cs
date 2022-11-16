using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application
{
    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //#region Services
            //services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            //services.AddTransient<IProductService, ProductService>();
            //services.AddTransient<ICategoryService, CategoryService>();
            //services.AddTransient<IUserService, UserService>();
            //#endregion
        }
    }
}
