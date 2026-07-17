using Infrastructure.AutoMapper;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<ICourierService, CourierService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<DataContext>(t => t.UseNpgsql(builder.Configuration.GetConnectionString
    ("DefaultConnection")).UseSnakeCaseNamingConvention());
builder.Services.AddAutoMapper(typeof(InfrastructureProfile));
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" }));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();