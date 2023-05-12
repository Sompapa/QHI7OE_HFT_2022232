using QHI7OE_HFT_2022232.Models;
using QHI7OE_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHI7OE_HFT_2022232.Logic
{
    public class GenreLogic : IGenreLogic
    {
        IRepository<Genre> repo;

        public GenreLogic(IRepository<Genre> repo)
        {
            this.repo = repo;
        }

        public void Create(Genre item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Genre Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Genre> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Genre item)
        {
            this.repo.Update(item);
        }
    }
}
