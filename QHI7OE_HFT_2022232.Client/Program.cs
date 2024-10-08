﻿using ConsoleTools;
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
                Console.WriteLine("New Author Created: " + id + ", " + name);
                Console.ReadLine();
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
                rest.Post(new Manga() { MangaId = id, Title = title, Price = price, Rating = rating, Release = reales, AuthorId = authorid, GenreId = genreid }, "manga");
                Console.WriteLine("New Manga Created: " + id + ", " + title);
                Console.ReadLine();
            }
            else if (entity == "Genre")
            {
                Console.WriteLine("Enter Genre ID: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Genre name:");
                string name = Console.ReadLine();
                rest.Post(new Genre() { GenreId = id, GenreName = name }, "genre");
                Console.WriteLine("New Genre Created: " + id + ", " + name);
                Console.ReadLine();
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
                Console.WriteLine("Author Updated");
                Console.ReadLine();
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
                Console.WriteLine("Manga Updated");
                Console.ReadLine();
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
                Console.WriteLine("Genre Updated");
                Console.ReadLine();
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Author")
            {
                Console.WriteLine("Enter Author ID to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "author");
                Console.WriteLine("Author Deleted on ID: " +id);
                Console.ReadLine();

            }
            else if (entity == "Manga")
            {
                Console.WriteLine("Enter Manga ID to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "manga");
                Console.WriteLine("Manga Deleted on ID: " +id);
                Console.ReadLine();
            }
            else if (entity == "Genre")
            {
                Console.WriteLine("Enter Genre ID to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "genre");
                Console.WriteLine("Genre Deleted on ID" + id);
                Console.ReadLine();
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
               .Add("AVG Manga Prices By Author", () => StatMethods("stat/avgPriceByAuthor"))
               .Add("AVG Manga Prices By Genre", () => StatMethods("stat/avgPriceByGenre"))
               .Add("SUM Manga Prices By Genre", () => StatMethods("stat/allPriceByGenre"))
               .Add("SUM Manga Prices By Years", () => StatMethods("stat/allPriceByYears"))
               .Add("AVG Manga Rating By Genre", ()=> StatMethods("stat/avgRateByGenre"))
               .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("MANGAS", ()=> mangaSubMenu.Show())
                .Add("AUTHORS", ()=> authorSubMenu.Show())
                .Add("GENRES", ()=> genreSubMenu.Show())
                .Add("OTHER FUNCTIONS", ()=> nonCrudSubMenu.Show())
                .Add("EXIT", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
