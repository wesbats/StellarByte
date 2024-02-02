using Application.Services;
using Domain.Request;
using Domain.Responses;
using Domain.Validator;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(config =>
{
    config.AddPolicy("AllowOrigin", options => options
                                                 .AllowAnyOrigin()
                                                 .AllowAnyMethod());
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IComputerRepository, ComputerRepository>();
builder.Services.AddScoped<IComputerService, ComputerService>();
builder.Services.AddScoped<IValidator<BaseComputerRequest>, ComputerValidator>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValidator<BaseUserRequest>, UserValidator>();

builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IValidator<BaseOrderRequest>, OrderValidator>();
builder.Services.AddScoped<IValidator<BaseOrderItem>, ItemValidator>();
builder.Services.AddScoped<IValidator<UpdatedOrderAddressRequest>, AddressValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
