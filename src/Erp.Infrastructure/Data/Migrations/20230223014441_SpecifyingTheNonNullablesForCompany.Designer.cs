﻿// <auto-generated />
using System;
using Erp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Erp.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ErpContext))]
    [Migration("20230223014441_SpecifyingTheNonNullablesForCompany")]
    partial class SpecifyingTheNonNullablesForCompany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Erp.Core.Entities.Account.Company", b =>
                {
                    b.Property<string>("CompanyId")
                        .HasColumnType("text")
                        .HasColumnName("company_id");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_line1");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("text")
                        .HasColumnName("address_line2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Initials")
                        .HasColumnType("text")
                        .HasColumnName("initials");

                    b.Property<string>("InsertedByUserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("inserted_by_user_id");

                    b.Property<DateTime>("InsertedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("inserted_date_time");

                    b.Property<string>("Logo")
                        .HasColumnType("text")
                        .HasColumnName("logo");

                    b.Property<string>("ModifiedByUserId")
                        .HasColumnType("text")
                        .HasColumnName("modified_by_user_id");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modified_date_time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("OwnerFirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("owner_first_name");

                    b.Property<string>("OwnerLastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("owner_last_name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("Website")
                        .HasColumnType("text")
                        .HasColumnName("website");

                    b.HasKey("CompanyId")
                        .HasName("pk_company");

                    b.HasIndex("InsertedByUserId")
                        .HasDatabaseName("ix_company_inserted_by_user_id");

                    b.HasIndex("ModifiedByUserId")
                        .HasDatabaseName("ix_company_modified_by_user_id");

                    b.ToTable("company", (string)null);
                });

            modelBuilder.Entity("Erp.Core.Entities.Account.CompanyJoinRequest", b =>
                {
                    b.Property<string>("CompanyJoinRequestId")
                        .HasColumnType("text")
                        .HasColumnName("company_join_request_id");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("company_id");

                    b.Property<string>("InsertedByUserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("inserted_by_user_id");

                    b.Property<DateTime>("InsertedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("inserted_date_time");

                    b.Property<string>("ModifiedByUserId")
                        .HasColumnType("text")
                        .HasColumnName("modified_by_user_id");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modified_date_time");

                    b.Property<bool>("RequestApproved")
                        .HasColumnType("boolean")
                        .HasColumnName("request_approved");

                    b.Property<string>("RequestApprovedByUserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("request_approved_by_user_id");

                    b.Property<DateTime?>("RequestApprovedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("request_approved_date_time");

                    b.Property<bool>("RequestCancelled")
                        .HasColumnType("boolean")
                        .HasColumnName("request_cancelled");

                    b.Property<string>("RequestCancelledByUserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("request_cancelled_by_user_id");

                    b.Property<DateTime?>("RequestCancelledDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("request_cancelled_date_time");

                    b.Property<bool>("RequestInitiatedByCompany")
                        .HasColumnType("boolean")
                        .HasColumnName("request_initiated_by_company");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("CompanyJoinRequestId")
                        .HasName("pk_company_join_request");

                    b.HasIndex("InsertedByUserId")
                        .HasDatabaseName("ix_company_join_request_inserted_by_user_id");

                    b.HasIndex("ModifiedByUserId")
                        .HasDatabaseName("ix_company_join_request_modified_by_user_id");

                    b.HasIndex("RequestApprovedByUserId")
                        .HasDatabaseName("ix_company_join_request_request_approved_by_user_id");

                    b.HasIndex("RequestCancelledByUserId")
                        .HasDatabaseName("ix_company_join_request_request_cancelled_by_user_id");

                    b.ToTable("company_join_request", (string)null);
                });

            modelBuilder.Entity("Erp.Core.Entities.Account.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("role_id");

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

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("Erp.Core.Entities.Account.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

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

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<DateTime?>("InactiveDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("inactive_date_time");

                    b.Property<DateTime>("InsertedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("inserted_date_time");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modified_date_time");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

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

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Erp.Core.Entities.Account.UserCompany", b =>
                {
                    b.Property<string>("UserCompanyId")
                        .HasColumnType("text")
                        .HasColumnName("user_company_id");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("company_id");

                    b.Property<string>("InsertedByUserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("inserted_by_user_id");

                    b.Property<DateTime>("InsertedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("inserted_date_time");

                    b.Property<bool>("IsOwner")
                        .HasColumnType("boolean")
                        .HasColumnName("is_owner");

                    b.Property<string>("ModifiedByUserId")
                        .HasColumnType("text")
                        .HasColumnName("modified_by_user_id");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modified_date_time");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("UserCompanyId")
                        .HasName("pk_user_company");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_user_company_company_id");

                    b.HasIndex("InsertedByUserId")
                        .HasDatabaseName("ix_user_company_inserted_by_user_id");

                    b.HasIndex("ModifiedByUserId")
                        .HasDatabaseName("ix_user_company_modified_by_user_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_company_user_id");

                    b.ToTable("user_company", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("role_claim_id");

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

                    b.ToTable("role_claim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_claim_id");

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

                    b.ToTable("user_claim", (string)null);
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

                    b.ToTable("user_login", (string)null);
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

                    b.ToTable("user_role", (string)null);
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

                    b.ToTable("user_token", (string)null);
                });

            modelBuilder.Entity("Erp.Core.Entities.Account.Company", b =>
                {
                    b.HasOne("Erp.Core.Entities.Account.User", "InsertedByUser")
                        .WithMany()
                        .HasForeignKey("InsertedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_company_user_inserted_by_user_id");

                    b.HasOne("Erp.Core.Entities.Account.User", "ModifiedByUser")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId")
                        .HasConstraintName("fk_company_user_modified_by_user_id");

                    b.Navigation("InsertedByUser");

                    b.Navigation("ModifiedByUser");
                });

            modelBuilder.Entity("Erp.Core.Entities.Account.CompanyJoinRequest", b =>
                {
                    b.HasOne("Erp.Core.Entities.Account.User", "InsertedByUser")
                        .WithMany()
                        .HasForeignKey("InsertedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_company_join_request_user_inserted_by_user_id");

                    b.HasOne("Erp.Core.Entities.Account.User", "ModifiedByUser")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId")
                        .HasConstraintName("fk_company_join_request_user_modified_by_user_id");

                    b.HasOne("Erp.Core.Entities.Account.User", "RequestApprovedByUser")
                        .WithMany()
                        .HasForeignKey("RequestApprovedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_company_join_request_user_request_approved_by_user_id");

                    b.HasOne("Erp.Core.Entities.Account.User", "RequestCancelledByUser")
                        .WithMany()
                        .HasForeignKey("RequestCancelledByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_company_join_request_user_request_cancelled_by_user_id");

                    b.Navigation("InsertedByUser");

                    b.Navigation("ModifiedByUser");

                    b.Navigation("RequestApprovedByUser");

                    b.Navigation("RequestCancelledByUser");
                });

            modelBuilder.Entity("Erp.Core.Entities.Account.UserCompany", b =>
                {
                    b.HasOne("Erp.Core.Entities.Account.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_company_company_company_id");

                    b.HasOne("Erp.Core.Entities.Account.User", "InsertedByUser")
                        .WithMany()
                        .HasForeignKey("InsertedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_company_user_inserted_by_user_id");

                    b.HasOne("Erp.Core.Entities.Account.User", "ModifiedByUser")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId")
                        .HasConstraintName("fk_user_company_user_modified_by_user_id");

                    b.HasOne("Erp.Core.Entities.Account.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_company_user_user_id");

                    b.Navigation("Company");

                    b.Navigation("InsertedByUser");

                    b.Navigation("ModifiedByUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Erp.Core.Entities.Account.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_claim_role_role_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Erp.Core.Entities.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_claim_user_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Erp.Core.Entities.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_login_user_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Erp.Core.Entities.Account.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_role_role_id");

                    b.HasOne("Erp.Core.Entities.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_user_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Erp.Core.Entities.Account.User", null)
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
