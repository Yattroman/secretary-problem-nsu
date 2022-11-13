using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using spn.targets.common.models;
using spn.targets.common.models.interfaces;
using spn.targets.common.util;
using spn.targets.task_4_db.db_conf;
using spn.targets.task_4_db.repositories;
using spn.targets.task_4_db.services;

namespace spn.targets.task_4_db;

public class Startup
{
    private const string ConfigAppSettingsPath = "targets/resources/appsettings.json";

    public static void Main(string[] args)
    {
        CreateHostBuilderFomDbInit(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilderFomDbInit(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) => ConfigureServices(services))
            .ConfigureHostConfiguration(confHost => ConfigureHostConfiguration(confHost, args));
    }

    private static IConfiguration CreateConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile(ConfigAppSettingsPath)
            .Build();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHostedService<SearchProcHostedWithDb>();
        services.AddScoped<IPrincess, Princess>();
        services.AddScoped<IHall, Hall>();
        services.AddScoped<IFriend, Friend>();
        services.AddScoped<IChoiceStrategy, ChoiceStrategyByFriendHelp>();
        services.AddScoped<IContendersFactory, ContenderFactory>();
        services.AddScoped<ISearchLoveRepository, SearchLoveRepository>();
        services.AddScoped<IDbSearchLoveService, DbSearchLoveService>();
        services.AddScoped<ISearchProcessSimulatorService, SearchProcessSimulatorService>();
        services.AddDbContextFactory<EnvironmentContext>(options =>
            options.UseNpgsql("name=ConnectionStrings:EnvironmentDatabase"));
    }

    private static void ConfigureHostConfiguration(IConfigurationBuilder builder, string[] args)
    {
        builder.AddJsonFile(ConfigAppSettingsPath);
    }
}