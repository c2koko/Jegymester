using Jegymester.Properties;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.Red;Console.WriteLine("       __                 __  ___    _____ __       ____ \n" +                          "      / ___  ____ ___  __/  |/  ___ / ___// /____  / __ \\\n" +                          " __  / / _ \\/ __ `/ / / / /|_/ / _ \\\\__ \\/ __/ _ \\/ /_/ /\n" +                          "/ /_/ /  __/ /_/ / /_/ / /  / /  _____/ / /_/  __/ _, _/ \n" +                          "\\____/\\___/\\__, /\\__, /_/  /_/\\___/____/\\__/\\___/_/ |_|  \n" +                          "          /____//____/                                   \n" +                          "           ___        ____                               \n" +                          " _   __   <  /       / __ \\                              \n" +                          "| | / /   / /       / / / /                              \n" +                          "| |/ /   / /  _    / /_/ /                               \n" +                          "|___/   /_/  (_)   \\____/                                \n" +                          "                                                         ");Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

