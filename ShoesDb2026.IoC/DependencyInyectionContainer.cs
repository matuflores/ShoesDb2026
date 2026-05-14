using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShoesDb2026.Data;
using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Data.Repositories;
using ShoesDb2026.Entities;
using ShoesDb2026.Service.Interfaces;
using ShoesDb2026.Service.Services;
using ShoesDb2026.Service.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.IoC
{
    public static class DependencyInyectionContainer
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ShoesDb2026DbContext>();

            services.AddScoped<IBrandsRepository, BrandRepository>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IValidator<Brand>, BrandValidator>();

            services.AddScoped<IGenresRepository, GenreRepository>();
            //services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IValidator<Genre>, GenreValidator>();

            services.AddScoped<IShoesRepository, ShoeRepository>();
            services.AddScoped<IShoeService, ShoeService>();
            services.AddScoped<IValidator<Shoe>, ShoeValidator>();

            services.AddScoped<ISizesRepository, SizeRepository>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IValidator<Size>, SizeValidator>();

            services.AddScoped<ISportsRepository, SportRepository>();
            services.AddScoped<ISportService, SportService>();
            services.AddScoped<IValidator<Sport>, SportValidator>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services.BuildServiceProvider();
        }
    }
}
