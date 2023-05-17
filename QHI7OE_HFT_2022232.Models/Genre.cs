using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QHI7OE_HFT_2022232.Models
{
    [Table("genres")]
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }

        [StringLength(240)]
        [Required]
        public string GenreName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Manga> Mangas { get; set; }

        public Genre()
        {
              Mangas = new HashSet<Manga>();
        }
        public Genre(string line)
        {
            string[] split = line.Split('#');
            GenreId = int.Parse(split[0]);
            GenreName = split[1];
            Mangas = new HashSet<Manga>();
        }

    }
}
