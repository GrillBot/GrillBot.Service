﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PointsService.Core.Entity;

#nullable disable

namespace PointsService.Core.Migrations
{
    [DbContext(typeof(PointsServiceContext))]
    [Migration("20230419164553_GuildId_Index5")]
    partial class GuildId_Index5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PointsService.Core.Entity.Channel", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("GuildId")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("PointsDisabled")
                        .HasColumnType("boolean");

                    b.HasKey("Id", "GuildId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("PointsService.Core.Entity.Transaction", b =>
                {
                    b.Property<string>("GuildId")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("UserId")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("MessageId")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("ReactionId")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("MergeRangeFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("MergeRangeTo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MergedCount")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("GuildId", "UserId", "MessageId", "ReactionId");

                    b.HasIndex("GuildId", "MessageId");

                    b.HasIndex("GuildId", "UserId");

                    b.HasIndex(new[] { "CreatedAt" }, "IX_Transactions_CreatedAt");

                    b.HasIndex(new[] { "MergedCount" }, "IX_Transactions_MergedCount");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("PointsService.Core.Entity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("GuildId")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<bool>("IsUser")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastMessageIncrement")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastReactionIncrement")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("PointsDisabled")
                        .HasColumnType("boolean");

                    b.HasKey("Id", "GuildId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
