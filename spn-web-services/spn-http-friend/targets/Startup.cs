using Microsoft.EntityFrameworkCore;
using spn_db.targets.db_conf;
using spn_db.targets.repositories;
using spn_db.targets.services;
using spn_http_friend.targets.services;

namespace spn_http_friend.targets;

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
        services.AddScoped<IFriendService, FriendService>();
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