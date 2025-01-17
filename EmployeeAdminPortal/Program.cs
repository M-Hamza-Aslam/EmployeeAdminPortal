using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Repositories;
using EmployeeAdminPortal.Services;
using EmployeeAdminPortal.Validators.Employee;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));

//services
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<OfficeService>();

//repositories
builder.Services.AddScoped<EmployeeRepo>();

//fluent validator
builder.Services.AddFluentValidationAutoValidation(options =>
{
    options.DisableDataAnnotationsValidation = true;
}).AddValidatorsFromAssemblyContaining<AddEmployeeDtoValidator>();

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
