﻿// <auto-generated />
using System;
using System.Collections.Generic;
using HRM_Management.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRM_Management.Dal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241015110134_FullNameOfUser")]
    partial class FullNameOfUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HRM_Management.Dal.Entities.ChildEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ParentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfComing")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("Documents")
                        .HasColumnType("text[]");

                    b.Property<string>(" Mail")
                        .HasColumnType("text");

                    b.Property<int?>("HirerId")
                        .HasColumnType("integer");

                    b.Property<int?>("HubId")
                        .HasColumnType("integer");

                    b.Property<string>("MentorName")
                        .HasColumnType("text");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<int>("TechLevel")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HirerId");

                    b.HasIndex("HubId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.ExEmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LeavingDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("ExEmployees");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.HubEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DeputyLeaderName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DirectorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LeaderName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Hubs");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.PartnerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.PersonEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CV")
                        .HasColumnType("text");

                    b.Property<int>("EnglishLevel")
                        .HasColumnType("integer");

                    b.Property<string>("FNameEn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Hobbies")
                        .HasColumnType("text");

                    b.Property<string>("LNameEn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MNameEn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PartnerId")
                        .HasColumnType("integer");

                    b.Property<string>("PassportScan")
                        .HasColumnType("text");

                    b.Property<string>("PersonalEmail")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .HasColumnType("text");

                    b.Property<string>("PrevWorkPlace")
                        .HasColumnType("text");

                    b.Property<int>("TShirtSize")
                        .HasColumnType("integer");

                    b.Property<string>("TechStack")
                        .HasColumnType("text");

                    b.Property<int>("TelegramId")
                        .HasColumnType("integer");

                    b.Property<string>("TelegramUserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.PersonTranslateEntity", b =>
                {
                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<string>("FNameUk")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LNameUk")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MNameUk")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PersonId");

                    b.ToTable("Translates");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.ChildEntity", b =>
                {
                    b.HasOne("HRM_Management.Dal.Entities.PersonEntity", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.EmployeeEntity", b =>
                {
                    b.HasOne("HRM_Management.Dal.Entities.UserEntity", "Hirer")
                        .WithMany("HiredEmployees")
                        .HasForeignKey("HirerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HRM_Management.Dal.Entities.HubEntity", "Hub")
                        .WithMany("Employees")
                        .HasForeignKey("HubId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("HRM_Management.Dal.Entities.PersonEntity", "Person")
                        .WithOne("Employee")
                        .HasForeignKey("HRM_Management.Dal.Entities.EmployeeEntity", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hirer");

                    b.Navigation("Hub");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.ExEmployeeEntity", b =>
                {
                    b.HasOne("HRM_Management.Dal.Entities.EmployeeEntity", "Employee")
                        .WithOne("ExEmployee")
                        .HasForeignKey("HRM_Management.Dal.Entities.ExEmployeeEntity", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.PersonEntity", b =>
                {
                    b.HasOne("HRM_Management.Dal.Entities.PartnerEntity", "Partner")
                        .WithOne("MainPartner")
                        .HasForeignKey("HRM_Management.Dal.Entities.PersonEntity", "PartnerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.PersonTranslateEntity", b =>
                {
                    b.HasOne("HRM_Management.Dal.Entities.PersonEntity", "Person")
                        .WithOne("Translate")
                        .HasForeignKey("HRM_Management.Dal.Entities.PersonTranslateEntity", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("HRM_Management.Dal.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("HRM_Management.Dal.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRM_Management.Dal.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("HRM_Management.Dal.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.EmployeeEntity", b =>
                {
                    b.Navigation("ExEmployee");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.HubEntity", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.PartnerEntity", b =>
                {
                    b.Navigation("MainPartner");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.PersonEntity", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Employee");

                    b.Navigation("Translate");
                });

            modelBuilder.Entity("HRM_Management.Dal.Entities.UserEntity", b =>
                {
                    b.Navigation("HiredEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}
