using QHI7OE_HFT_2022232.Models;
using System.Linq;

namespace QHI7OE_HFT_2022232.Logic
{
    public interface IGenreLogic
    {
        void Create(Genre item);
        void Delete(int id);
        Genre Read(int id);
        IQueryable<Genre> ReadAll();
        void Update(Genre item);
    }
}