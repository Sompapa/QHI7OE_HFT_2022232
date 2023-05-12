using QHI7OE_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHI7OE_HFT_2022232.Repository
{
    public class AuthorRepository : Repository<Author>, IRepository<Author>
    {
        public AuthorRepository(MangaDbContext ctx) : base(ctx)
        {
        }

        public override Author Read(int id)
        {
            return ctx.Authors.FirstOrDefault(t => t.AuthorId == id);
        }

        public override void Update(Author item)
        {
            var old = Read(item.AuthorId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
