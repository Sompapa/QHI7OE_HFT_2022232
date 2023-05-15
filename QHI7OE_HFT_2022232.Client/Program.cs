using ConsoleTools;
using QHI7OE_HFT_2022232.Logic;
using QHI7OE_HFT_2022232.Models;
using QHI7OE_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace QHI7OE_HFT_2022232.Client
{ 
    internal class Program
    {
        static void Create(string entity)
        {
            //if (entity == "Genre")
            //{ 
            //    Console.Write("Enter Genre Id: ");
            //    int shapeId = Convert.ToInt32(Console.ReadLine());
            //    Console.Write("Enter Genre name: ");
            //    string name = Console.ReadLine();
            //    //rest.Post(new Shape() { Name = name, ShapeId = shapeId }, "shape");
            //    //rest.Post(new Shape() { Name = name }, "shape");
            //}
            //if (entity == "Author")
            //{
            //    Console.Write("Enter Author Id: ");
            //    int basePrice = int.Parse(Console.ReadLine());
            //    Console.Write("Enter Author's name: ");
            //    string name = Console.ReadLine();
            //    //int id = rest.Get<Guitar>("guitar").Count() + 1;
            //    //rest.Post(new Guitar() { BasePrice = basePrice, BrandId = brandId, Year = year, ShapeId = shapeId, Id = id }, "guitar");
            //}
            //if (entity == "Manga")
            //{
            //    Console.Write("Enter Manga Id: ");
            //    int id = int.Parse(Console.ReadLine());
            //    Console.Write("Enter Manga Title: ");
            //    string name = Console.ReadLine();
            //    Console.WriteLine("Eneter Price: ");
            //    double price = double.Parse(Console.ReadLine());
            //    Console.WriteLine("Enter Manga rating: ");
            //    double rating = double.Parse(Console.ReadLine());
            //    Console.WriteLine("Enter Release date: ");
            //    DateTime release = DateTime.Parse(Console.ReadLine());
            //    //rest.Post(new Brand() { Name = name, BrandId = brandId }, "brand");
            //}
        }
        static void List(string entity)
        {
            //if (entity == "Brand")
            //{
            //    //List<Brand> brands = rest.Get<Brand>("brand");
            //    foreach (var item in brands)
            //    {
            //        Console.WriteLine(item.Name);
            //    }
            //}
            //else if (entity == "Guitar")
            //{
            //    //List<Guitar> guitars = rest.Get<Guitar>("guitar");
            //    foreach (var item in guitars)
            //    {
            //        Console.WriteLine(item.Id + ": " + item.Brand.Name + ": " + item.Shape.Name + ": " + item.BasePrice);
            //    }
            //}
            //else if (entity == "Shape")
            //{
            //    //List<Shape> shapes = rest.Get<Shape>("shape");
            //    foreach (var item in shapes)
            //    {
            //        Console.WriteLine(item.Name);
            //    }
            //}
            //Console.ReadLine();
        }

        static void Update(string entity)
        {
            //if (entity == "Brand")
            //{
            //    Console.Write("Enter Brand's id to update: ");
            //    int id = int.Parse(Console.ReadLine());
            //    Brand one = rest.Get<Brand>(id, "brand");
            //    Console.Write($"New name [old: {one.Name}]: ");
            //    string name = Console.ReadLine();
            //    one.Name = name;
            //    rest.Put(one, "brand");
            //}
            //if (entity == "Guitar")
            //{
            //    Console.Write("Enter Guitar's id to update its Base Price: ");
            //    int id = int.Parse(Console.ReadLine());
            //    Guitar one = rest.Get<Guitar>(id, "guitar");
            //    Console.Write($"New Price [old: {one.BasePrice}]: ");
            //    int basePrice = Convert.ToInt32(Console.ReadLine());
            //    one.BasePrice = basePrice;
            //    rest.Put(one, "guitar");
            //}
            //if (entity == "Shape")
            //{
            //    Console.Write("Enter Shape's id to update: ");
            //    int id = int.Parse(Console.ReadLine());
            //    Shape one = rest.Get<Shape>(id, "shape");
            //    Console.Write($"New name [old: {one.Name}]: ");
            //    string name = Console.ReadLine();
            //    one.Name = name;
            //    rest.Put(one, "shape");
            //}
        }
        static void Delete(string entity)
        {
            //if (entity == "Brand")
            //{
            //    Console.Write("Enter Brand's id to delete: ");
            //    int id = int.Parse(Console.ReadLine());
            //    rest.Delete(id, "brand");
            //}
            //if (entity == "Guitar")
            //{
            //    Console.Write("Enter Guitar's id to delete: ");
            //    int id = int.Parse(Console.ReadLine());
            //    rest.Delete(id, "guitar");
            //}
            //if (entity == "Shape")
            //{
            //    Console.Write("Enter Shape's id to delete: ");
            //    int id = int.Parse(Console.ReadLine());
            //    rest.Delete(id, "shape");
            //}
        }
        static void StatMethods(string entity)
        {

            //if (entity == "stat/avgpricebybrands" || entity == "stat/AVGPriceByShapes" || entity == "stat/AllPriceOfGuitarsByBrands")
            //{
            //    var guitars = rest.Get<KeyValuePair<string, double>>(entity);
            //    foreach (var item in guitars)
            //    {
            //        Console.WriteLine(item.Key + ": " + item.Value);
            //    }
            //}
            //else if (entity == "stat/AVGPriceByYears" || entity == "stat/AllPriceByYears")
            //{
            //    var guitars = rest.Get<KeyValuePair<int, double>>(entity);
            //    foreach (var item in guitars)
            //    {
            //        Console.WriteLine(item.Key + ": " + item.Value);
            //    }
            //}
            //else
            //{
            //    throw new ArgumentException("No such Non-CRUD method!");
            //}
            //Console.ReadLine();

        }
        static void Main(string[] args)
        {
            var ctx = new MangaDbContext();

            var mangaRepo = new MangaRepository(ctx);
            var authorRepo = new AuthorRepository(ctx);
            var genreRepo = new GenreRepository(ctx);

            var mangaLogic = new MangaLogic(mangaRepo);
            var authorLogic = new AuthorLogic(authorRepo);
            var genreLogic = new GenreLogic(genreRepo);

            var valami = mangaLogic.AVGPriceByYears().ToArray();

            ;


            var mangaSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Brand"))
                .Add("Create", () => Create("Brand"))
                .Add("Delete", () => Delete("Brand"))
                .Add("Update", () => Update("Brand"))
                .Add("Exit", ConsoleMenu.Close);

            var nonCrudMethods = new ConsoleMenu(args, level: 1)
                .Add("AVGPriceByBrands", () => StatMethods("stat/avgpricebybrands"))
                .Add("AVGPriceByShapes", () => StatMethods("stat/AVGPriceByShapes"))
                .Add("AllPriceOfGuitarsByBrands", () => StatMethods("stat/AllPriceOfGuitarsByBrands"))
                .Add("AVGPriceByYears", () => StatMethods("stat/AVGPriceByYears"))
                .Add("AllPriceByYears", () => StatMethods("stat/AllPriceByYears"))
                .Add("Exit", ConsoleMenu.Close);

            var authorSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Guitar"))
                .Add("Create", () => Create("Guitar"))
                .Add("Delete", () => Delete("Guitar"))
                .Add("Update", () => Update("Guitar"))
                .Add("Exit", ConsoleMenu.Close);

            var GenreSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Shape"))
                .Add("Create", () => Create("Shape"))
                .Add("Delete", () => Delete("Shape"))
                .Add("Update", () => Update("Shape"))
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Mangas", () => mangaSubMenu.Show())
                .Add("Authors", () => authorSubMenu.Show())
                .Add("Genres", () => GenreSubMenu.Show())
                .Add("Non-CRUD Methods", () => nonCrudMethods.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
