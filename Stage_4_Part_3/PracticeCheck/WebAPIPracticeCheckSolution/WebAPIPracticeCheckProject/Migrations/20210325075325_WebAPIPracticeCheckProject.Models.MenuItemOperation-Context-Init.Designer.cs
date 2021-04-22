﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPIPracticeCheckProject.Models;

namespace WebAPIPracticeCheckProject.Migrations
{
    [DbContext(typeof(MenuItemOperation))]
    [Migration("20210325075325_WebAPIPracticeCheckProject.Models.MenuItemOperation-Context-Init")]
    partial class WebAPIPracticeCheckProjectModelsMenuItemOperationContextInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPIPracticeCheckProject.Models.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfLaunch")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<bool>("isFreeDelivery")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("MenuItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = 1,
                            DateOfLaunch = new DateTime(2021, 1, 20, 0, 3, 0, 0, DateTimeKind.Unspecified),
                            Name = "Poori",
                            Price = 20.0,
                            isActive = true,
                            isFreeDelivery = true
                        });
                });
#pragma warning restore 612, 618
        }
    }
}