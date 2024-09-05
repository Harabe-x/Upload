﻿// <auto-generated />
using System;
using ImageVault.RequestMetricsService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ImageVault.RequestMetricsService.Migrations
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

            modelBuilder.Entity("ImageVault.RequestMetricsService.Data.Models.AnonymousRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Endpoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AnonymousRequests");
                });

            modelBuilder.Entity("ImageVault.RequestMetricsService.Data.Models.DailyUsageMetrics", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long>("TotalImageUploaded")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalRequests")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalStorageUsed")
                        .HasColumnType("bigint");

                    b.Property<string>("UsageMetricsId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UsageMetricsId");

                    b.ToTable("UsersDailyUsageMetrics");
                });

            modelBuilder.Entity("ImageVault.RequestMetricsService.Data.Models.Request", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DailyUsageMetricsId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Endpoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DailyUsageMetricsId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("ImageVault.RequestMetricsService.Data.Models.UsageMetrics", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("TotalImageUploaded")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalRequests")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalStorageUsed")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserUsageMetrics");
                });

            modelBuilder.Entity("ImageVault.RequestMetricsService.Data.Models.DailyUsageMetrics", b =>
                {
                    b.HasOne("ImageVault.RequestMetricsService.Data.Models.UsageMetrics", "UsageMetrics")
                        .WithMany("DailyUsageMetrics")
                        .HasForeignKey("UsageMetricsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsageMetrics");
                });

            modelBuilder.Entity("ImageVault.RequestMetricsService.Data.Models.Request", b =>
                {
                    b.HasOne("ImageVault.RequestMetricsService.Data.Models.DailyUsageMetrics", "DailyUsageMetrics")
                        .WithMany("Requests")
                        .HasForeignKey("DailyUsageMetricsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DailyUsageMetrics");
                });

            modelBuilder.Entity("ImageVault.RequestMetricsService.Data.Models.DailyUsageMetrics", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("ImageVault.RequestMetricsService.Data.Models.UsageMetrics", b =>
                {
                    b.Navigation("DailyUsageMetrics");
                });
#pragma warning restore 612, 618
        }
    }
}
