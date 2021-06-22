using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EventBooking.Models
{
    public partial class TicketBookingContext : DbContext
    {
        public TicketBookingContext()
        {
        }

        public TicketBookingContext(DbContextOptions<TicketBookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EventInfo> EventInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-OOV8U7V;Database=TicketBooking;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<EventInfo>(entity =>
            {
                entity.HasKey(e => e.RecId)
                    .HasName("PK__EventInf__360414FFD6EC05B3");

                entity.ToTable("EventInfo");

                entity.Property(e => e.RecId).HasColumnName("RecID");

                entity.Property(e => e.EventId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EventID");

                entity.Property(e => e.EventName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EventStatus)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EventTimings)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
