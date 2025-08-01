﻿// <auto-generated />
using System;
using LogisticsSolution.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LogisticsSolution.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250710143707_AddRemarkColumn")]
    partial class AddRemarkColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.AgentProvince", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AgentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("ZN_AgentsProvince", (string)null);
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.Mailing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.ToTable("ZN_MailingList", (string)null);
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.MoveHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("CompletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MoveAgentId")
                        .HasColumnType("integer");

                    b.Property<long>("MoveRequestId")
                        .HasColumnType("bigint");

                    b.Property<int>("MoveStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ScheduledTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("MoveAgentId");

                    b.HasIndex("MoveRequestId");

                    b.ToTable("ZN_MoveHostories", (string)null);
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.MoveItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemCount")
                        .HasColumnType("integer");

                    b.Property<string>("ItemName")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<long>("MoveRequestId")
                        .HasColumnType("bigint");

                    b.Property<string>("RoomName")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("MoveRequestId");

                    b.ToTable("ZN_Move_Items", (string)null);
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.MoveRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DropOffAddress")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("DropOffLatitude")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("DropOffLongitude")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("HasBuildingInsurance")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasElevator")
                        .HasColumnType("boolean");

                    b.Property<string>("LongCarry")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("MoveCode")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)");

                    b.Property<int>("MoveStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("MoveTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("NeedHelpPacking")
                        .HasColumnType("boolean");

                    b.Property<bool>("NeedShuttle")
                        .HasColumnType("boolean");

                    b.Property<int>("NumberOfFloors")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("PickUpAddress")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("PickUpLatitude")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PickUpLongitude")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("PickUpTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("integer");

                    b.Property<string>("Remark")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MoveCode")
                        .IsUnique();

                    b.HasIndex("ProvinceId");

                    b.ToTable("ZN_Move_Requests", (string)null);
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.MovingAgent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("CompanyOverView")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("KvkNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("KvkNumber")
                        .IsUnique();

                    b.ToTable("ZN_Moving_Agents", (string)null);
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("ZN_Province", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Drenthe"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Flevoland"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Friesland (Fryslân)"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Gelderland"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Groningen"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Limburg"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Noord-Brabant"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Noord-Holland"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Overijssel"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Utrecht"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Zeeland"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Zuid-Holland"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Assen"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Lelystad"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Arnhem"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Groningen"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Maastricht"
                        },
                        new
                        {
                            Id = 18,
                            Name = "'s-Hertogenbosch (Den Bosch)"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Haarlem"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Zwolle"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Middelburg"
                        },
                        new
                        {
                            Id = 22,
                            Name = "The Hague (Den Haag)"
                        },
                        new
                        {
                            Id = 23,
                            Name = "Leeuwarden"
                        });
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.Quote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AdditonalInformation")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("MoveRequestId")
                        .HasColumnType("bigint");

                    b.Property<int>("MovingAgentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ProposedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("MoveRequestId");

                    b.HasIndex("MovingAgentId");

                    b.ToTable("ZN_Quotes", (string)null);
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.AgentProvince", b =>
                {
                    b.HasOne("LogisticsSolution.Domain.Entities.MovingAgent", "Agent")
                        .WithMany("ProvincesCovered")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogisticsSolution.Domain.Entities.Province", "Province")
                        .WithMany("MovingAgents")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Agent");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.MoveHistory", b =>
                {
                    b.HasOne("LogisticsSolution.Domain.Entities.MovingAgent", "MovingAgent")
                        .WithMany("MoveHistories")
                        .HasForeignKey("MoveAgentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogisticsSolution.Domain.Entities.MoveRequest", "MoveRequest")
                        .WithMany("MoveHistories")
                        .HasForeignKey("MoveRequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MoveRequest");

                    b.Navigation("MovingAgent");
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.MoveItem", b =>
                {
                    b.HasOne("LogisticsSolution.Domain.Entities.MoveRequest", "MoveRequest")
                        .WithMany("MoveItems")
                        .HasForeignKey("MoveRequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MoveRequest");
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.MoveRequest", b =>
                {
                    b.HasOne("LogisticsSolution.Domain.Entities.Province", "Province")
                        .WithMany("MoveRequests")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.Quote", b =>
                {
                    b.HasOne("LogisticsSolution.Domain.Entities.MoveRequest", "MoveRequest")
                        .WithMany("Quotes")
                        .HasForeignKey("MoveRequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogisticsSolution.Domain.Entities.MovingAgent", "MovingAgent")
                        .WithMany("Quotes")
                        .HasForeignKey("MovingAgentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MoveRequest");

                    b.Navigation("MovingAgent");
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.MoveRequest", b =>
                {
                    b.Navigation("MoveHistories");

                    b.Navigation("MoveItems");

                    b.Navigation("Quotes");
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.MovingAgent", b =>
                {
                    b.Navigation("MoveHistories");

                    b.Navigation("ProvincesCovered");

                    b.Navigation("Quotes");
                });

            modelBuilder.Entity("LogisticsSolution.Domain.Entities.Province", b =>
                {
                    b.Navigation("MoveRequests");

                    b.Navigation("MovingAgents");
                });
#pragma warning restore 612, 618
        }
    }
}
