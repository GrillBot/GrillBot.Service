﻿// <auto-generated />
using System;
using AuditLogService.Core.Entity.Statistics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuditLogService.Core.Migrations.Statistics
{
    [DbContext(typeof(AuditLogStatisticsContext))]
    partial class AuditLogStatisticsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("statistics")
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.ApiDateCountStatistic", b =>
                {
                    b.Property<string>("ApiGroup")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.HasKey("ApiGroup", "Date");

                    b.ToTable("DateCountStatistics", "statistics");
                });

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.ApiRequestStat", b =>
                {
                    b.Property<string>("Endpoint")
                        .HasMaxLength(4096)
                        .HasColumnType("character varying(4096)");

                    b.Property<long>("FailedCount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastRequest")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("LastRunDuration")
                        .HasColumnType("bigint");

                    b.Property<long>("MaxDuration")
                        .HasColumnType("bigint");

                    b.Property<long>("MinDuration")
                        .HasColumnType("bigint");

                    b.Property<long>("SuccessCount")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalDuration")
                        .HasColumnType("bigint");

                    b.HasKey("Endpoint");

                    b.ToTable("RequestStats", "statistics");
                });

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.ApiResultCountStatistic", b =>
                {
                    b.Property<string>("ApiGroup")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("Result")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.HasKey("ApiGroup", "Result");

                    b.ToTable("ResultCountStatistic", "statistics");
                });

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.ApiUserActionStatistic", b =>
                {
                    b.Property<string>("Action")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ApiGroup")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<string>("UserId")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.HasKey("Action", "ApiGroup", "IsPublic", "UserId");

                    b.ToTable("ApiUserActionStatistics", "statistics");
                });

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.AuditLogDateStatistic", b =>
                {
                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.HasKey("Date");

                    b.ToTable("DateStatistics", "statistics");
                });

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.AuditLogTypeStatistic", b =>
                {
                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.HasKey("Type");

                    b.ToTable("TypeStatistics", "statistics");
                });

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.DailyAvgTimes", b =>
                {
                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<double>("ExternalApi")
                        .HasColumnType("double precision");

                    b.Property<double>("Interactions")
                        .HasColumnType("double precision");

                    b.Property<double>("InternalApi")
                        .HasColumnType("double precision");

                    b.Property<double>("Jobs")
                        .HasColumnType("double precision");

                    b.HasKey("Date");

                    b.ToTable("DailyAvgTimes", "statistics");
                });

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.InteractionUserActionStatistic", b =>
                {
                    b.Property<string>("Action")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("UserId")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.HasKey("Action", "UserId");

                    b.ToTable("InteractionUserActionStatistics", "statistics");
                });
#pragma warning restore 612, 618
        }
    }
}
