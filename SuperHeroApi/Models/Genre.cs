using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperHeroApi.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        public string GenreName { get; set; } = string.Empty;

    }
}
