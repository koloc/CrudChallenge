using CrudChallenge.Data;
using CrudChallenge.Data.Repositories;
using CrudChallenge.Notifications.Services;
using CrudChallenge.ReportWorker;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddHostedService<Worker>();

        services.AddDbContext<CrudChallengeDbContext>(options =>
        {
            var connectionString = ctx.Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        },
        optionsLifetime: ServiceLifetime.Singleton,
        contextLifetime: ServiceLifetime.Singleton);

        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<IEmailService, EmailService>();
    })
    .Build();

await host.RunAsync();
