using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QHI7OE_HFT_2022232.Models
{
    [Table("mangas")]
    public class Manga
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MangaId { get; set; }

        [Required]
        [StringLength(240)]
        public string Title { get; set; }

        public double Price { get; set; }

        [Range (0, 10)]
        public double Rating { get; set; }

        public DateTime Release { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public virtual Genre Genre { get; set; }

        public Manga()
        {

        }
    }
}
