using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Identity;
using CleanArchMvc.Infra.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraEstructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options
              .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                  x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            options.AccessDeniedPath = "/Account/Login");

            services.AddScoped<IAuthenticate, Authenticate>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IproductRepository, ProductRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfiler));

            var myHandlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(myHandlers));

            return services;
        }
    }
}
