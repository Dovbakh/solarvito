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
using Solarvito.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using Solarvito.AppServices.User.Validators;
using Microsoft.AspNetCore.Identity;
using Solarvito.Contracts.User;
using Solarvito.AppServices.User.Additional;
using Solarvito.Contracts.Advertisement;
using Solarvito.AppServices.Advertisement.Validators;
using Solarvito.Infrastructure.ObjectStorage;
using Minio;
using Solarvito.AppServices.File.Services;
using Solarvito.AppServices.File.Repositories;
using Solarvito.DataAccess.EntityConfigurations.File;
using Solarvito.AppServices.AdvertisementImage.Repositories;
using Solarvito.DataAccess.EntityConfigurations.AdvertisementImage;

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

            services.AddScoped<IClaimsAccessor, HttpContextClaimsAccessor>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();


            services.AddScoped<IValidator<UserCredentialsDto>, UserValidator>();
            services.AddScoped<IValidator<AdvertisementRequestDto>, AdvertisementValidator>();

            services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));


            services.AddScoped<IObjectStorage, MinioStorage>();

            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IFileRepository, FileRepository>();

            services.AddTransient<IAdvertisementImageRepository, AdvertisementImageRepository>();

            //services.AddScoped<IBucketOperations>(mc => new MinioClient().WithEndpoint("127.0.0.1:9000").WithCredentials("7d0OSrPdBpU0Iabo", "53jc7XIX5o5BfqxhE2ToCBJQcZCiu80f").Build());
            //services.AddScoped((Func<IMinioClient, MinioClient>)(mc => mc.WithEndpoint("127.0.0.1:9000").WithCredentials("7d0OSrPdBpU0Iabo", "53jc7XIX5o5BfqxhE2ToCBJQcZCiu80f").Build()));

            return services;
        }
    }
}
