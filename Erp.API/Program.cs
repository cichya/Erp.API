using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Erp.API.Data;
using Erp.API.XmlDataProvider;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Erp.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

			try
			{
				logger.Debug("init main");
				var host = CreateWebHostBuilder(args).Build();

				using (var scope = host.Services.CreateScope())
				{
					var services = scope.ServiceProvider;

					var ctx = services.GetRequiredService<DataContext>();

					DataProvider.Init(services);
				}

				host.Run();
			}
			catch (Exception ex)
			{
				//NLog: catch setup errors
				logger.Error(ex, "Stopped program because of exception");
				throw;
			}
			finally
			{
				// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
				NLog.LogManager.Shutdown();
			}
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
				})
				.UseNLog();  // NLog: setup NLog for Dependency injection;
	}
}
