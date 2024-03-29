﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistence.Repositories;

namespace ETicaretAPI.Persistence
{
    public static class ServiceRegistration
    {
        /*
        Docker'da postgre kullanmak için cmd üzerinde 
        docker pull posgres
        diyerek son sürüm image'ını indiriyoruz.
        sonra onu bir container üzerinde ayağa kaldırmamız lazım
        docker run --name postgres -e POSTGRES_PASSWORD=123456 -d -p 5432:5432 postgres

        migration komutlarının çalışması için öncelikle  package manager console ekranında aşağıdaki komutu çalıştırıp ilgili package kurulmalıdır.
        Install-Package Microsoft.EntityFrameworkCore.Tools
        add-migration mig_1
        yaparak migration oluşturabiliriz.
        
        update-database 
        yaparsak ilgili migration'lar database'e uygulanır
         */
        public static void AddPersistenceService(this IServiceCollection services)
        {
            //ConfigurationManager configurationManager = new ConfigurationManager();
            //configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ETicaretAPI.Presentation"));
            //configurationManager.AddJsonFile("appsettings.json");
            services.AddDbContext<ETicaretAPIDbContext>(options => options.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=ETicaretAPIDb;"));
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();

            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
