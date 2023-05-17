using QHI7OE_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QHI7OE_HFT_2022232.Logic
{
    public interface IMangaLogic
    {
        IEnumerable<KeyValuePair<string, double>> AllPriceByGenre();
        IEnumerable<KeyValuePair<DateTime, double>> AllPriceByYears();
        IEnumerable<KeyValuePair<string, double>> AVGPriceByAuthor();
        IEnumerable<KeyValuePair<string, double>> AVGPriceByGenre();
        IEnumerable<KeyValuePair<string, double>> AVGRateByGenre();
        void Create(Manga item);
        void Delete(int id);
        Manga Read(int id);
        IQueryable<Manga> ReadAll();
        void Update(Manga item);
    }
}