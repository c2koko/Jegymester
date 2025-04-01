using Jegymester.DataContext.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jegymester.DataContext.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace Jegymester.Services
{

    public interface ITestData
    {
        Task<bool> ClearDatabaseDataAsync();
        Task<bool> InsertDatabaseTestDataAsync();
    }
    public class TestData : ITestData
    {
        private readonly JegymesterDbContext _context;

        public TestData(JegymesterDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ClearDatabaseDataAsync()
        {
            /* 
            Clear order:
                -Tickets
                -Screenings
                -Movies
                -Users
                -Roles
                
                összekötő táblák helye (ha lesznek) a sorban még nincs meg
            */
            {
                //clear TICKETS
                var trows = from o in _context.Tickets select o;
                foreach (var row in trows) { _context.Tickets.Remove(row); }
                await _context.SaveChangesAsync();

                //clear SCREENINGS
                var scrows = from o in _context.Screenings select o;
                foreach (var row in scrows) {_context.Screenings.Remove(row);}
                await _context.SaveChangesAsync();

                //clear MOVIES
                var mrows = from o in _context.Movies select o;
                foreach (var row in mrows) {_context.Movies.Remove(row);}
                await _context.SaveChangesAsync();

                //clear USERS
                var urows = from o in _context.Users select o;
                foreach (var row in urows) { _context.Users.Remove(row); }
                await _context.SaveChangesAsync();

                //clear ROLES
                var rrows = from o in _context.Roles select o;
                foreach (var row in rrows){_context.Roles.Remove(row);}
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> InsertDatabaseTestDataAsync()
        {
            // 0) create roles
            Role NotRegisteredUser = new Role { PermaId = 1, RoleName = "NotRegisteredUser" };
            Role RegisteredUser = new Role { PermaId = 2, RoleName = "RegisteredUser" };
            Role Cashier = new Role { PermaId = 3, RoleName = "Cashier" };
            Role Admin = new Role { PermaId = 4, RoleName = "Admin" };

            // 0.1) create users


            // 1) create movies
            Movie movie1 = new Movie{MovieName = "Film1", MovieDescription = "Film1 leirasa", MovieDuration = 110};
            Movie movie2 = new Movie{MovieName = "Film2", MovieDescription = "Film2 Leirasa", MovieDuration = 124};
            Movie movie3 = new Movie{MovieName = "Film3", MovieDescription = "Film3 Leirasa", MovieDuration = 106};

            //adding the test data to the db
            {

            //roles
            _context.Roles.Add(NotRegisteredUser);
            _context.Roles.Add(RegisteredUser);
            _context.Roles.Add(Cashier);
            _context.Roles.Add(Admin);
            await _context.SaveChangesAsync();
            
            //movies
            _context.Movies.Add(movie1);
            _context.Movies.Add(movie2);
            _context.Movies.Add(movie3);
            await _context.SaveChangesAsync();

            //screeninget nem tudunk tesztadatként készíteni autoincrement movieid miatt
            //ticketet nem tudunk tesztadatként készíteni mert nincs teszt screenings, ezeket mind manuálisan kell tesztelni apival
            }
            return true;
        }
    }
}
