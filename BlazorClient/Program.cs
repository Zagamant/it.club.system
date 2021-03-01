using System;
using BlazorClient.Services.UserManagement;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorClient.Services.AccountManagement;
using BlazorClient.Services.AlertManagement;
using BlazorClient.Services.AuthenticationManagement;
using BlazorClient.Services.LocalStorageManagement;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped<IUserService, UserService>()
                .AddScoped<ILocalStorageService, LocalStorageService>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IAlertService, AlertService>();

            builder.Services.AddTransient(x =>
            {
                var apiUrl = new Uri(builder.Configuration["apiUrl"]);
                
                return new HttpClient {BaseAddress = apiUrl};
                //  sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/v1/")});
            });

            
            builder.Services.AddAntDesign();

            var host = builder.Build();

            var accountService = host.Services.GetRequiredService<IAccountService>();
            await accountService.Initialize();

            await host.RunAsync();
        }
    }
}