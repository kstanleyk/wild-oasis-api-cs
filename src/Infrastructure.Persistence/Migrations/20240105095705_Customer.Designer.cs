﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WildOasis.Infrastructure.Persistence;

#nullable disable

namespace WildOasis.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(WildOasisContext))]
    [Migration("20240105095705_Customer")]
    partial class Customer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_role");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("role", "identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_role_claim");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_role_claim_role_id");

                    b.ToTable("role_claim", "identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_claim");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_claim_user_id");

                    b.ToTable("user_claim", "identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_user_login");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_login_user_id");

                    b.ToTable("user_login", "identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("RoleId")
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_user_role");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_role_role_id");

                    b.ToTable("user_role", "identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_user_token");

                    b.ToTable("user_token", "identity");
                });

            modelBuilder.Entity("WildOasis.Domain.Entity.Cabin", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<double>("Discount")
                        .HasColumnType("double precision")
                        .HasColumnName("discount");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("image");

                    b.Property<double>("MaxCapacity")
                        .HasColumnType("double precision")
                        .HasColumnName("max_capacity");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_cabin");

                    b.ToTable("cabin", "oasis");
                });

            modelBuilder.Entity("WildOasis.Domain.Entity.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("id");

                    b.Property<string>("CountryFlag")
                        .IsRequired()
                        .HasMaxLength(75)
                        .IsUnicode(false)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("country_flag");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(75)
                        .IsUnicode(false)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("full_name");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasMaxLength(75)
                        .IsUnicode(false)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("national_id");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasMaxLength(35)
                        .IsUnicode(false)
                        .HasColumnType("character varying(35)")
                        .HasColumnName("nationality");

                    b.HasKey("Id")
                        .HasName("pk_customer");

                    b.ToTable("customer", "oasis");
                });

            modelBuilder.Entity("WildOasis.Infrastructure.Persistence.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<string>("FullName")
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("full_name");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("image_url");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_login");

                    b.Property<string>("Locale")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("locale");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<int>("LoginCount")
                        .HasColumnType("integer")
                        .HasColumnName("login_count");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("Organization")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("organization");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserCode")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("user_code");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("user", "identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_claim_role_role_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WildOasis.Infrastructure.Persistence.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_claim_user_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WildOasis.Infrastructure.Persistence.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_login_user_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_role_role_id");

                    b.HasOne("WildOasis.Infrastructure.Persistence.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_user_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WildOasis.Infrastructure.Persistence.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_token_user_user_id");
                });
#pragma warning restore 612, 618
        }
    }
}
