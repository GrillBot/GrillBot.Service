﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RubbergodService.Core.Entity;

#nullable disable

namespace RubbergodService.Core.Migrations
{
    [DbContext(typeof(RubbergodServiceContext))]
    [Migration("20230219165541_MemberCacheValidation")]
    partial class MemberCacheValidation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RubbergodService.Core.Entity.Karma", b =>
                {
                    b.Property<string>("MemberId")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasAnnotation("Relational:JsonPropertyName", "member_ID");

                    b.Property<int>("KarmaValue")
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "karma");

                    b.Property<int>("Negative")
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "negative");

                    b.Property<int>("Positive")
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "positive");

                    b.HasKey("MemberId");

                    b.ToTable("Karma");
                });

            modelBuilder.Entity("RubbergodService.Core.Entity.MemberCacheItem", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.HasKey("UserId");

                    b.ToTable("MemberCache");
                });
#pragma warning restore 612, 618
        }
    }
}
