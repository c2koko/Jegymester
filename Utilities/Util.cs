﻿using System.Diagnostics;

namespace Utilities
{
    public enum Genre
    {
    Action,
    Adventure,
    Animation,
    Comedy,
    Crime,
    Drama,
    Fantasy,
    Horror,
    Mystery,
    SciFi,
    Romance,
    Thriller,
    Western,
    War,
    Documentary,
    Musical
    }


    public static class UtilitiesClass
    {
        public static void PrintBanner()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("       __                 __  ___    _____ __       ____ \n" + 
                                                                                                                        "      / ___  ____ ___  __/  |/  ___ / ___// /____  / __ \\\n" +
                                                                                                                        " __  / / _ \\/ __ `/ / / / /|_/ / _ \\\\__ \\/ __/ _ \\/ /_/ /\n" +
                                                                                                                        "/ /_/ /  __/ /_/ / /_/ / /  / /  _____/ / /_/  __/ _, _/ \n" +
                                                                                                                        "\\____/\\___/\\__, /\\__, /_/  /_/\\___/____/\\__/\\___/_/ |_|  \n" +
                                                                                                                        "          /____//____/                                   \n" +
                                                                                                                        "           ___        ____                               \n" +
                                                                                                                        " _   __   <  /       / __ \\                              \n" +
                                                                                                                        "| | / /   / /       / / / /                              \n" +
                                                                                                                        "| |/ /   / /  _    / /_/ /                               \n" +
                                                                                                                        "|___/   /_/  (_)   \\____/                                \n" +
                                                                                                                        "                                                         ");                                                                                                      Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("ENTER - A szerver fut \n  0   - A szerver fut + scalar megnyílik böngészőben"); for (ConsoleKeyInfo key; (key = Console.ReadKey(true)).Key != ConsoleKey.Enter;) if (key.Key == ConsoleKey.D0) { Process.Start(new ProcessStartInfo { FileName = "https://localhost:7137/scalar/v1", UseShellExecute = true }); break; } Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White;
            // innen lehet folytatni a kódot...

        }

    }
    
}
