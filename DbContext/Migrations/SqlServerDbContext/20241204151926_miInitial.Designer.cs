﻿// <auto-generated />
using System;
using DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbContext.Migrations.SqlServerDbContext
{
    [DbContext(typeof(MainDbContext.SqlServerDbContext))]
    [Migration("20241204151926_miInitial")]
    partial class miInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DbModels.AddressDbM", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("AddressId");

                    b.HasIndex("StreetAddress", "ZipCode", "City", "Country")
                        .IsUnique();

                    b.ToTable("Addresses", "supusr");
                });

            modelBuilder.Entity("DbModels.FriendDbM", b =>
                {
                    b.Property<Guid>("FriendId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.HasKey("FriendId");

                    b.HasIndex("AddressId");

                    b.HasIndex("FirstName", "LastName");

                    b.HasIndex("LastName", "FirstName");

                    b.ToTable("Friends", "supusr");
                });

            modelBuilder.Entity("DbModels.PetDbM", b =>
                {
                    b.Property<Guid>("PetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FriendId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Kind")
                        .HasColumnType("int");

                    b.Property<int>("Mood")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.Property<string>("strKind")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("strMood")
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("PetId");

                    b.HasIndex("FriendId");

                    b.ToTable("Pets", "supusr");
                });

            modelBuilder.Entity("DbModels.QuoteDbM", b =>
                {
                    b.Property<Guid>("QuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("QuoteText")
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.HasKey("QuoteId");

                    b.ToTable("Quotes", "supusr");
                });

            modelBuilder.Entity("FriendDbMQuoteDbM", b =>
                {
                    b.Property<Guid>("FriendsDbMFriendId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuotesDbMQuoteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FriendsDbMFriendId", "QuotesDbMQuoteId");

                    b.HasIndex("QuotesDbMQuoteId");

                    b.ToTable("FriendDbMQuoteDbM", "supusr");
                });

            modelBuilder.Entity("Models.DTO.GstUsrInfoDbDto", b =>
                {
                    b.Property<int>("NrFriendsWithAddress")
                        .HasColumnType("int");

                    b.Property<int>("NrSeededAddresses")
                        .HasColumnType("int");

                    b.Property<int>("NrSeededFriends")
                        .HasColumnType("int");

                    b.Property<int>("NrSeededPets")
                        .HasColumnType("int");

                    b.Property<int>("NrSeededQuotes")
                        .HasColumnType("int");

                    b.Property<int>("NrUnseededAddresses")
                        .HasColumnType("int");

                    b.Property<int>("NrUnseededFriends")
                        .HasColumnType("int");

                    b.Property<int>("NrUnseededPets")
                        .HasColumnType("int");

                    b.Property<int>("NrUnseededQuotes")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("vwInfoDb", "gstusr");
                });

            modelBuilder.Entity("Models.DTO.GstUsrInfoFriendsDto", b =>
                {
                    b.Property<string>("City")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("NrFriends")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("vwInfoFriends", "gstusr");
                });

            modelBuilder.Entity("Models.DTO.GstUsrInfoPetsDto", b =>
                {
                    b.Property<string>("City")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("NrPets")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("vwInfoPets", "gstusr");
                });

            modelBuilder.Entity("Models.DTO.GstUsrInfoQuotesDto", b =>
                {
                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("NrQuotes")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("vwInfoQuotes", "gstusr");
                });

            modelBuilder.Entity("DbModels.FriendDbM", b =>
                {
                    b.HasOne("DbModels.AddressDbM", "AddressDbM")
                        .WithMany("FriendsDbM")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("AddressDbM");
                });

            modelBuilder.Entity("DbModels.PetDbM", b =>
                {
                    b.HasOne("DbModels.FriendDbM", "FriendDbM")
                        .WithMany("PetsDbM")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FriendDbM");
                });

            modelBuilder.Entity("FriendDbMQuoteDbM", b =>
                {
                    b.HasOne("DbModels.FriendDbM", null)
                        .WithMany()
                        .HasForeignKey("FriendsDbMFriendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbModels.QuoteDbM", null)
                        .WithMany()
                        .HasForeignKey("QuotesDbMQuoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DbModels.AddressDbM", b =>
                {
                    b.Navigation("FriendsDbM");
                });

            modelBuilder.Entity("DbModels.FriendDbM", b =>
                {
                    b.Navigation("PetsDbM");
                });
#pragma warning restore 612, 618
        }
    }
}
