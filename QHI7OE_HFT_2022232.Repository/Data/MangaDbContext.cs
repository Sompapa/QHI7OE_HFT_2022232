using Microsoft.EntityFrameworkCore;
using QHI7OE_HFT_2022232.Models;
using System;

namespace QHI7OE_HFT_2022232.Repository
{
    public class MangaDbContext : DbContext
    {
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public MangaDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string conn =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Mangas.mdf;Integrated Security=True;MultipleActiveRsultSets=true";
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Manga>(mangas =>
            {
                mangas
                .HasOne(mangas => mangas.Author)
                .WithMany(authors => authors.Mangas)
                .HasForeignKey(authors => authors.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                // - DeleteBehavior.ClientSetNull: Csak olyan márkára engedi kiadni a törlést, amire nincs hivatkozás. Egyéb esetben, elszállna kivétellel.
                // - DeleteBehavior.Cascade: Ha törölsz egy márkát, akkor automatikusan az összes ilyen márkájú autót is törli.

                mangas
                .HasOne(mangas => mangas.Genre)
                .WithMany(genres => genres.Mangas)
                .HasForeignKey(mangas => mangas.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Author>(authors => authors
            .HasMany(authors => authors.Mangas)
            .WithOne(mangas => mangas.Author)
            .OnDelete(DeleteBehavior.ClientSetNull));

            modelBuilder.Entity<Genre>(genres => genres
            .HasMany(genres => genres.Mangas)
            .WithOne(mangas => mangas.Genre)
            .OnDelete(DeleteBehavior.ClientSetNull));

            modelBuilder.Entity<Manga>().HasData(new Manga[]
            {
                new Manga("1#Naruto#3,99#8,7#1999*9*21#1#1"),
                new Manga("2#Boruto#5,99#5,3#2016*5*9#1#1"),
                new Manga("3#Berserk#6,64#10#1990*10*26#2#2"),
                new Manga("4#Fire Punch#5,99#7,8#2016*9*14#3#2"),
                new Manga("5#Chainsaw Man#5,99#9,7#2020*7*7#3#2"),
                new Manga("6#Uzumaki#8,99#10#1998*8*3#4#3"),
                new Manga("7#Gyo#13,99#7,9#2002*2*28#4#3"),
                new Manga("8#Mieruko-chan#8,99#7,4#2021*7*3#5#4"),
                new Manga("9#Soul Eater#8,66#7,8#2008*4*7#6#1"),
                new Manga("10#fire Force#15,41#7,6#2015*9*23#6#1")
            });

            modelBuilder.Entity<Author>().HasData(new Author[]
           {
               new Author("1#Kishimoto Masashi"),
               new Author("2#Miura Kentaro"),
               new Author("3#Fujimoto Tatsuki"),
               new Author("4#Ito Junji"),
               new Author("5#Tomoki Izumi"),
               new Author("6#Ohkubo Atsushi")
           });

            modelBuilder.Entity<Genre>().HasData(new Genre[]
           {
               new Genre("1#Adventure Fantasy"),
               new Genre("2#Dark Fantasy"),
               new Genre("3#Horror"),
               new Genre("4#Horror Comedy")
           });
        }
    }
}
