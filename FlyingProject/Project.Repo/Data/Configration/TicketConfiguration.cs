using FlyingProject.Project.core.Entities.main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlyingProject.Project.Repo.Data.Configration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(t => t.Price)
                  .IsRequired()
                  .HasColumnType("decimal(18,2)");

            builder.HasOne(x=>x.Seat).WithOne().HasForeignKey<Ticket>(t => t.SeatId);

        }
    }
}
