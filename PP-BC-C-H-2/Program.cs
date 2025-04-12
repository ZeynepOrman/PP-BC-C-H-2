using FluentValidation.AspNetCore;
// No changes needed in this file for the method rename.
using PP_BC_C_H_2.Controllers;
using PP_BC_C_H_2.Middleware;
using PP_BC_C_H_2.Services;
using PP_BC_C_H_2.Extensions;
using PP_BC_C_H_2.Attributes;
using PP_BC_C_H_2.Validation;
using PP_BC_C_H_2.Services;
using log4net;
using log4net.Config;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add FluentValidation
builder.Services.AddControllers().AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssemblyContaining<ProductValidator>();
});

// Register product services
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure log4net
var log4netConfig = new FileInfo("log4net.config");
XmlConfigurator.Configure(log4netConfig);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.Run();
