using Monitoring.AspNetCore.Extensions;
using Monitoring.AspNetCore.Options;
using Monitoring.SDK.Extensions;
using TestUI.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<HttpMonitoringOptions>(options =>
{
    options.CaptureRequestBody = false;
    options.CaptureResponseBody = false;

    options.ExcludedPaths =
    [
        "/health",
        "/swagger"
    ];
});

builder.Services.AddMonitoringSdk(options =>
{
    options.BaseAddress = "https://localhost:7228";
    options.BatchSize = 100;
    options.UploadInterval = TimeSpan.FromSeconds(5);
    options.MaxRetryAttempts = 3;
});

builder.Services.Configure<HttpMonitoringOptions>(options =>
{
    options.CaptureRequestBody = false;
    options.CaptureResponseBody = false;
});

// Add services to the container.
builder.Services.AddRazorComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>();

app.UseRecoilMonitoring();

app.Run();
