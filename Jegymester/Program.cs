using Jegymester.DataContext.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Utilities;
using Jegymester.DataContext.Entities;
using Jegymester.Controllers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Jegymester.Services;


//banner - comment it out before migrations and database updates
//UtilitiesClass.PrintBanner();
//https://localhost:7137/scalar/v1 <--- Ctrl + Click to open to easily test API stuff



//Authentication and Authorization stuff
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // saját localhost
            ValidAudience = builder.Configuration["Jwt:Audience"], // saját localhost
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // saját jelszó
        };
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//connecting to the db
builder.Services.AddDbContext<JegymesterDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Service
builder.Services.AddScoped<ITestData, TestData>(); //this one is for clearing and inserting test data
//============================================= UNDER DEV =========================================
//builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IScreeningService, ScreeningService>();
builder.Services.AddScoped<ICashierService, CashierService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IUserService, UserService>();


//enélkül valamiért nem mûködnek rendesen az hívások, valami Json.Serialization cycle miatt
builder.Services.AddMvc()
               .AddJsonOptions(opt =>
               {
                   opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
               });

//AutoMapper config
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();

    //https://localhost:7137/scalar/v1 <--- open to easily test API stuff
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(options =>
{
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
});

app.MapControllers();

app.Run();

