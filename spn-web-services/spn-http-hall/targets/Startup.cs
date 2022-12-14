using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using spn_db.targets.db_conf;
using spn_db.targets.repositories;
using spn_db.targets.services;
using spn_http_hall.targets.services;
using spn_models.targets.common.models;
using spn_models.targets.common.models.interfaces;

namespace spn_http_hall.targets;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddScoped<IHallService, HallService>();
        services.AddScoped<IIterationRepository, IterationRepository>();
        services.AddScoped<IDbIterationService, DbIterationService>();
        services.AddScoped<ISearchLoveRepository, SearchLoveRepository>();
        services.AddScoped<IDbSearchLoveService, DbSearchLoveService>();
        services.AddDbContextFactory<EnvironmentContext>(options =>
            options.UseNpgsql("name=ConnectionStrings:EnvironmentDatabase"));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }

}