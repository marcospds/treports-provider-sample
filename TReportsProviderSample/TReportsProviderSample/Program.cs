using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace TReportsProviderSample
{
  public class Program
  {
    private static IWebHost BuildWebHost(string[] args)
    {
      var configuration = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("hosting.json", optional: true)
                      .AddCommandLine(args)
                      .Build();

      return WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>()
                .UseSerilog()
                .UseConfiguration(configuration)
                .Build();
    }

    public static int Main(string[] args)
    {

      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File(Directory.GetCurrentDirectory()+ "\\logs\\log.txt")
  

        .CreateLogger();

      try
      {
        Log.Information("Inicializando Host do provedor Samples...");
        BuildWebHost(args).Run();
        return 0;
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Erro inesperado...");
        return 1;
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }
  }
}

      /*var configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("hosting.json", optional: true)
                  .AddCommandLine(args)
                  .Build();

      try
      {
        var host = WebHost.CreateDefaultBuilder(args)
          .ConfigureAppConfiguration((hostingContext, config) =>
          {
            var env = hostingContext.HostingEnvironment;
            config.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
            config.AddEnvironmentVariables();
            config.AddCommandLine(args);
          })
          .UseConfiguration(configuration)
          .UseStartup<Startup>()
          .ConfigureLogging((hostingContext, logging) =>
          {
            Log.Logger = new LoggerConfiguration()
            .Enrich.WithMachineName()
            .ReadFrom.Configuration(hostingContext.Configuration)
            .CreateLogger();
          })
          .UseSerilog()
          .Build();
        Log.Error("ero");
        host.Run();
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }
    }*/
