using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using on_e_commerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class dbEticaretContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public dbEticaretContext()
        {
        }

        public dbEticaretContext(DbContextOptions<dbEticaretContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ilceler> Ilcelers { get; set; }
        public virtual DbSet<Sehirler> Sehirlers { get; set; }
        public virtual DbSet<SemtMah> SemtMahs { get; set; }
        public virtual DbSet<TblKategori> TblKategoris { get; set; }
        public virtual DbSet<TblRoller> TblRollers { get; set; }
        public virtual DbSet<TblSepet> TblSepets { get; set; }
        public virtual DbSet<TblSepetDurumu> TblSepetDurumus { get; set; }
        public virtual DbSet<TblSiparisDetay> TblSiparisDetays { get; set; }
        public virtual DbSet<TblSiparisDurumu> TblSiparisDurumus { get; set; }
        public virtual DbSet<TblSiparisUrunleri> TblSiparisUrunleris { get; set; }
        public virtual DbSet<TblSlideImage> TblSlideImages { get; set; }
        public virtual DbSet<TblUrun> TblUruns { get; set; }
        public virtual DbSet<TblUye> TblUyes { get; set; }
        public virtual DbSet<TblUyeRol> TblUyeRols { get; set; }
        public virtual DbSet<Ulkeler> Ulkelers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-INON5PI\\SQLEXPRESS;Database=dbEticaret;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Ilceler>(entity =>
            {
                entity.HasKey(e => e.ilceId);

                entity.ToTable("Ilceler");

                entity.Property(e => e.ilceId)
                    .ValueGeneratedNever()
                    .HasColumnName("ilceId");

                entity.Property(e => e.IlceAdi)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.SehirAdi)
                    .IsRequired()
                    .HasMaxLength(55);

                entity.HasOne(d => d.Sehir)
                    .WithMany(p => p.Ilcelers)
                    .HasForeignKey(d => d.SehirId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ilceler_Sehirler");
            });

            modelBuilder.Entity<Sehirler>(entity =>
            {
                entity.HasKey(e => e.SehirId);

                entity.ToTable("Sehirler");

                entity.Property(e => e.SehirId).ValueGeneratedNever();

                entity.Property(e => e.SehirAdi)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<SemtMah>(entity =>
            {
                entity.ToTable("SemtMah");

                entity.Property(e => e.SemtMahId).ValueGeneratedNever();

                entity.Property(e => e.IlceId).HasColumnName("ilceId");

                entity.Property(e => e.MahalleAdi)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PostaKodu)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.SemtAdi)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.HasOne(d => d.Ilce)
                    .WithMany(p => p.SemtMahs)
                    .HasForeignKey(d => d.IlceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SemtMah_Ilceler");
            });

            modelBuilder.Entity<TblKategori>(entity =>
            {
                entity.HasKey(e => e.KategoriId)
                    .HasName("PK__Tbl_Kate__1782CC7219BD4E2E");

                entity.ToTable("TblKategori");

                entity.HasIndex(e => e.KategoriAdi, "UQ__Tbl_Kate__110FF79E49F82EF3")
                    .IsUnique();

                entity.Property(e => e.KategoriAdi)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblRoller>(entity =>
            {
                entity.HasKey(e => e.RolId)
                    .HasName("PK__Tbl_Roll__F92302F15DF9128C");

                entity.ToTable("TblRoller");

                entity.HasIndex(e => e.RolAdi, "UQ__Tbl_Roll__85F2635D39D9BE7B")
                    .IsUnique();

                entity.Property(e => e.RolAdi)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSepet>(entity =>
            {
                entity.HasKey(e => e.SepetId)
                    .HasName("PK__tbl_Sepe__97CA09F36BC2F035");

                entity.ToTable("TblSepet");

                entity.HasOne(d => d.Urun)
                    .WithMany(p => p.TblSepets)
                    .HasForeignKey(d => d.UrunId)
                    .HasConstraintName("FK__tbl_Sepet__UrunI__38996AB5");
            });

            modelBuilder.Entity<TblSepetDurumu>(entity =>
            {
                entity.HasKey(e => e.SepetDurumuId)
                    .HasName("PK__tbl_Sepe__AEE24F2C43718A67");

                entity.ToTable("TblSepetDurumu");

                entity.Property(e => e.SepetDurumu)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSiparisDetay>(entity =>
            {
                entity.HasKey(e => e.SiparisDetayId)
                    .HasName("PK__Tbl_Sipa__DA4BDFD21CD8D989");

                entity.ToTable("TblSiparisDetay");

                entity.Property(e => e.Ad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Adres)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ilce)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OdemeTipi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostaKodu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sehir)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SiparisBarkod)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SiparisTarihi).HasColumnType("datetime");

                entity.Property(e => e.Soyad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonNo)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Ulke)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SiparisDurumuNavigation)
                    .WithMany(p => p.TblSiparisDetays)
                    .HasForeignKey(d => d.SiparisDurumu)
                    .HasConstraintName("FK_Tbl_SiparisDetay_Tbl_SiparisDurumu");

                
            });

            modelBuilder.Entity<TblSiparisDurumu>(entity =>
            {
                entity.ToTable("TblSiparisDurumu");

                entity.Property(e => e.SiparisDurumu)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSiparisUrunleri>(entity =>
            {
                entity.HasKey(e => e.SiparisId)
                    .HasName("PK__Tbl_Sipa__3214EC07D9F30590");

                entity.ToTable("TblSiparisUrunleri");

                entity.Property(e => e.SiparisBarkod)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Urun)
                    .WithMany(p => p.TblSiparisUrunleris)
                    .HasForeignKey(d => d.UrunId)
                    .HasConstraintName("FK_Tbl_SiparisUrunleri_Tbl_Urun");

                
            });

            modelBuilder.Entity<TblSlideImage>(entity =>
            {
                entity.HasKey(e => e.SlideId)
                    .HasName("PK__Tbl_Slid__9E7CB6509A7F186A");

                entity.ToTable("TblSlideImage");

                entity.Property(e => e.SlideImage).IsUnicode(false);

                entity.Property(e => e.SlideTitle)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUrun>(entity =>
            {
                entity.HasKey(e => e.UrunId)
                    .HasName("PK__Tbl_Urun__623D366BE4F17C68");

                entity.ToTable("TblUrun");

                entity.HasIndex(e => e.UrunAdi, "UQ__Tbl_Urun__32349D21E48641E0")
                    .IsUnique();

                entity.Property(e => e.Acıklama).IsUnicode(false);

                entity.Property(e => e.Agırlık).HasMaxLength(50);

                entity.Property(e => e.DegistirilmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Fiyat).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OlusturulmaTarihi).HasColumnType("datetime");

                entity.Property(e => e.UrunAdi)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UrunResim).IsUnicode(false);

                entity.HasOne(d => d.Kategori)
                    .WithMany(p => p.TblUruns)
                    .HasForeignKey(d => d.KategoriId)
                    .HasConstraintName("FK__Tbl_Urun__Katego__398D8EEE");
            });

            modelBuilder.Entity<TblUye>(entity =>
            {
                entity.HasKey(e => e.UyeId)
                    .HasName("PK__Tbl_Uye__76F7D98F3255BF47");

                entity.ToTable("TblUye");

                entity.HasIndex(e => e.Email, "UQ__Tbl_Uye__A9D105347B10258B")
                    .IsUnique();

                entity.Property(e => e.Adi)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Adres)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.DegistirilmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OlusturulmaTarihi).HasColumnType("datetime");

                entity.Property(e => e.Sifre)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Soyadi)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Yetkiid).HasColumnName("yetkiid");

                entity.HasOne(d => d.Yetki)
                    .WithMany(p => p.TblUyes)
                    .HasForeignKey(d => d.Yetkiid)
                    .HasConstraintName("FK_Tbl_Uye_Tbl_Roller");
            });

            modelBuilder.Entity<TblUyeRol>(entity =>
            {
                entity.HasKey(e => e.UyeRolId)
                    .HasName("PK__Tbl_UyeR__E08A6574BCDD1057");

                entity.ToTable("TblUyeRol");
            });

            modelBuilder.Entity<Ulkeler>(entity =>
            {
                entity.HasKey(e => e.UlkeId);

                entity.ToTable("Ulkeler");

                entity.Property(e => e.UlkeId).ValueGeneratedNever();

                entity.Property(e => e.IkiliKod)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.TelKodu)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.UcluKod)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.UlkeAdi)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
