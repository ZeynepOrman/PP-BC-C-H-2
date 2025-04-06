using FluentValidation.AspNetCore;
using PP_BC_C_H_2.Controllers;
using PP_BC_C_H_2.Middleware;
using PP_BC_C_H_2.Services;
using PP_BC_C_H_2.Extensions;
using PP_BC_C_H_2.Attributes;
using PP_BC_C_H_2.Validation;

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

// Register fake services
builder.Services.AddScoped<IFakeService, FakeService>();

// Add custom extensions
builder.Services.AddCustomExtensions();

var app = builder.Build();

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

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.Run();
