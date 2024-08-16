﻿// <auto-generated />
using System;
using ImageVault.ImageService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ImageVault.ImageService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ImageVault.ImageService.Data.Models.Image", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApiKeyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Collection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageCollectionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ImageCollectionId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ImageVault.ImageService.Data.Models.ImageCollection", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CollectionCoverUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CollectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ImageCollections");
                });

            modelBuilder.Entity("ImageVault.ImageService.Data.Models.Image", b =>
                {
                    b.HasOne("ImageVault.ImageService.Data.Models.ImageCollection", "ImageCollection")
                        .WithMany("ImagesCollection")
                        .HasForeignKey("ImageCollectionId");

                    b.Navigation("ImageCollection");
                });

            modelBuilder.Entity("ImageVault.ImageService.Data.Models.ImageCollection", b =>
                {
                    b.Navigation("ImagesCollection");
                });
#pragma warning restore 612, 618
        }
    }
}
