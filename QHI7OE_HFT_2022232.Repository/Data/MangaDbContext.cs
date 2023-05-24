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
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("manga");
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
                new Manga("10#Fire Force#15,41#7,6#2015*9*23#6#1"),
                new Manga("11#Jujutsu Kaisen#5,99#8,6#2018*3*5#7#2"),
                new Manga("12#Demon Slayer#5,99#8#2016*2*10#7#2"),
                new Manga("13#My Hero Academia#3,99#9,1#2014*11*4#9#1"),
                new Manga("14#Attack On Titan#7,99#9,5#2009*9*9#10#2"),
                new Manga("15#Bleach#5,99#8,6#2001*8*1#11#1"),
                new Manga("16#JoJo's Bizarre Adventure#11,65#10#1987*1*1#12#5"),
                new Manga("17#Burn The Witch#6,15#7,6#2022*10*2#11#1"),
                new Manga("18#Dragon Ball#5,99#6,9#1984*12*3#13#6"),
                new Manga("19#Dragon Ball Z#5,99#7,3#1986*2*26#13#6"),
                new Manga("20#Dragon Ball Super#5,99#6,4#2015*6*20#13#6"),
                new Manga("21#Goodbye Eri#11,69#9,3#2022*6*5#3#7"),
                new Manga("22#Fist of the North Star#11,68#8,3#1983*9*23#14#6"),
                new Manga("23#Blue Period#8,50#8,6#2017*12*22#15#7"),
                new Manga("24#Tokyo Revengers#16,27#7,3#2015*3*1#16#1"),
                new Manga("25#Shinjuku Swan#15,41#6,3#2005*4*11#16#1"),
                new Manga("26#Vinlad Saga#15,27#9,4#2013*10*14#17#8"),
                new Manga("27#Vagabond#7,71#9,3#1998*9*17#18#8"),
                new Manga("28#Slam Dunk#8,31#9,9#1990*10*1#18#9"),
                new Manga("29#Blue Lock#10,65#7,6#2018*10*6#19#9"),
                new Manga("30#Dorohedoro#4,99#9,9#2000*10*30#20#2")
            });

            modelBuilder.Entity<Author>().HasData(new Author[]
           {
               new Author("1#Kishimoto Masashi"),
               new Author("2#Miura Kentaro"),
               new Author("3#Fujimoto Tatsuki"),
               new Author("4#Ito Junji"),
               new Author("5#Tomoki Izumi"),
               new Author("6#Ohkubo Atsushi"),
               new Author("7#Akutami Gege"),
               new Author("8#Gotouge Koyoharu"),
               new Author("9#Horikoshi Kohei"),
               new Author("10#Isayama Hajime"),
               new Author("11#Kubo Tite"),
               new Author("12#Araki Hirohiko"),
               new Author("13#Toriyama Akira"),
               new Author("14#Hara Tetsuo"),
               new Author("15# Yamaguchi Tsubasa"),
               new Author("16#Wakui Ken"),
               new Author("17#Yukimura Makoto"),
               new Author("18#Inoue Takehiko"),
               new Author("19#Kaneshiro Muneyuki"),
               new Author("20# Hayashida Q")
           });

            modelBuilder.Entity<Genre>().HasData(new Genre[]
           {
               new Genre("1#Adventure Fantasy"),
               new Genre("2#Dark Fantasy"),
               new Genre("3#Horror"),
               new Genre("4#Horror Comedy"),
               new Genre("5#JoJo's Bizarre Adventure"),
               new Genre("6#OG BattleShonen"),
               new Genre("7#Human Drama"),
               new Genre("8#Seinen"),
               new Genre("9#Sport")
           });
        }
    }
}
