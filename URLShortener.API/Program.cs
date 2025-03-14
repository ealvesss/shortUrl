using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.App.Interfaces;
using URLShortener.App.Services;
using URLShortener.Domain.DTOs;
using URLShortener.Repo.Persistence;
using URLShortener.Repo.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Database Configuration (PostgreSQL com RDS)
builder.Services.AddDbContext<IUrlDbContext, UrlDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PrimaryDb")));

//SQS Configuration
var awsRegion = builder.Configuration["AWS:Region"]; // Get Region in appsettings.json file
builder.Services.AddSingleton<IAmazonSQS>(new AmazonSQSClient(Amazon.RegionEndpoint.GetBySystemName(awsRegion)));
builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddScoped<IUrlService, UrlService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // added swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
// app.UseAuthorization();
app.MapControllers();

app.UseHttpsRedirection();

app.Run();