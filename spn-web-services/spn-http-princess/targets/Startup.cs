using spn_http_princess.targets.clients;
using spn_http_princess.targets.services;
using spn_models.targets.common.models;
using spn_models.targets.common.models.interfaces;

namespace spn_http_princess.targets;

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
        services.AddScoped<IPrincessService, PrincessService>();
        services.AddScoped<IPrincess, Princess>();
        services.AddScoped<IChoiceStrategy, ChoiceStrategyByFriendHelp>();
        services.AddHttpClient<IFriend, FriendClient>();
        services.AddHttpClient<ISimulatedHall, SimulatedHall>();
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