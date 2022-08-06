using LIUMarketplace.UI.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.UI.Service
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            return services.AddScoped<IAuthenticationService, HttpAuthenticaionService>()
                           .AddScoped<IProductService, HttpProductService>()
                           .AddScoped<ICategoryService, HttpCategoryService>()
                           .AddScoped<IUserService, HttpUserService>()
                           .AddScoped<ICartService, HttpCartService>()
                           .AddScoped<IFavoriteService, HttpFavoriteService>()
                           ;
                           

        }
    }
}
