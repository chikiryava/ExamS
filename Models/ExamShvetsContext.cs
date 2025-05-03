using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExamShvets.Models;

public partial class ExamShvetsContext : DbContext
{
    public ExamShvetsContext()
    {
    }

    public ExamShvetsContext(DbContextOptions<ExamShvetsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaterialTypeImport> MaterialTypeImports { get; set; }

    public virtual DbSet<PartnerProductsImport> PartnerProductsImports { get; set; }

    public virtual DbSet<PartnerTypeImport> PartnerTypeImports { get; set; }

    public virtual DbSet<PartnersImport> PartnersImports { get; set; }

    public virtual DbSet<ProductTypeImport> ProductTypeImports { get; set; }

    public virtual DbSet<ProductsImport> ProductsImports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PC310-08;Database=ExamenShvets;Trusted_Connection=True; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<MaterialTypeImport>(entity =>
        {
            entity.HasKey(e => e.MaterialTypeId);

            entity.ToTable("Material_type_import");

            entity.Property(e => e.MaterialTypeId)
                .ValueGeneratedNever()
                .HasColumnName("material_type_id");
            entity.Property(e => e.MaterialType)
                .HasMaxLength(100)
                .HasColumnName("material_type");
            entity.Property(e => e.PercentageOfDefects)
                .HasMaxLength(50)
                .HasColumnName("percentage_of_defects");
        });

        modelBuilder.Entity<PartnerProductsImport>(entity =>
        {
            entity.HasKey(e => e.SalesId);

            entity.ToTable("Partner_products_import");

            entity.Property(e => e.SalesId)
                .ValueGeneratedNever()
                .HasColumnName("sales_id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.DataSales).HasColumnName("data_sales");
            entity.Property(e => e.PartnerId).HasColumnName("partner_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Partner).WithMany(p => p.PartnerProductsImports)
                .HasForeignKey(d => d.PartnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_products_import_Partners_import");

            entity.HasOne(d => d.Product).WithMany(p => p.PartnerProductsImports)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_products_import_Products_import");
        });

        modelBuilder.Entity<PartnerTypeImport>(entity =>
        {
            entity.HasKey(e => e.PartnerTypeId);

            entity.ToTable("Partner_type_import");

            entity.Property(e => e.PartnerTypeId)
                .ValueGeneratedNever()
                .HasColumnName("partner_type_id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<PartnersImport>(entity =>
        {
            entity.HasKey(e => e.PartnerId);

            entity.ToTable("Partners_import");

            entity.Property(e => e.PartnerId)
                .ValueGeneratedNever()
                .HasColumnName("partner_id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.AddressPartner)
                .HasMaxLength(100)
                .HasColumnName("address_partner");
            entity.Property(e => e.Director)
                .HasMaxLength(100)
                .HasColumnName("director");
            entity.Property(e => e.EmailPartner)
                .HasMaxLength(100)
                .HasColumnName("email_partner");
            entity.Property(e => e.Inn).HasColumnName("inn");
            entity.Property(e => e.PartnerTypeId).HasColumnName("partner_type_id");
            entity.Property(e => e.PhonePartner)
                .HasMaxLength(50)
                .HasColumnName("phone_partner");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.PartnerType).WithMany(p => p.PartnersImports)
                .HasForeignKey(d => d.PartnerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partners_import_Partner_type_import");
        });

        modelBuilder.Entity<ProductTypeImport>(entity =>
        {
            entity.HasKey(e => e.TypeProductId);

            entity.ToTable("Product_type_import");

            entity.Property(e => e.TypeProductId)
                .ValueGeneratedNever()
                .HasColumnName("type_product_id");
            entity.Property(e => e.CoefTypeProduct)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("coef_type_product");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<ProductsImport>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("Products_import");

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("product_id");
            entity.Property(e => e.Article).HasColumnName("article");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.MinCostForPartner)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("min_cost_for_partner");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Material).WithMany(p => p.ProductsImports)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_import_Material_type_import");

            entity.HasOne(d => d.Type).WithMany(p => p.ProductsImports)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_import_Product_type_import");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
