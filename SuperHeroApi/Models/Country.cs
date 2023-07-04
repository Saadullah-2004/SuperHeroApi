using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperHeroApi.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        public string CountryName { get; set; } = string.Empty;
    }
}
