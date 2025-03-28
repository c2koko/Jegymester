using Jegymester.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;

namespace Jegymester.DataContext.Data
{
    public class JegymesterDbContext(DbContextOptions<JegymesterDbContext> options) : DbContext(options)
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Chair> Chairs { get; set; }
        public DbSet<Room> Rooms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        //--- User tábla relációi ---

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Id)
                .IsUnique();

            // Primary key
            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            // User |1 <=> N| Role
            modelBuilder.Entity<User>()
                .HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // User |1 <=> N| Ticket
            modelBuilder.Entity<User>()
                .HasMany(user => user.Tickets)
                .WithOne(ticket => ticket.User)
                .HasForeignKey(ticket => ticket.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        //--- Ticket tábla relációi (User releciókon kívül) ---

            modelBuilder.Entity<Ticket>()
                .HasIndex(ticket => ticket.Id)
                .IsUnique();

            // Primary Key
            modelBuilder.Entity<Ticket>()
                .HasKey(ticket => ticket.Id);

            // Ticket |N <=> 1| Screening
            modelBuilder.Entity<Ticket>()
                .HasOne(ticket => ticket.Screening)
                .WithMany(scr => scr.Tickets)
                .HasForeignKey(ticket => ticket.ScreeningId)
                .OnDelete(DeleteBehavior.Cascade);

        //--- Screening tábla relációi ---

            // Primary Key
            modelBuilder.Entity<Screening>()
                .HasIndex(scr => scr.Id)
                .IsUnique();

            modelBuilder.Entity<Screening>()
                .HasKey(scr => scr.Id);

            // Screening |N <=> 1| Movie
            modelBuilder.Entity<Screening>()
                .HasOne(scr => scr.Movie)
                .WithMany(movie => movie.Screenings)
                .HasForeignKey(scr => scr.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // Screening |N <=> 1| Room                // változtatva

            modelBuilder.Entity<Screening>()
                .HasOne(scr => scr.Room)                
                .WithMany(room => room.screening) 
                .HasForeignKey(scr => scr.RoomId)
                .OnDelete(DeleteBehavior.Cascade);


            // Room |1 <=> N| Chair

            modelBuilder.Entity<Chair>()
                .HasOne(chr => chr.Room)
                .WithMany(r => r.chairs)
                .HasForeignKey(chr => chr.RoomId)
                .OnDelete(DeleteBehavior.Cascade);



        //--- Room tábla relációi ---
        
        /*
            modelBuilder.Entity<Room>()
                .HasIndex(room => room.Id)
                .IsUnique();

            modelBuilder.Entity<Room>()
                .HasKey(room => room.Id);

            // Room |1 <=> N| RoomChair

            modelBuilder.Entity<Room>()
                .HasMany(room => room.RoomsChairs)
                .WithOne(rc => rc.Room)
                .HasForeignKey(rc => rc.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

        //--- RoomChair kapcsolótábla relációi ---

            // öszetett kulcs
            modelBuilder.Entity<RoomChair>()
                .HasKey(rc => new { rc.RoomId, rc.ChairId });

            modelBuilder.Entity<RoomChair>()
                .HasOne(rc => rc.Chair)
                .WithMany(chair => chair.RoomsChairs)
                .HasForeignKey(rc => rc.ChairId)
                .OnDelete(DeleteBehavior.Cascade);
        

        //--- Chair tábla relációi ---

            modelBuilder.Entity<Chair>()
                .HasIndex(chair => chair.Id)
                .IsUnique();

            */

            modelBuilder.Entity<Chair>()
                .HasKey(chair => chair.Id);
        }
    }
}
