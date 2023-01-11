using System.Reflection;
using CarBooking.API.Services;
using CarBooking.Data.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json", false, true)
    .Build();
builder.Host.UseNLog();

builder.Services.AddDbContext<CarBookingContext>(op => op.UseSqlServer(builder.Configuration["ConnectionStrings:CarBookingDb"],
                             b => b.MigrationsAssembly("CarBooking.Data")));
builder.Services.AddControllers().AddFluentValidation(options =>
{
    // Validate child properties and root collection elements
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;

    // Automatic registration of validators in assembly
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}); ;

builder.Services.AddCoreServices();


builder.Services.AddSwaggerGen(s => 
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "V1",
        Title = "Car Booking API",
        Description = "API for car booking system"
    });
});

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.Run();
