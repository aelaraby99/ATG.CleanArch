﻿// <auto-generated />
using System;
using CleanArch.ATG.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace CleanArch.ATG.Infrastructure.Migrations
{
    [DbContext(typeof(ATGDbContext))]
    [Migration("20240624095830_AddingPriceColumnStr")]
    partial class AddingPriceColumnStr
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CleanArch.ATG.Domain.Entities.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("CleanArch.ATG.Domain.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("CleanArch.ATG.Domain.Entities.BrandCategory", b =>
                {
                    b.Property<int>("BrandId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<byte>("IsDeleted")
                        .HasColumnType("NUMBER(3)");

                    b.HasKey("BrandId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BrandCategories");
                });

            modelBuilder.Entity("CleanArch.ATG.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<byte>("IsDeleted")
                        .HasColumnType("NUMBER(3)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CleanArch.ATG.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<byte>("IsDeleted")
                        .HasColumnType("NUMBER(3)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TIMESTAMP(7)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CleanArch.ATG.Domain.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("ProductId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<byte>("IsDeleted")
                        .HasColumnType("NUMBER(3)");

                    b.Property<int>("Quantity")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("CleanArch.ATG.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<byte>("IsDeleted")
                        .HasColumnType("NUMBER(3)");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<double>("Price")
                        .HasColumnType("BINARY_DOUBLE");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CleanArch.ATG.Domain.Entities.BrandCategory", b =>
                {
                    b.HasOne("CleanArch.ATG.Domain.Entities.Brand", "Brand")
                        .WithMany("BrandCategories")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanArch.ATG.Domain.Entities.Category", "Category")
                        .WithMany("BrandCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CleanArch.ATG.Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("CleanArch.ATG.Domain.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanArch.ATG.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CleanArch.ATG.Domain.Entities.Brand", b =>
                {
                    b.Navigation("BrandCategories");
                });

            modelBuilder.Entity("CleanArch.ATG.Domain.Entities.Category", b =>
                {
                    b.Navigation("BrandCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
