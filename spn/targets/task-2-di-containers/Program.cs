using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using spn.targets.common.models;
using spn.targets.common.models.interfaces;
using spn.targets.common.util;

namespace spn.targets.task_2_di_containers;

public class Program
{
    /*public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }*/

    static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<SearchProcHosted>();
                services.AddScoped<IPrincess, Princess>();
                services.AddScoped<IHall, Hall>();
                services.AddScoped<IFriend, Friend>();
                services.AddScoped<IChoiceStrategy, ChoiceStrategyByFriendHelp>();
                services.AddScoped<IContendersFactory, ContenderFactory>();
            });
    }
}