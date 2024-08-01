using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using TodoApp.Web;
using TodoApp.Web.ApiServices;
using TodoApp.Web.ApiServices.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["TodoAppApi:BaseUrl"]!) });

// Api services
builder.Services.AddScoped<ITodoService, TodoService>();

// Radzen components
builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();
