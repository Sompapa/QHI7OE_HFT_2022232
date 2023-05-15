using ConsoleTools;
using QHI7OE_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace QHI7OE_HFT_2022232.Client
{ 
    internal class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
           
        }
        static void List(string entity)
        {
            if (entity == "Author")
            {
                List<Author> authors = rest.Get<Author>("authors");
                foreach (var item in authors)
                {
                    Console.WriteLine(item.AuthorName);
                }
            }
            Console.ReadLine();
        }

        static void Update(string entity)
        {
           
        }
        static void Delete(string entity)
        {
          
        }
        static void StatMethods(string entity)
        {

          
        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:59073", "manga");

            var mangaSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Manga"))
                .Add("Create", () => Create("Manga"))
                .Add("Update", () => Update("Manga"))
                .Add("Delete", () => Delete("Manga"))
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Mangas", ()=> mangaSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
