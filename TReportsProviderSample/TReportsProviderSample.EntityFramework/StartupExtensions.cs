using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TReportsProviderSampleEntityFramework
{
  public static class StartupExtensions
  {
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
      using (var serviceScope = app.ApplicationServices.CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<ContextSample>();
        context.Database.Migrate();
      }
      return app;
    }
  }
}
