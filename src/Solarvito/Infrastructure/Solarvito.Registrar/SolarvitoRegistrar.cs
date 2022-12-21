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
using Solarvito.Contracts.Comment;
using Solarvito.AppServices.Comment.Helpers;
using Solarvito.AppServices.User.Helpers;
using Solarvito.AppServices.Notifier.Services;

namespace Solarvito.Registrar
{
    public static class SolarvitoRegistrar
    {
        public static IServiceCollection AddServiceRegistrationModule(this IServiceCollection services)
        {                   
            // Регистрация сервисов работы с БД
            services.AddSingleton<IDbContextOptionsConfigurator<SolarvitoContext>, ShoppingCartContextConfiguration>();
            services.AddDbContext<SolarvitoContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
                ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<SolarvitoContext>>()
                   .Configure((DbContextOptionsBuilder<SolarvitoContext>)dbOptions)));
            services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<SolarvitoContext>()));

            // Регистрация сервисов работы с обьектным хранилищем
            services.AddScoped<IObjectStorage, MinioStorage>();


            // Регистрация репозиториев
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ICachedRepository<>), typeof(CachedRepository<>));
            services.AddTransient<IAdvertisementRepository, AdvertisementRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IAdvertisementImageRepository, AdvertisementImageRepository>();


            // Регистрация application-сервисов
            services.AddTransient<IAdvertisementService, AdvertisementService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<INotifierService, EmailService>();

            // Регистрация вспомогательных сервисов
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddTransient(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            services.AddScoped<IClaimsAccessor, HttpContextClaimsAccessor>();
            services.AddTransient<IValidator<UserRegisterDto>, UserRegisterValidator>();
            services.AddTransient<IValidator<UserLoginDto>, UserLoginValidator>();
            services.AddTransient<IValidator<UserChangePasswordDto>, UserPasswordValidator>();
            services.AddTransient<IValidator<AdvertisementRequestDto>, AdvertisementValidator>();
            services.AddTransient<IValidator<AdvertisementUpdateRequestDto>, AdvertisementUpdateValidator>();
            services.AddTransient<IValidator<CommentRequestDto>, CommentValidator>();
            services.AddTransient<IValidator<CommentUpdateRequestDto>, CommentUpdateValidator>();


            return services;
        }
    }
}
