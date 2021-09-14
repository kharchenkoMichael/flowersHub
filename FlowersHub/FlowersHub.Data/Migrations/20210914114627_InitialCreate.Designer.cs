﻿// <auto-generated />
using FlowersHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlowersHub.Data.Migrations
{
    [DbContext(typeof(FlowersHubContext))]
    [Migration("20210914114627_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlowersHub.Model.ColorType", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FlowerUrl")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Variations")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.HasIndex("FlowerUrl");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("FlowersHub.Model.Flower", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Url");

                    b.ToTable("Flowers");
                });

            modelBuilder.Entity("FlowersHub.Model.FlowerType", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FlowerUrl")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Variations")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.HasIndex("FlowerUrl");

                    b.ToTable("FlowerTypes");
                });

            modelBuilder.Entity("FlowersHub.Model.ColorType", b =>
                {
                    b.HasOne("FlowersHub.Model.Flower", null)
                        .WithMany("Colors")
                        .HasForeignKey("FlowerUrl");
                });

            modelBuilder.Entity("FlowersHub.Model.FlowerType", b =>
                {
                    b.HasOne("FlowersHub.Model.Flower", null)
                        .WithMany("FlowerTypes")
                        .HasForeignKey("FlowerUrl");
                });

            modelBuilder.Entity("FlowersHub.Model.Flower", b =>
                {
                    b.Navigation("Colors");

                    b.Navigation("FlowerTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
