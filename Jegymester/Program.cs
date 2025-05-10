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

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetpincerApp API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
    }});
});

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
builder.Services.AddScoped<IChairService, ChairService>();
builder.Services.AddScoped<INotRegisteredUserervice, NotRegisteredUserService>();


// ez kell, hogy a szerver engedéjezzen cross-origin kéréseket
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


//enélkül nem mûködnek rendesen a hívások scalarban, valami Json.Serialization cycle miatt
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

    app.UseSwagger();
    app.UseSwaggerUI( options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

    //https://localhost:7137/scalar/v1 <--- open to easily test API stuff
}


// kell a front-end mûködése érdekében
app.UseCors();

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

