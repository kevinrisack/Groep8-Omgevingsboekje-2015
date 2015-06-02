namespace DigitaalOmgevingsboek
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OmgevingsboekContext : DbContext
    {
        public OmgevingsboekContext()
            : base("name=OmgevingsboekContext")
        {
        }

        public virtual DbSet<Activiteit> Activiteit { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Doelgroep> Doelgroep { get; set; }
        public virtual DbSet<Foto_Activiteit> Foto_Activiteit { get; set; }
        public virtual DbSet<Foto_POI> Foto_POI { get; set; }
        public virtual DbSet<Leerdoel> Leerdoel { get; set; }
        public virtual DbSet<Link> Link { get; set; }
        public virtual DbSet<POI> POI { get; set; }
        public virtual DbSet<POI_Log> POI_Log { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<Thema> Thema { get; set; }
        public virtual DbSet<Uitstap> Uitstap { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activiteit>()
                .HasMany(e => e.Foto_Activiteit)
                .WithRequired(e => e.Activiteit)
                .HasForeignKey(e => e.Activiteit_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activiteit>()
                .HasMany(e => e.Link)
                .WithRequired(e => e.Activiteit)
                .HasForeignKey(e => e.Activiteit_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activiteit>()
                .HasMany(e => e.Leerdoel)
                .WithMany(e => e.Activiteit)
                .Map(m => m.ToTable("Activiteit_Leerdoel"));

            modelBuilder.Entity<Activiteit>()
                .HasMany(e => e.Doelgroep)
                .WithMany(e => e.Activiteit)
                .Map(m => m.ToTable("Doelgroep_Activiteit"));

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.POI)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.Auteur_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Uitstap)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.Auteur_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Uitstap1)
                .WithMany(e => e.AspNetUsers1)
                .Map(m => m.ToTable("Uitstap_Gebruiker").MapLeftKey("Gebruiker_Id"));

            modelBuilder.Entity<Doelgroep>()
                .HasMany(e => e.POI)
                .WithMany(e => e.Doelgroep)
                .Map(m => m.ToTable("POI_Doelgroep"));

            modelBuilder.Entity<POI>()
                .Property(e => e.Toegangsprijs)
                .HasPrecision(2, 0);

            modelBuilder.Entity<POI>()
                .HasOptional(e => e.Activiteit)
                .WithRequired(e => e.POI);

            modelBuilder.Entity<POI>()
                .HasMany(e => e.Foto_POI)
                .WithRequired(e => e.POI)
                .HasForeignKey(e => e.POI_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<POI>()
                .HasMany(e => e.POI_Log)
                .WithRequired(e => e.POI)
                .HasForeignKey(e => e.POI_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<POI>()
                .HasMany(e => e.Rating)
                .WithOptional(e => e.POI)
                .HasForeignKey(e => e.POI_Id);

            modelBuilder.Entity<POI>()
                .HasMany(e => e.Thema)
                .WithMany(e => e.POI)
                .Map(m => m.ToTable("POI_Thema"));

            modelBuilder.Entity<POI>()
                .HasMany(e => e.Uitstap)
                .WithMany(e => e.POI)
                .Map(m => m.ToTable("Uitstap_POI"));

            modelBuilder.Entity<POI_Log>()
                .Property(e => e.Time)
                .IsFixedLength();

            modelBuilder.Entity<Route>()
                .HasMany(e => e.Uitstap)
                .WithOptional(e => e.Route)
                .HasForeignKey(e => e.Route_Id);
        }
    }
}
