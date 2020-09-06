using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace API_SECURITY.Models
{
    public partial class PICA_SECURITYContext : DbContext
    {
        private readonly IConfiguration _config;

        public PICA_SECURITYContext()
        {
        }

        public PICA_SECURITYContext(DbContextOptions<PICA_SECURITYContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        public virtual DbSet<PicRoleUser> PicRoleUser { get; set; }
        public virtual DbSet<PicRoles> PicRoles { get; set; }
        public virtual DbSet<PicUsers> PicUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetValue<String>("ConnectionStrings:ConnectionSecurity"));
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PicRoleUser>(entity =>
            {
                entity.HasKey(e => e.RusNcode)
                    .HasName("PK__PIC_ROLE__2A03E1E9752257DE");

                entity.ToTable("PIC_ROLE_USER");

                entity.Property(e => e.RusNcode).HasColumnName("RUS_NCODE");

                entity.Property(e => e.RolNcode).HasColumnName("ROL_NCODE");

                entity.Property(e => e.UseNcode).HasColumnName("USE_NCODE");

                entity.HasOne(d => d.RolNcodeNavigation)
                    .WithMany(p => p.PicRoleUser)
                    .HasForeignKey(d => d.RolNcode)
                    .HasConstraintName("FK__PIC_ROLE___ROL_N__3C69FB99");

                entity.HasOne(d => d.UseNcodeNavigation)
                    .WithMany(p => p.PicRoleUser)
                    .HasForeignKey(d => d.UseNcode)
                    .HasConstraintName("FK__PIC_ROLE___USE_N__3B75D760");
            });

            modelBuilder.Entity<PicRoles>(entity =>
            {
                entity.HasKey(e => e.RolNcode)
                    .HasName("PK__PIC_ROLE__A629DFC63B958D22");

                entity.ToTable("PIC_ROLES");

                entity.Property(e => e.RolNcode)
                    .HasColumnName("ROL_NCODE")
                    .ValueGeneratedNever();

                entity.Property(e => e.RolCdescription)
                    .HasColumnName("ROL_CDESCRIPTION")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PicUsers>(entity =>
            {
                entity.HasKey(e => e.UseNcode)
                    .HasName("PK__PIC_USER__1EE0D216F9B59C39");

                entity.ToTable("PIC_USERS");

                entity.Property(e => e.UseNcode).HasColumnName("USE_NCODE");

                entity.Property(e => e.UseCaddress)
                    .HasColumnName("USE_CADDRESS")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UseCcity)
                    .HasColumnName("USE_CCITY")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UseCcountry)
                    .HasColumnName("USE_CCOUNTRY")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UseCemail)
                    .HasColumnName("USE_CEMAIL")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UseCname)
                    .HasColumnName("USE_CNAME")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UseCpassword)
                    .HasColumnName("USE_CPASSWORD")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UseCsurname)
                    .HasColumnName("USE_CSURNAME")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
