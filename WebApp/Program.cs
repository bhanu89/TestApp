using Microsoft.Extensions.Options;
using WebApp.Services.Forecast;
using WebApp.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection(ApiSettings.Section));

// Register HttpClient with Base Address from configuration
builder.Services.AddHttpClient(Options.DefaultName, (serviceProvider, client) =>
{
    var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri(apiSettings.Url);
});

builder.Services.AddTransient<IForecastService, ForecastService>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
