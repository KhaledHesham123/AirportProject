using FlyingProject.Project.core.Entities.main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlyingProject.Project.Repo.Data.Configration
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
           
            // Properties
            builder.Property(s => s.SeatNumber)
                   .IsRequired()
                   .HasMaxLength(10);

            // تحويل Enum لـ string
            

            builder.Property(s => s.FlightId)
                   .IsRequired();

        }
    }
}
