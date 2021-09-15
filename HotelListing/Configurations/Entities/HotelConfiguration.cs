using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                 new Hotel
                 {
                     Id = 1,
                     Name = "Hotel 1",
                     Address = "Address 1",
                     CountryId = 1,
                     Rating = 3.5
                 },
                 new Hotel
                 {
                     Id = 2,
                     Name = "Hotel 2",
                     Address = "Address 2",
                     CountryId = 2,
                     Rating = 4
                 },
                 new Hotel
                 {
                     Id = 3,
                     Name = "Hotel 3",
                     Address = "Address 3",
                     CountryId = 1,
                     Rating = 2.5
                 }
                 );
        }
    }
}
