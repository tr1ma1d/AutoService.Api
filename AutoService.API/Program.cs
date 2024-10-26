using AutoService.Core.Abstractions;
using AutoService.Core.Models;
using AutoService.DataAccess;
using AutoService.DataAccess.Repositoties;
using AutoService.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Register services and repositories
builder.Services.AddScoped<IRepository<Users>, UserRepository>();
builder.Services.AddScoped<IRepository<Cars>, CarRepository>();
builder.Services.AddScoped<IService<Users>, UserService>();
builder.Services.AddScoped<IService<Cars>, CarService>();
builder.Services.AddScoped<IUserService, UserService>();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database context
builder.Services.AddDbContext<AutoServiceDataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
