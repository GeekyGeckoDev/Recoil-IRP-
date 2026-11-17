
using Monitoring.AspNetCore.Extensions;
using Monitoring.AspNetCore.Options;
using TestUI.Components;
using TestUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Monitoring SDK
builder.Services.AddMonitoringServices(options =>
{
    options.BaseAddress = "https://localhost:7228";
    options.BatchSize = 100;
    options.UploadInterval = TimeSpan.FromSeconds(5);
    options.MaxRetryAttempts = 3;
});

// HTTP monitoring configuration
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

builder.Services.AddControllers();


// Razor Components
builder.Services.AddRazorComponents();

var app = builder.Build();

// Exception handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

// Monitoring middleware
app.UseRecoilMonitoring();
app.MapControllers();

app.UseStatusCodePagesWithReExecute(
    "/not-found",
    createScopeForStatusCodePages: true);

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>();

app.Run();