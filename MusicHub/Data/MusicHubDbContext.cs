using Microsoft.EntityFrameworkCore;
using MusicHub.Data.Models;

namespace MusicHub.Data
{
    public class MusicHubDbContext : DbContext
    {
        public MusicHubDbContext()
        {
        }

        public MusicHubDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Writer> Writers { get; set; }

        public DbSet<SongPerformer> SongPerformers { get; set; }

        public DbSet<Performer> Performers { get; set; }

        public DbSet<Song> Songs { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Producer> Producers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Song>(entity =>
            {
                entity
                    .Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");

                entity
                    .HasMany(x => x.SongPerformers)
                    .WithOne(x => x.Song);
            });

            builder.Entity<Performer>(entity =>
            {
                entity
                    .Property(p => p.NetWorth)
                    .HasColumnType("decimal(18,2)");

                entity
                    .HasMany(x => x.SongPerformers)
                    .WithOne(x => x.Performer);
            });

            builder.Entity<Writer>(entity =>
            {
                entity
                    .HasMany(x => x.Songs)
                    .WithOne(x => x.Writer);
            });

            builder.Entity<Album>(entity =>
            {
                entity
                    .HasMany(x => x.Songs)
                    .WithOne(x => x.Album);
            });

            builder.Entity<Producer>(entity =>
            {
                entity
                    .HasMany(x => x.Albums)
                    .WithOne(x => x.Producer);
            });


            builder.Entity<SongPerformer>(e =>
            {
                e.HasKey(sc => new { sc.PerformerId, sc.SongId });

                e.HasOne(sc => sc.Performer)
                  .WithMany(s => s.SongPerformers)
                  .HasForeignKey(sc => sc.PerformerId);

                e.HasOne(sc => sc.Song)
                   .WithMany(s => s.SongPerformers)
                   .HasForeignKey(sc => sc.SongId);
            });

            base.OnModelCreating(builder);
        }
    }
}
