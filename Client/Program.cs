using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Occumetric.Client.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Occumetric.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("Occumetric.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the
            // server project
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Occumetric.ServerAPI"));
            builder.Services.AddTransient<ICommonHttpService, CommonHttpService>();

            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
