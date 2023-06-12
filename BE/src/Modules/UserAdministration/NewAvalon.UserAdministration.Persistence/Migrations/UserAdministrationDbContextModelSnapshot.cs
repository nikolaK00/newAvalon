﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewAvalon.UserAdministration.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NewAvalon.UserAdministration.Persistence.Migrations
{
    [DbContext(typeof(UserAdministrationDbContext))]
    partial class UserAdministrationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("NewAvalon.Persistence.Relational.Outbox.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<bool>("Failed")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Processed")
                        .HasColumnType("boolean");

                    b.Property<int>("Retries")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("NewAvalon.UserAdministration.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("NewAvalon.UserAdministration.Domain.Entities.Dealer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("Dealers");
                });

            modelBuilder.Entity("NewAvalon.UserAdministration.Domain.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Can read users",
                            Name = "UserRead"
                        },
                        new
                        {
                            Id = 100,
                            Description = "Can read orders",
                            Name = "OrderRead"
                        },
                        new
                        {
                            Id = 101,
                            Description = "Can create orders",
                            Name = "OrderCreate"
                        },
                        new
                        {
                            Id = 102,
                            Description = "Can delete orders",
                            Name = "OrderDelete"
                        },
                        new
                        {
                            Id = 200,
                            Description = "Can create products",
                            Name = "ProductCreate"
                        },
                        new
                        {
                            Id = 201,
                            Description = "Can update products",
                            Name = "ProductUpdate"
                        },
                        new
                        {
                            Id = 202,
                            Description = "Can delete products",
                            Name = "ProductDelete"
                        },
                        new
                        {
                            Id = 300,
                            Description = "Can read dealer",
                            Name = "DealerRead"
                        },
                        new
                        {
                            Id = 301,
                            Description = "Can update dealer",
                            Name = "DealerUpdate"
                        });
                });

            modelBuilder.Entity("NewAvalon.UserAdministration.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOnUtc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Dealer user",
                            Name = "DealerUser"
                        },
                        new
                        {
                            Id = 2,
                            CreatedOnUtc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Super admin",
                            Name = "SuperAdmin"
                        },
                        new
                        {
                            Id = 3,
                            CreatedOnUtc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Client",
                            Name = "Client"
                        });
                });

            modelBuilder.Entity("NewAvalon.UserAdministration.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid?>("IdentityProviderId")
                        .HasColumnType("uuid");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e486bbf4-f94b-4e61-9d9f-01ce92cccb2d"),
                            Address = "Admin address",
                            CreatedOnUtc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@admin.com",
                            FirstName = "Admin",
                            LastName = "Admin",
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<int>("PermissionsId")
                        .HasColumnType("integer");

                    b.Property<int>("RolesId")
                        .HasColumnType("integer");

                    b.HasKey("PermissionsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("PermissionRole");

                    b.HasData(
                        new
                        {
                            PermissionsId = 1,
                            RolesId = 2
                        },
                        new
                        {
                            PermissionsId = 100,
                            RolesId = 2
                        },
                        new
                        {
                            PermissionsId = 100,
                            RolesId = 1
                        },
                        new
                        {
                            PermissionsId = 100,
                            RolesId = 3
                        },
                        new
                        {
                            PermissionsId = 101,
                            RolesId = 3
                        },
                        new
                        {
                            PermissionsId = 200,
                            RolesId = 1
                        },
                        new
                        {
                            PermissionsId = 201,
                            RolesId = 1
                        },
                        new
                        {
                            PermissionsId = 202,
                            RolesId = 1
                        },
                        new
                        {
                            PermissionsId = 300,
                            RolesId = 2
                        },
                        new
                        {
                            PermissionsId = 301,
                            RolesId = 2
                        },
                        new
                        {
                            PermissionsId = 102,
                            RolesId = 3
                        });
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");

                    b.HasData(
                        new
                        {
                            RolesId = 2,
                            UsersId = new Guid("e486bbf4-f94b-4e61-9d9f-01ce92cccb2d")
                        });
                });

            modelBuilder.Entity("NewAvalon.UserAdministration.Domain.Entities.Client", b =>
                {
                    b.HasOne("NewAvalon.UserAdministration.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("NewAvalon.UserAdministration.Domain.Entities.Client", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NewAvalon.UserAdministration.Domain.Entities.Dealer", b =>
                {
                    b.HasOne("NewAvalon.UserAdministration.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("NewAvalon.UserAdministration.Domain.Entities.Dealer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NewAvalon.UserAdministration.Domain.Entities.User", b =>
                {
                    b.OwnsOne("NewAvalon.UserAdministration.Domain.ValueObjects.ProfileImage", "ProfileImage", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("ProfileImageId");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ProfileImageUrl");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("ProfileImage")
                        .IsRequired();
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("NewAvalon.UserAdministration.Domain.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewAvalon.UserAdministration.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("NewAvalon.UserAdministration.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewAvalon.UserAdministration.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
