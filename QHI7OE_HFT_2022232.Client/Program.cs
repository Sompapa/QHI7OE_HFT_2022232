using ConsoleTools;
using QHI7OE_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace QHI7OE_HFT_2022232.Client
{ 
    internal class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            if (entity == "Author")
            {
                Console.WriteLine("Enter Author ID: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Author name:");
                string name = Console.ReadLine();
                rest.Post(new Author() { AuthorId = id, AuthorName = name }, "author");
            }
            else if (entity == "Manga")
            {
                Console.WriteLine("Enter Manga ID: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Manga title: ");
                string title = Console.ReadLine();
                Console.WriteLine("Enter Manga price: ");
                double price = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter Manga rating: ");
                double rating = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter Manga reales (fromat: yyyy*mm*dd): ");
                DateTime reales = DateTime.Parse(Console.ReadLine().Replace('*', '.'));
                Console.WriteLine("Enter Author ID: ");
                int authorid = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Genre ID: ");
                int genreid = int.Parse(Console.ReadLine());
                rest.Post(new Manga() { MangaId = id, Title = title, Price = price, Rating =rating, Release = reales, AuthorId = authorid, GenreId = genreid }, "manga");
            }
            else if (entity == "Genre")
            {
                Console.WriteLine("Enter Genre ID: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Genre name:");
                string name = Console.ReadLine();
                rest.Post(new Genre() { GenreId = id, GenreName = name }, "genre");
            }
        }
        static void List(string entity)
        {
            if (entity == "Author")
            {
                List<Author> authors = rest.Get<Author>("author");
                foreach (var item in authors)
                {
                    Console.WriteLine(item.AuthorId + ": " + item.AuthorName);
                }
            }
            else if(entity == "Manga")
            {
                List<Manga> mangas = rest.Get<Manga>("manga");
                foreach (var item in mangas)
                {
                    Console.WriteLine(item .MangaId + ": " + item.Title + ", " + item.Release + ", " + item.Rating + ", " + item.Price);
                }
            }
            else if (entity == "Genre")
            {
                List<Genre> genres = rest.Get<Genre>("genre");
                foreach (var item in genres)
                {
                    Console.WriteLine(item.GenreName);
                }
            }
            Console.ReadLine();
        }

        static void Update(string entity)
        {
            if (entity == "Author")
            {
                Console.WriteLine("Enter Author's ID to update: ");
                int id = int.Parse(Console.ReadLine());
                Author one = rest.Get<Author>(id, "author");
                Console.WriteLine($"New name [old: {one.AuthorName}]: ");
                string name = Console.ReadLine();
                one.AuthorName = name;
                rest.Put(one, "author");
            }
            else if (entity == "Manga")
            {
                Console.WriteLine("Enter Manga's ID to update it's price: ");
                int id = int.Parse(Console.ReadLine());
                Manga one = rest.Get<Manga>(id, "manga");
                Console.WriteLine($"New price [old: {one.Price}]: ");
                double price = double.Parse(Console.ReadLine());
                one.Price = price;
                rest.Put(one, "manga");
            }
            else if (entity == "Genre")
            {
                Console.WriteLine("Enter Genre's ID to update: ");
                int id = int.Parse(Console.ReadLine());
                Genre one = rest.Get<Genre>(id, "genre");
                Console.WriteLine($"New name [old: {one.GenreName}]: ");
                string name = Console.ReadLine();
                one.GenreName = name;
                rest.Put(one, "genre");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Author")
            {
                Console.WriteLine("Enter Author ID to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "author");
            }
            else if (entity == "Manga")
            {
                Console.WriteLine("Enter Manga ID to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "manga");
            }
            else if (entity == "Genre")
            {
                Console.WriteLine("Enter Genre ID to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "genre");
            }
        }
        static void StatMethods(string entity)
        {
            if (entity == "stat/avgPriceByAuthor" || entity == "stat/avgPriceByGenre" || entity == "stat/avgRateByGenre" || entity == "stat/allPriceByGenre")
            {
                var mangas = rest.Get<KeyValuePair<string, double>>(entity);
                foreach (var item in mangas)
                {
                    Console.WriteLine(item.Key + ": "+ item.Value);
                }
            }
            else if (entity == "stat/allPriceByYears")
            {
                var mangas = rest.Get<KeyValuePair<DateTime, double>>(entity);
                foreach (var item in mangas)
                {
                    Console.WriteLine(item.Key + ": " + item.Value);
                }
            }
            else
            {
                throw new Exception("No Non_CRUD found");
            }
            Console.ReadLine();
        }
        
        static void Main(string[] args)
        {
           rest = new RestService("http://localhost:59073/");

            var mangaSubMenu = new ConsoleMenu(args, level:1)
                .Add("List", ()=> List("Manga"))
                .Add("Create", ()=> Create("Manga"))
                .Add("Delete", ()=> Delete("Manga"))
                .Add("Update", ()=> Update("Manga"))
                .Add("Exit", ConsoleMenu.Close);

            var authorSubMenu = new ConsoleMenu(args, level: 1)
               .Add("List", () => List("Author"))
               .Add("Create", () => Create("Author"))
               .Add("Delete", () => Delete("Author"))
               .Add("Update", () => Update("Author"))
               .Add("Exit", ConsoleMenu.Close);

            var genreSubMenu = new ConsoleMenu(args, level: 1)
               .Add("List", () => List("Genre"))
               .Add("Create", () => Create("Genre"))
               .Add("Delete", () => Delete("Genre"))
               .Add("Update", () => Update("Genre"))
               .Add("Exit", ConsoleMenu.Close);

            var nonCrudSubMenu = new ConsoleMenu(args, level: 1)
               .Add("AVG Price By Author", () => StatMethods("stat/avgPriceByAuthor"))
               .Add("AVG Price By Genre", () => StatMethods("stat/avgPriceByGenre"))
               .Add("All Price By Genre", () => StatMethods("stat/allPriceByGenre"))
               .Add("All Price By Years", () => StatMethods("stat/allPriceByYears"))
               .Add("AVG Rate By Genre", ()=> StatMethods("stat/avgRateByGenre"))
               .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Mangas", ()=> mangaSubMenu.Show())
                .Add("Author", ()=> authorSubMenu.Show())
                .Add("Genre", ()=> genreSubMenu.Show())
                .Add("Other functions", ()=> nonCrudSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
