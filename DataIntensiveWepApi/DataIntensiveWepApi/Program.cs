using DataIntensiveWepApi;
using DataIntensiveWepApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using DataIntensiveWepApi.RepositoriesOne;
using DataIntensiveWepApi.Services;
using DataIntensiveWepApi.RepositoriesTwo;

var builder = WebApplication.CreateBuilder(args);

// Add AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile));

// Add services to the container.

var conStringOne = builder.Configuration.GetConnectionString("DbOneConnection") ??
     throw new InvalidOperationException("Connection string 'DbOneConnection'" +
    " not found.");

var conStringTwo = builder.Configuration.GetConnectionString("DbTwoConnection") ??
     throw new InvalidOperationException("Connection string 'DbTwoConnection'" +
    " not found.");

builder.Services.AddDbContext<DataIntensiveDatabase1Context>(option => option.UseSqlServer(conStringOne));

builder.Services.AddDbContext<DataIntensiveDatabase2Context>(option => option.UseSqlServer(conStringTwo));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerRepositoryTwo, CustomerRepositoryTwo>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

//Configure CORS
builder.Services.AddCors(options => options.AddPolicy("AllowAnyPolicy",
    builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
