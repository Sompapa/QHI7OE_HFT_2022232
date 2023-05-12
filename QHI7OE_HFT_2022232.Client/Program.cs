using QHI7OE_HFT_2022232.Logic;
using QHI7OE_HFT_2022232.Models;
using QHI7OE_HFT_2022232.Repository;
using System;
using System.Linq;


namespace QHI7OE_HFT_2022232.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ctx = new MangaDbContext();

            var mangaRepo = new MangaRepository(ctx);
            var authorRepo = new AuthorRepository(ctx);
            var genreRepo = new GenreRepository(ctx);

            var mangaLogic = new MangaLogic(mangaRepo);
            var authorLogic = new AuthorLogic(authorRepo);
            var genreLogic = new GenreLogic(genreRepo);
        }
    }
}
