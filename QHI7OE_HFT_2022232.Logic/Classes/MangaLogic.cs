using System;
using System.Collections.Generic;
using System.Linq;
using QHI7OE_HFT_2022232.Models;
using QHI7OE_HFT_2022232.Repository;

namespace QHI7OE_HFT_2022232.Logic
{
    public class MangaLogic : IMangaLogic
    {
        IRepository<Manga> repo;

        public MangaLogic(IRepository<Manga> repo)
        {
            this.repo = repo;
        }

        //non crud

        public IEnumerable<KeyValuePair<string, double>> AVGPriceByAuthor()
        {
            return from manga in repo.ReadAll()
                   group manga by manga.Author.AuthorName into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Average(t => t.Price) ?? -1);
        }

        public IEnumerable<KeyValuePair<string, double>> AVGPriceByGenre()
        {
            return from manga in repo.ReadAll()
                   group manga by manga.Genre.GenreName into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Average(t => t.Price) ?? -1);
        }

        public IEnumerable<KeyValuePair<string, double>> AllPriceByGenre()
        {
            return from manga in repo.ReadAll()
                   group manga by manga.Genre.GenreName into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Sum(t => t.Price) ?? -1);
        }

        public IEnumerable<KeyValuePair<DateTime, double>> AVGPriceByYears()
        {
            return from manga in repo.ReadAll()
                   group manga by manga.Release into g
                   select new KeyValuePair<DateTime, double>
                   (g.Key, g.Average(t => t.Price) ?? -1);
        }

        public IEnumerable<KeyValuePair<DateTime, double>> AllPriceByYears()
        {
            return from manga in repo.ReadAll()
                   group manga by manga.Release into g
                   select new KeyValuePair<DateTime, double>
                   (g.Key, g.Sum(t => t.Price) ?? -1);
        }
        public void Create(Manga item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Manga Read(int id)
        {
            var manga = this.repo.Read(id);
            if (manga == null)
            {
                throw new ArgumentException("This Manga does not exist");
            }
            return manga;
        }

        public IQueryable<Manga> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Manga item)
        {
            this.repo.Update(item);
        }
    }
}
