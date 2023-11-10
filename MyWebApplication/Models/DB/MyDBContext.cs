using Microsoft.EntityFrameworkCore;

namespace MyWebApplication.Models.DB
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
        {
        }

        public MyDBContext(DbContextOptions<MyDBContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<SystemUsers> SystemUsers { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // warning To protect potentially sensitive information in your connection string,
                // you should move it out of source code.See http://go.microsoft.com/fwlink/?LinkId=723263
                // for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MyNewDB;Initial Catalog=DemoDB;Integrated Security=True;Multiple Active Result Sets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("SYSUserProfile");

                entity.HasKey(e => new { e.ProfileID, e.UserID });

                entity.Property(e => e.ProfileID)
               .HasColumnName("SYSUserProfileID")
                  .UseIdentityColumn()
                .HasColumnType("int");

                entity.Property(e => e.UserID)
                .HasColumnName("SYSUserID")
                .HasColumnType("int");

                entity.Property(e => e.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.Gender)
                .HasColumnName("Gender")
                .HasColumnType("char(1)");

                entity.Property(e => e.CreatedBy)
                .HasColumnName("RowCreatedSYSUserID")
                .HasColumnType("int");

                entity.Property(e => e.CreatedDateTime)
                .HasColumnName("RowCreatedDateTime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedBy)
                .HasColumnName("RowModifiedSYSUserID")
                .HasColumnType("int");

                entity.Property(e => e.ModifiedDateTime)
                .HasColumnName("RowModifiedDateTime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.AccountImage)
                .HasColumnName("AccountImage")
                .HasMaxLength(8000)
                .IsUnicode(false)
                 .IsRequired(false);
            });

            modelBuilder.Entity<SystemUsers>(entity =>
            {
                entity.ToTable("SYSUser");

                entity.Property(e => e.UserID)
                .HasColumnName("SYSUserID")
                .HasColumnType("int");

                entity.Property(e => e.LoginName)
                .HasColumnName("LoginName")
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                .HasColumnName("RowCreatedSYSUserID")
                .HasColumnType("int");

                entity.Property(e => e.CreatedDateTime)
                .HasColumnName("RowCreatedDateTime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedBy)
                .HasColumnName("RowModifiedSYSUserID")
                .HasColumnType("int");

                entity.Property(e => e.ModifiedDateTime)
                .HasColumnName("RowModifiedDateTime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("SYSUserRole");

                entity.HasKey(e => new { e.RoleID });

                entity.Property(e => e.RoleID)
                .HasColumnName("SYSUserRoleID")
                .HasColumnType("int");

                entity.Property(e => e.UserID)
                .HasColumnName("SYSUserID")
                .HasColumnType("int");

                entity.Property(e => e.LookUpRoleID)
                .HasColumnName("LOOKUPRoleID")
                .HasColumnType("int");

                entity.Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit");

                entity.Property(e => e.CreatedBy)
                .HasColumnName("RowCreatedSYSUserID")
                .HasColumnType("int");

                entity.Property(e => e.CreatedDateTime)
                .HasColumnName("RowCreatedDateTime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedBy)
                .HasColumnName("RowModifiedSYSUserID")
                .HasColumnType("int");

                entity.Property(e => e.ModifiedDateTime)
                .HasColumnName("RowModifiedDateTime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("LOOKUPRole");

                entity.Property(e => e.RoleID)
                .HasColumnName("LOOKUPRoleID")
                .HasColumnType("int");

                entity.Property(e => e.RoleName)
                .HasColumnName("RoleName")
                .HasMaxLength(100)
                .IsUnicode(false);

                entity.Property(e => e.RoleDescription)
                .HasColumnName("RoleDescription")
                .HasMaxLength(500)
                .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                .HasColumnName("RowCreatedSYSUserID")
                .HasColumnType("int");

                entity.Property(e => e.CreatedDateTime)
                .HasColumnName("RowCreatedDateTime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedBy)
                .HasColumnName("RowModifiedSYSUserID")
                .HasColumnType("int");

                entity.Property(e => e.ModifiedDateTime)
                .HasColumnName("RowModifiedDateTime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            });
        }
    }
}

