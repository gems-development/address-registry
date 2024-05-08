﻿// <auto-generated />
using System;
using Gems.AddressRegistry.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gems.AddressRegistry.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AdministrativeAreaId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BuildingId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<Guid?>("MunicipalAreaId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PlaningStructureElementId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RoadNetworkElementId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SettlementId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TerritoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AdministrativeAreaId");

                    b.HasIndex("BuildingId");

                    b.HasIndex("CityId");

                    b.HasIndex("MunicipalAreaId");

                    b.HasIndex("PlaningStructureElementId");

                    b.HasIndex("RegionId");

                    b.HasIndex("RoadNetworkElementId");

                    b.HasIndex("SettlementId");

                    b.HasIndex("TerritoryId");

                    b.ToTable("Addresses");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Address");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.AdministrativeArea", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("AdministrativeAreas");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Building", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BuildingType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PlaningStructureElementId")
                        .HasColumnType("uuid");

                    b.Property<int>("Postcode")
                        .HasColumnType("integer");

                    b.Property<Guid?>("RoadNetworkElementId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PlaningStructureElementId");

                    b.HasIndex("RoadNetworkElementId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AdministrativeAreaId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<Guid?>("MunicipalAreaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("TerritoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AdministrativeAreaId");

                    b.HasIndex("MunicipalAreaId");

                    b.HasIndex("TerritoryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Common.DataSourceBase", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("SourceType")
                        .HasColumnType("integer");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id", "SourceType");

                    b.ToTable("DataSource", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("DataSourceBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Country", b =>
                {
                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.MunicipalArea", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("MunicipalAreas");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.PlaningStructureElement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("SettlementId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("SettlementId");

                    b.ToTable("PlaningStructureElements");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.RoadNetworkElement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PlaningStructureElementId")
                        .HasColumnType("uuid");

                    b.Property<int>("RoadNetworkElementType")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SettlementId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("PlaningStructureElementId");

                    b.HasIndex("SettlementId");

                    b.ToTable("RoadNetworkElements");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Settlement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<Guid?>("MunicipalAreaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("TerritoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("MunicipalAreaId");

                    b.HasIndex("TerritoryId");

                    b.ToTable("Settlements");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Territory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GeoJson")
                        .HasColumnType("text");

                    b.Property<Guid?>("MunicipalAreaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("MunicipalAreaId");

                    b.ToTable("Territories");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.InvalidAddress", b =>
                {
                    b.HasBaseType("Gems.AddressRegistry.Entities.Address");

                    b.HasDiscriminator().HasValue("InvalidAddress");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.AddressDataSource", b =>
                {
                    b.HasBaseType("Gems.AddressRegistry.Entities.Common.DataSourceBase");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid");

                    b.HasIndex("AddressId");

                    b.HasDiscriminator().HasValue("AddressDataSource");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.AdministrativeAreaDataSource", b =>
                {
                    b.HasBaseType("Gems.AddressRegistry.Entities.Common.DataSourceBase");

                    b.Property<Guid>("AdministrativeAreaId")
                        .HasColumnType("uuid");

                    b.HasIndex("AdministrativeAreaId");

                    b.HasDiscriminator().HasValue("AdministrativeAreaDataSource");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.BuildingDataSource", b =>
                {
                    b.HasBaseType("Gems.AddressRegistry.Entities.Common.DataSourceBase");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uuid");

                    b.HasIndex("BuildingId");

                    b.HasDiscriminator().HasValue("BuildingDataSource");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.CityDataSource", b =>
                {
                    b.HasBaseType("Gems.AddressRegistry.Entities.Common.DataSourceBase");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uuid");

                    b.HasIndex("CityId");

                    b.HasDiscriminator().HasValue("CityDataSource");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.EpsDataSource", b =>
                {
                    b.HasBaseType("Gems.AddressRegistry.Entities.Common.DataSourceBase");

                    b.Property<Guid>("EpsId")
                        .HasColumnType("uuid");

                    b.HasIndex("EpsId");

                    b.HasDiscriminator().HasValue("EpsDataSource");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.ErnDataSource", b =>
                {
                    b.HasBaseType("Gems.AddressRegistry.Entities.Common.DataSourceBase");

                    b.Property<Guid>("ErnId")
                        .HasColumnType("uuid");

                    b.HasIndex("ErnId");

                    b.HasDiscriminator().HasValue("ErnDataSource");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.MunicipalAreaDataSource", b =>
                {
                    b.HasBaseType("Gems.AddressRegistry.Entities.Common.DataSourceBase");

                    b.Property<Guid>("MunicipalAreaId")
                        .HasColumnType("uuid");

                    b.HasIndex("MunicipalAreaId");

                    b.HasDiscriminator().HasValue("MunicipalAreaDataSource");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.RegionDataSource", b =>
                {
                    b.HasBaseType("Gems.AddressRegistry.Entities.Common.DataSourceBase");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.HasIndex("RegionId");

                    b.HasDiscriminator().HasValue("RegionDataSource");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.SettlementDataSource", b =>
                {
                    b.HasBaseType("Gems.AddressRegistry.Entities.Common.DataSourceBase");

                    b.Property<Guid>("SettlementId")
                        .HasColumnType("uuid");

                    b.HasIndex("SettlementId");

                    b.HasDiscriminator().HasValue("SettlementDataSource");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.TerritoryDataSource", b =>
                {
                    b.HasBaseType("Gems.AddressRegistry.Entities.Common.DataSourceBase");

                    b.Property<Guid>("TerritoryId")
                        .HasColumnType("uuid");

                    b.HasIndex("TerritoryId");

                    b.HasDiscriminator().HasValue("TerritoryDataSource");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Address", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.AdministrativeArea", "AdministrativeArea")
                        .WithMany()
                        .HasForeignKey("AdministrativeAreaId");

                    b.HasOne("Gems.AddressRegistry.Entities.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingId");

                    b.HasOne("Gems.AddressRegistry.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("Gems.AddressRegistry.Entities.MunicipalArea", "MunicipalArea")
                        .WithMany()
                        .HasForeignKey("MunicipalAreaId");

                    b.HasOne("Gems.AddressRegistry.Entities.PlaningStructureElement", "PlaningStructureElement")
                        .WithMany()
                        .HasForeignKey("PlaningStructureElementId");

                    b.HasOne("Gems.AddressRegistry.Entities.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gems.AddressRegistry.Entities.RoadNetworkElement", "RoadNetworkElement")
                        .WithMany()
                        .HasForeignKey("RoadNetworkElementId");

                    b.HasOne("Gems.AddressRegistry.Entities.Settlement", "Settlement")
                        .WithMany()
                        .HasForeignKey("SettlementId");

                    b.HasOne("Gems.AddressRegistry.Entities.Territory", "Territory")
                        .WithMany()
                        .HasForeignKey("TerritoryId");

                    b.Navigation("AdministrativeArea");

                    b.Navigation("Building");

                    b.Navigation("City");

                    b.Navigation("MunicipalArea");

                    b.Navigation("PlaningStructureElement");

                    b.Navigation("Region");

                    b.Navigation("RoadNetworkElement");

                    b.Navigation("Settlement");

                    b.Navigation("Territory");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.AdministrativeArea", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Building", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.PlaningStructureElement", "PlaningStructureElement")
                        .WithMany()
                        .HasForeignKey("PlaningStructureElementId");

                    b.HasOne("Gems.AddressRegistry.Entities.RoadNetworkElement", "RoadNetworkElement")
                        .WithMany()
                        .HasForeignKey("RoadNetworkElementId");

                    b.Navigation("PlaningStructureElement");

                    b.Navigation("RoadNetworkElement");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.City", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.AdministrativeArea", "AdministrativeArea")
                        .WithMany()
                        .HasForeignKey("AdministrativeAreaId");

                    b.HasOne("Gems.AddressRegistry.Entities.MunicipalArea", "MunicipalArea")
                        .WithMany()
                        .HasForeignKey("MunicipalAreaId");

                    b.HasOne("Gems.AddressRegistry.Entities.Territory", "Territory")
                        .WithMany()
                        .HasForeignKey("TerritoryId");

                    b.Navigation("AdministrativeArea");

                    b.Navigation("MunicipalArea");

                    b.Navigation("Territory");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.MunicipalArea", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.PlaningStructureElement", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("Gems.AddressRegistry.Entities.Settlement", "Settlement")
                        .WithMany()
                        .HasForeignKey("SettlementId");

                    b.Navigation("City");

                    b.Navigation("Settlement");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.RoadNetworkElement", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("Gems.AddressRegistry.Entities.PlaningStructureElement", "PlaningStructureElement")
                        .WithMany()
                        .HasForeignKey("PlaningStructureElementId");

                    b.HasOne("Gems.AddressRegistry.Entities.Settlement", "Settlement")
                        .WithMany()
                        .HasForeignKey("SettlementId");

                    b.Navigation("City");

                    b.Navigation("PlaningStructureElement");

                    b.Navigation("Settlement");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Settlement", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("Gems.AddressRegistry.Entities.MunicipalArea", "MunicipalArea")
                        .WithMany()
                        .HasForeignKey("MunicipalAreaId");

                    b.HasOne("Gems.AddressRegistry.Entities.Territory", "Territory")
                        .WithMany()
                        .HasForeignKey("TerritoryId");

                    b.Navigation("City");

                    b.Navigation("MunicipalArea");

                    b.Navigation("Territory");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Territory", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.MunicipalArea", "MunicipalArea")
                        .WithMany()
                        .HasForeignKey("MunicipalAreaId");

                    b.Navigation("MunicipalArea");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.AddressDataSource", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.Address", "Address")
                        .WithMany("DataSources")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.AdministrativeAreaDataSource", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.AdministrativeArea", "AdministrativeArea")
                        .WithMany("DataSources")
                        .HasForeignKey("AdministrativeAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdministrativeArea");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.BuildingDataSource", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.Building", "Building")
                        .WithMany("DataSources")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.CityDataSource", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.City", "City")
                        .WithMany("DataSources")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.EpsDataSource", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.PlaningStructureElement", "Eps")
                        .WithMany("DataSources")
                        .HasForeignKey("EpsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Eps");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.ErnDataSource", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.RoadNetworkElement", "Ern")
                        .WithMany("DataSources")
                        .HasForeignKey("ErnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ern");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.MunicipalAreaDataSource", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.MunicipalArea", "MunicipalArea")
                        .WithMany("DataSources")
                        .HasForeignKey("MunicipalAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MunicipalArea");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.RegionDataSource", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.Region", "Region")
                        .WithMany("DataSources")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.SettlementDataSource", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.Settlement", "Settlement")
                        .WithMany("DataSources")
                        .HasForeignKey("SettlementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Settlement");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.DataSources.TerritoryDataSource", b =>
                {
                    b.HasOne("Gems.AddressRegistry.Entities.Territory", "Territory")
                        .WithMany("DataSources")
                        .HasForeignKey("TerritoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Territory");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Address", b =>
                {
                    b.Navigation("DataSources");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.AdministrativeArea", b =>
                {
                    b.Navigation("DataSources");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Building", b =>
                {
                    b.Navigation("DataSources");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.City", b =>
                {
                    b.Navigation("DataSources");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.MunicipalArea", b =>
                {
                    b.Navigation("DataSources");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.PlaningStructureElement", b =>
                {
                    b.Navigation("DataSources");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Region", b =>
                {
                    b.Navigation("DataSources");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.RoadNetworkElement", b =>
                {
                    b.Navigation("DataSources");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Settlement", b =>
                {
                    b.Navigation("DataSources");
                });

            modelBuilder.Entity("Gems.AddressRegistry.Entities.Territory", b =>
                {
                    b.Navigation("DataSources");
                });
#pragma warning restore 612, 618
        }
    }
}
