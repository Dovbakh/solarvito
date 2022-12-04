using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Solarvito.AppServices.Advertisement.Repositories;
using Solarvito.AppServices.Advertisement.Services;
using Solarvito.AppServices.Category.Repositories;
using Solarvito.AppServices.Category.Services;
using Solarvito.AppServices.User.Repositories;
using Solarvito.AppServices.User.Services;
using Solarvito.AppServices.Services;
using Solarvito.DataAccess;
using Solarvito.DataAccess.EntityConfigurations.Advertisement;
using Solarvito.DataAccess.EntityConfigurations.Category;
using Solarvito.DataAccess.EntityConfigurations.User;
using Solarvito.DataAccess.Interfaces;
using Solarvito.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Registrar
{
    public static class SolarvitoRegistrar
    {
        public static IServiceCollection AddServiceRegistrationModule(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeService, DateTimeService>();

            services.AddSingleton<IDbContextOptionsConfigurator<SolarvitoContext>, ShoppingCartContextConfiguration>();

            services.AddDbContext<SolarvitoContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
                ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<SolarvitoContext>>()
                   .Configure((DbContextOptionsBuilder<SolarvitoContext>)dbOptions)));

            services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<SolarvitoContext>()));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IAdvertisementService, AdvertisementService>();
            services.AddTransient<IAdvertisementRepository, AdvertisementRepository>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            //services.AddTransient<IProductService, ProductService>();
            //services.AddTransient<IProductRepository, ProductRepository>();

            //services.AddTransient<IShoppingCartService, ShoppingCartService>();
            //services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();

            //services.AddScoped<IClaimsAccessor, HttpContextClaimsAccessor>();

            return services;
        }
    }
}
