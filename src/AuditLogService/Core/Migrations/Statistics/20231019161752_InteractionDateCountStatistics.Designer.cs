﻿// <auto-generated />
using System;
using AuditLogService.Core.Entity.Statistics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuditLogService.Core.Migrations.Statistics
{
    [DbContext(typeof(AuditLogStatisticsContext))]
    [Migration("20231019161752_InteractionDateCountStatistics")]
    partial class InteractionDateCountStatistics
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.DatabaseStatistic", b =>
                {
                    b.Property<string>("TableName")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<long>("RecordsCount")
                        .HasColumnType("bigint");

                    b.HasKey("TableName");

                    b.ToTable("DatabaseStatistics", "statistics");
                });

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.FileExtensionStatistic", b =>
                {
                    b.Property<string>("Extension")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("Extension");

                    b.ToTable("FileExtensionStatistics", "statistics");
                });

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.InteractionDateCountStatistic", b =>
                {
                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.HasKey("Date");

                    b.ToTable("InteractionDateCountStatistics", "statistics");
                });

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.InteractionStatistic", b =>
                {
                    b.Property<string>("Action")
                        .HasMaxLength(4096)
                        .HasColumnType("character varying(4096)");

                    b.Property<long>("FailedCount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastRun")
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

                    b.HasKey("Action");

                    b.ToTable("InteractionStatistics", "statistics");
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

            modelBuilder.Entity("AuditLogService.Core.Entity.Statistics.JobInfo", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("AvgTime")
                        .HasColumnType("integer");

                    b.Property<int>("FailedCount")
                        .HasColumnType("integer");

                    b.Property<int>("LastRunDuration")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastStartAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MaxTime")
                        .HasColumnType("integer");

                    b.Property<int>("MinTime")
                        .HasColumnType("integer");

                    b.Property<int>("StartCount")
                        .HasColumnType("integer");

                    b.Property<int>("TotalDuration")
                        .HasColumnType("integer");

                    b.HasKey("Name");

                    b.ToTable("JobInfos", "statistics");
                });
#pragma warning restore 612, 618
        }
    }
}
