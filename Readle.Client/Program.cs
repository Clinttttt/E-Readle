using Readle.Client.Components;
using Readle.Client.Services;
using Readle.Infrastructure;
using Readle.Infrastructure.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
   


builder.Services.AddScoped<BookServices>();
builder.Services.AddScoped<PageState>();


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpClient<BookApiServices>();
builder.Services.AddHttpClient<AuthApiServices>(options =>
options.BaseAddress = new Uri("https://localhost:7248"));


builder.Services.AddHttpClient("WithRedirects")
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 10,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };
    });




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();
