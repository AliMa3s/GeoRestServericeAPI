﻿// <auto-generated />
using GeoService.DataEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GeoService.DataEF.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GeoService.Core.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryId = 1,
                            Name = "Brussel",
                            Population = 8000
                        },
                        new
                        {
                            Id = 2,
                            CountryId = 2,
                            Name = "Berlin",
                            Population = 8000
                        },
                        new
                        {
                            Id = 3,
                            CountryId = 3,
                            Name = "Peking",
                            Population = 12000
                        },
                        new
                        {
                            Id = 4,
                            CountryId = 4,
                            Name = "Tokyo",
                            Population = 12000
                        },
                        new
                        {
                            Id = 5,
                            CountryId = 5,
                            Name = "Istanbul",
                            Population = 15000
                        },
                        new
                        {
                            Id = 6,
                            CountryId = 6,
                            Name = "Tehran",
                            Population = 15000
                        },
                        new
                        {
                            Id = 7,
                            CountryId = 7,
                            Name = "Nairobi",
                            Population = 15000
                        },
                        new
                        {
                            Id = 8,
                            CountryId = 8,
                            Name = "Newyork",
                            Population = 15000
                        },
                        new
                        {
                            Id = 9,
                            CountryId = 9,
                            Name = "Moscow",
                            Population = 20000
                        },
                        new
                        {
                            Id = 10,
                            CountryId = 10,
                            Name = "London",
                            Population = 20000
                        });
                });

            modelBuilder.Entity("GeoService.Core.Model.Continent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Continent");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Europe",
                            Population = 8000
                        },
                        new
                        {
                            Id = 2,
                            Name = "Asia",
                            Population = 8000
                        },
                        new
                        {
                            Id = 3,
                            Name = "Afrika",
                            Population = 12000
                        },
                        new
                        {
                            Id = 4,
                            Name = "North-America",
                            Population = 12000
                        },
                        new
                        {
                            Id = 5,
                            Name = "South-America",
                            Population = 15000
                        });
                });

            modelBuilder.Entity("GeoService.Core.Model.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContinentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.Property<double>("Surface")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ContinentId");

                    b.ToTable("Country");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContinentId = 1,
                            Name = "Belgium",
                            Population = 8000,
                            Surface = 0.0
                        },
                        new
                        {
                            Id = 2,
                            ContinentId = 1,
                            Name = "Germany",
                            Population = 8000,
                            Surface = 0.0
                        },
                        new
                        {
                            Id = 3,
                            ContinentId = 2,
                            Name = "China",
                            Population = 12000,
                            Surface = 0.0
                        },
                        new
                        {
                            Id = 4,
                            ContinentId = 2,
                            Name = "Japan",
                            Population = 12000,
                            Surface = 0.0
                        },
                        new
                        {
                            Id = 5,
                            ContinentId = 2,
                            Name = "Turkey",
                            Population = 15000,
                            Surface = 0.0
                        },
                        new
                        {
                            Id = 6,
                            ContinentId = 2,
                            Name = "Iran",
                            Population = 15000,
                            Surface = 0.0
                        },
                        new
                        {
                            Id = 7,
                            ContinentId = 3,
                            Name = "Nigeria",
                            Population = 15000,
                            Surface = 0.0
                        },
                        new
                        {
                            Id = 8,
                            ContinentId = 4,
                            Name = "Usa",
                            Population = 15000,
                            Surface = 0.0
                        },
                        new
                        {
                            Id = 9,
                            ContinentId = 2,
                            Name = "Russia",
                            Population = 20000,
                            Surface = 0.0
                        },
                        new
                        {
                            Id = 10,
                            ContinentId = 1,
                            Name = "England",
                            Population = 20000,
                            Surface = 0.0
                        });
                });

            modelBuilder.Entity("GeoService.Core.Model.CountryRiver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int")
                        .HasColumnName("Country_Id");

                    b.Property<int>("RiverId")
                        .HasColumnType("int")
                        .HasColumnName("River_Id");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("RiverId");

                    b.ToTable("Country_River");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryId = 2,
                            RiverId = 1
                        },
                        new
                        {
                            Id = 2,
                            CountryId = 3,
                            RiverId = 2
                        },
                        new
                        {
                            Id = 3,
                            CountryId = 4,
                            RiverId = 4
                        },
                        new
                        {
                            Id = 4,
                            CountryId = 1,
                            RiverId = 3
                        },
                        new
                        {
                            Id = 5,
                            CountryId = 5,
                            RiverId = 5
                        },
                        new
                        {
                            Id = 6,
                            CountryId = 10,
                            RiverId = 6
                        },
                        new
                        {
                            Id = 7,
                            CountryId = 9,
                            RiverId = 7
                        },
                        new
                        {
                            Id = 8,
                            CountryId = 7,
                            RiverId = 10
                        },
                        new
                        {
                            Id = 9,
                            CountryId = 6,
                            RiverId = 9
                        },
                        new
                        {
                            Id = 10,
                            CountryId = 8,
                            RiverId = 8
                        });
                });

            modelBuilder.Entity("GeoService.Core.Model.River", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("River");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Length = 5464.0,
                            Name = "Berlins Wall River"
                        },
                        new
                        {
                            Id = 2,
                            Length = 2348.0,
                            Name = "Salween River"
                        },
                        new
                        {
                            Id = 3,
                            Length = 6300.0,
                            Name = "Gent Kanal"
                        },
                        new
                        {
                            Id = 4,
                            Length = 1012.0,
                            Name = "japanees blosom river"
                        },
                        new
                        {
                            Id = 5,
                            Length = 4200.0,
                            Name = "Istanbul River"
                        },
                        new
                        {
                            Id = 6,
                            Length = 3625.0,
                            Name = "London bridge river"
                        },
                        new
                        {
                            Id = 7,
                            Length = 3142.0,
                            Name = "Moscow kanal"
                        },
                        new
                        {
                            Id = 8,
                            Length = 890.0,
                            Name = "New York River"
                        },
                        new
                        {
                            Id = 9,
                            Length = 4200.0,
                            Name = "Tehran River"
                        },
                        new
                        {
                            Id = 10,
                            Length = 3000.0,
                            Name = "Abotalal River"
                        });
                });

            modelBuilder.Entity("GeoService.Core.Model.City", b =>
                {
                    b.HasOne("GeoService.Core.Model.Country", "Country")
                        .WithMany("cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("GeoService.Core.Model.Country", b =>
                {
                    b.HasOne("GeoService.Core.Model.Continent", "Continent")
                        .WithMany("countries")
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Continent");
                });

            modelBuilder.Entity("GeoService.Core.Model.CountryRiver", b =>
                {
                    b.HasOne("GeoService.Core.Model.Country", "Country")
                        .WithMany("countryRiver")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK_Country_River_Country")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeoService.Core.Model.River", "River")
                        .WithMany("countryRiver")
                        .HasForeignKey("RiverId")
                        .HasConstraintName("FK_Country_River_River")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("River");
                });

            modelBuilder.Entity("GeoService.Core.Model.Continent", b =>
                {
                    b.Navigation("countries");
                });

            modelBuilder.Entity("GeoService.Core.Model.Country", b =>
                {
                    b.Navigation("cities");

                    b.Navigation("countryRiver");
                });

            modelBuilder.Entity("GeoService.Core.Model.River", b =>
                {
                    b.Navigation("countryRiver");
                });
#pragma warning restore 612, 618
        }
    }
}
