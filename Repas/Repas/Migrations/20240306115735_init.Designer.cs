﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repas.Data;

#nullable disable

namespace Repas.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240306115735_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Repas.Models.DateForniture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("FornitureDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("FornitureDate")
                        .IsUnique();

                    b.ToTable("DateFornitures");
                });

            modelBuilder.Entity("Repas.Models.RepasService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DateFornitureId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int?>("TotalRepas")
                        .HasColumnType("int");

                    b.Property<int>("TypeRepasId")
                        .HasColumnType("int");

                    b.Property<int>("destination")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TypeRepasId");

                    b.ToTable("RepasServices");
                });

            modelBuilder.Entity("Repas.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Repas.Models.TypeRepas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Destination")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TypeRepas");
                });

            modelBuilder.Entity("Repas.Models.RepasService", b =>
                {
                    b.HasOne("Repas.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repas.Models.TypeRepas", "TypeRepas")
                        .WithMany()
                        .HasForeignKey("TypeRepasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("TypeRepas");
                });
#pragma warning restore 612, 618
        }
    }
}
