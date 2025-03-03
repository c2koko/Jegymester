using Jegymester.Data;
using Jegymester.Entities;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Utilities;
using Entities;
using Jegymester.Controllers;

UtilitiesClass.PrintBanner();

/*                              /// TESZT \\\
Rooms room = new Rooms();
List<bool> SzékekBooood = new List<bool>(){ true, false, true, false, true, true, false, false, true, false, false, true, true, false, true, false, false, true, true, false, true, false, false, true, true, false, false, true, true, false, true, false, true, false, false, true, true, false, true, false, false, true, true, false, true, false, true, false, false, true, true, false, true, false, false, true, true, false, true, false, true, false, false, true, true, false, true, false, false, true, true, false, true, false, true, false, false, true, true, false, true, false, false, true, true, false, true, false, true, false, false, true, true, false, true, false, false, true, true, false, true, false, true, false, false, true, true, false, true, false, false, true, true, false, true, false, true, false, false, true, true, false, true, false, false, true };
room.Chairs = SzékekBaszod;
room.BookChair(35);
room.BookChair(25);
room.BookChair(75);
Console.WriteLine(room.GetEmptyChairs());
*/


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//connecting to the db
builder.Services.AddDbContext<JegymesterDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();

    //https://localhost:7137/scalar/v1 <--- open to easily test API stuff
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

