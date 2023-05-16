using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QHI7OE_HFT_2022232.Models
{
    [Table("authors")]
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(50)]
        public string AuthorName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Manga> Mangas { get; set; }

        public Author()
        {
           Mangas = new HashSet<Manga>();
        }

        public Author(string line)
        {
            string[] split = line.Split('#');
            AuthorId = int.Parse(split[0]);
            AuthorName = split[1];
            Mangas = new HashSet<Manga>();
        }
    }
}
