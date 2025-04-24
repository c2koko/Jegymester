using Jegymester.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;

namespace Jegymester.DataContext.Data
{
    public class JegymesterDbContext(DbContextOptions<JegymesterDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        // chair stuff
        public DbSet<Chair> Chairs { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //--------Movie Relations--------//
            //PK
            modelBuilder.Entity<Movie>()
                .HasIndex(m => m.Id)
                .IsUnique();
            modelBuilder.Entity<Movie>()
                .HasKey(m => m.Id);
            //Screening |N <=> 1| Movie kifejtve screening relationsben


            //--------Role relations--------//
            // Role |N <=> 1| User
            modelBuilder.Entity<Role>() //Principal keyt használunk primary key helyett a role - user összeköttetésnél
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasPrincipalKey(r => r.PermaId);

            //--------User relations--------//
            // Role |N <=> 1| User kifejtve role relationsben
            // Ticket |N <=> 1| User kifejtve ticket relationsben


            //--------Screening relations--------//
            //PK
            modelBuilder.Entity<Screening>()
                .HasIndex(s => s.Id)
                .IsUnique();
            modelBuilder.Entity<Screening>()
                .HasKey(s => s.Id);

            // Screening |N <=> 1| Movie
            modelBuilder.Entity<Screening>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Screenings)
                .HasForeignKey(s => s.MovieId);
            //.OnDelete(DeleteBehavior.Cascade); még teszt alatt

            // Ticket |N <=> 1| Screening kifejtve ticket relationsben


            //--------Ticket relations--------//
            //PK
            modelBuilder.Entity<Ticket>()
                .HasIndex(t => t.Id)
                .IsUnique();
            modelBuilder.Entity<Ticket>()
                .HasKey(t => t.Id);

            // Ticket |N <=> 1| User               
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId);
            //.OnDelete(DeleteBehavior.Cascade);

            // Ticket |N <=> 1| Screening
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Screening)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.ScreeningId);

            //----------------------------------------- chair stuff---------------------------------------

            // Screening |N <=> 1| Room

            modelBuilder.Entity<Screening>()
                .HasOne(scr => scr.Room)
                .WithMany(room => room.screening)
                .HasForeignKey(scr => scr.RoomId)
                .OnDelete(DeleteBehavior.Cascade);


            // Room |1 <=> N| Chair

            modelBuilder.Entity<Chair>()
                .HasOne(chr => chr.Room)
                .WithMany(r => r.chairs)
                .HasForeignKey(chr => chr.RoomId);

            // Chair

            modelBuilder.Entity<Chair>()
                .HasKey(chair => chair.Id);


            /*
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

            // Screening |N <=> 1| Room

            modelBuilder.Entity<Screening>()
                .HasOne(scr => scr.Room)
                .WithMany(room => room.Screenings)
                .HasForeignKey(scr => scr.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

        //--- Room tábla relációi ---

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

            modelBuilder.Entity<Chair>()
                .HasKey(chair => chair.Id);
            */
        }
    }
}
