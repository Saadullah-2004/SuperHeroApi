using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroApi.Models
{
    public class SuperHero
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Place { get; set; } = string.Empty;

        [ForeignKey("Country")]
        public int? CountryId { get; set; } // Foreign key property

        public Country Country { get; set; } // Navigation property

        [ForeignKey("Genre")]
        public int? GenreId { get; set; } // Foreign key property

        public Genre Genre { get; set; } // Navigation property
    }
}
