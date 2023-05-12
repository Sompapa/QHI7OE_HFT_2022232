using QHI7OE_HFT_2022232.Models;
using System.Linq;

namespace QHI7OE_HFT_2022232.Logic
{
    public interface IAuthorLogic
    {
        void Create(Author item);
        void Delete(int id);
        Author Read(int id);
        IQueryable<Author> ReadAll();
        void Update(Author item);
    }
}