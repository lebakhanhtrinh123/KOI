﻿using BusinessLayer.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepoitoryLayer.Implement;
using RepoitoryLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Repository;
using ServiceLayer.Implement;
using ServiceLayer.Interface;
using ServiceLayer.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("default.json", optional: true, reloadOnChange: true);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddDbContext<KoiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBDefault")));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthenRepository, AuthenRepository>();
builder.Services.AddScoped<IAuthenService, AuthenService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();

builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IVnPayRepo, VnPayRepo>();


builder.Services.AddScoped<IPondsRepository, PondsRepository>();
builder.Services.AddScoped<IPondsService, PondsService>();

builder.Services.AddScoped<ISaltCalculationsRepository, SaltCalculationsRepository>();

builder.Services.AddScoped<IWaterParametersRepository, WaterParametersRepository>();


builder.Services.AddScoped<ISaltCalculationService, SaltCalculationService>();

builder.Services.AddScoped<IKoiRepository, KoiRepository>();
builder.Services.AddScoped<IKoiService, KoiService>();


builder.Services.AddScoped<IKoiGrowthRepository, KoiGrowthRepository>();
builder.Services.AddScoped<IKoiGrowthService, KoiGrowthService>();

builder.Services.AddScoped<IFeedScheduleService, FeedScheduleService>();
builder.Services.AddScoped<IFeedScheduleRepository, FeedScheduleRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3001")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin() // Cho phép mọi origin, hữu ích cho việc kiểm tra trên di động
            .AllowAnyMethod()
            .AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseCors("AllowAll");
app.UseCors("AllowReactApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
