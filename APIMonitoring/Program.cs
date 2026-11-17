
using APIMonitoring.Hubs;
using Monitoring.API.Extensions;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSignalR();

builder.Services.AddMonitoringServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUI", policy =>
    {
        policy.WithOrigins("https://localhost:7286", "http://localhost:5030")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseWhen(context =>
      app.Configuration.GetValue<bool?>("IsSwaggerUIEnabled") ?? false,
      appBuilder => appBuilder.UseSwaggerUI(options =>
      {
          options.SwaggerEndpoint("/openapi/v1.json", "StandardApi-v1");
      }));
}


app.MapHub<MonitoringHub>("/hubs/monitoring");

app.UseHttpsRedirection();
app.UseCors("AllowUI");

app.UseAuthorization();

app.MapControllers();

app.Run();
