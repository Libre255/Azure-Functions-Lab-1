using Cloud_Azure_Lab_3_part_1.Services.AzureMembersService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddScoped<IAzureMembersService, AzureMembersService>();
    })
    .Build();

host.Run();
