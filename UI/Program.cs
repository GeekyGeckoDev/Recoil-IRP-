using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UI.Authentication;
using UI.Clients;
using UI.Components;
using UI.Exstensions;

var builder = WebApplication.CreateBuilder(args);

var signingKey = builder.Configuration["Jwt:SigningKey"]
    ?? throw new InvalidOperationException("JWT Signing Key is missing.");

var keyBytes = Convert.FromBase64String(signingKey);

// Add services to the container.
builder.Services.AddRazorComponents()
 .AddInteractiveServerComponents();

//Authentication & Authorization

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthManager>();
builder.Services.AddScoped<CustomAuthStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthStateProvider>());


builder.Services.AddSingleton<ITokenStore, TokenStore>();
builder.Services.AddTransient<AuthHeaderHandler>();

builder.Services.AddAuthorization();

//Bases fpr authHandler

var employeeBase = builder.Configuration["ApiSettings:EmployeeBaseUrl"];

builder.Services.AddHttpClient<AuthClient>(c =>
{
    c.BaseAddress = new Uri(employeeBase);
})
    .AddHttpMessageHandler<AuthHeaderHandler>();

//

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
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>();

app.Run();
