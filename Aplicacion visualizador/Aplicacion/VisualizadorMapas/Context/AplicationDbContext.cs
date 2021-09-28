using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VisualizadorMapas.Model;

namespace VisualizadorMapas.Context
{
    public partial class AplicationDbContext : DbContext
    {
        public AplicationDbContext()
        {
        }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Incidentes> Incidentes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Incidentes>(entity =>
            {
                entity.Property(e => e.Calle1).HasMaxLength(100);

                entity.Property(e => e.Calle2).HasMaxLength(100);

                entity.Property(e => e.Comuna).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
