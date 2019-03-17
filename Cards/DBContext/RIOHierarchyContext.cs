using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cards.DBContext
{
    public partial class RIOHierarchyContext : DbContext
    {
        public RIOHierarchyContext()
        {
        }

        public RIOHierarchyContext(DbContextOptions<RIOHierarchyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IntegrationEventLog> IntegrationEventLog { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=HYDHTC135611L;Database=RIO.Hierarchy;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntegrationEventLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content).IsUnicode(false);

                entity.Property(e => e.CorrelationId)
                    .IsRequired()
                    .HasColumnName("CorrelationID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.EventTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IntegrationEvent)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CardHolderName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CardId)
                    .HasColumnName("CardID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CorrelationId)
                    .IsRequired()
                    .HasColumnName("CorrelationID")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
