using HotelListing.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    public class CountryDto : CreateCountryDto
    {
        public int Id { get; set; }
        public IList<Hotel> Hotels { get; set; }
    }

    public class CreateCountryDto
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country name is too long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 2, ErrorMessage = "Short Country name is too long")]
        public string ShortName { get; set; }
    }
}
