using Application.Common.Events;
using Application.Common.Events.EventHandlers;
using Application.IRepos;
using Application.IServices;
using Application.Services;
using Domain.Events.Alerts;
using Infrastructure;
using Infrastructure.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Infrastructure

builder.Services.AddScoped<
    IDomainEventDispatcher,
    DomainEventDispatcher>();

builder.Services.AddScoped<
    IDomainEventHandler<AlertCreatedEvent>,
    CreateIncidentFromAlertHandler>();

builder.Services.AddScoped<IAlertService, AlertService>();

builder.Services.AddScoped<
    IUnitOfWork,
    UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
