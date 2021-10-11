﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eshop.Models.Database;

namespace eshop.Migrations
{
    [DbContext(typeof(EshopDBContext))]
    [Migration("20201211154825_MSSQL_1_0_5")]
    partial class MSSQL_1_0_5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eshop.Models.Carousel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarouselContent")
                        .IsRequired();

                    b.Property<string>("DataTarget")
                        .IsRequired();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ImageAlt")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ImageSrc")
                        .IsRequired()
                        .HasMaxLength(225);

                    b.HasKey("ID");

                    b.ToTable("Carousel");
                });

            modelBuilder.Entity("eshop.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("ID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("eshop.Models.OrderItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("OrderID");

                    b.Property<decimal>("Price");

                    b.Property<int>("ProductID");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("eshop.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ImageAlt")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ImageSrc")
                        .IsRequired()
                        .HasMaxLength(225);

                    b.Property<double>("Price");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("eshop.Models.OrderItem", b =>
                {
                    b.HasOne("eshop.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eshop.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
