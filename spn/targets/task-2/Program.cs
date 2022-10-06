using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace task_1.targets.task_2;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

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