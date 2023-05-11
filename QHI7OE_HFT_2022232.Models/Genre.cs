using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int MangaId { get; set; }

        public virtual ICollection<Manga> Mangas { get; set; }

    }
}
