using QHI7OE_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHI7OE_HFT_2022232.Repository
{
    public class MangaRepository : Repository<Manga>, IRepository<Manga>
    {
        public MangaRepository(MangaDbContext ctx) : base(ctx)
        {
        }

        public override Manga Read(int id)
        {
            return ctx.Mangas.FirstOrDefault(t => t.MangaId == id);
        }

        public override void Update(Manga item)
        {
            var old = Read(item.MangaId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
