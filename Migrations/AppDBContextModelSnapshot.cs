﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SevenEleven.Data;

namespace SevenEleven.Migrations
{
      [DbContext(typeof(AppDBContext))]
      partial class AppDBContextModelSnapshot : ModelSnapshot
      {
            protected override void BuildModel(ModelBuilder modelBuilder)
            {
#pragma warning disable 612, 618
                  modelBuilder
                      .HasAnnotation("ProductVersion", "3.1.11")
                      .HasAnnotation("Relational:MaxIdentifierLength", 128)
                      .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                  modelBuilder.Entity("NetCoreAPI_Template_v3_with_auth.Models.Role", b =>
                      {
                            b.Property<Guid>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("uniqueidentifier");

                            b.Property<string>("Name")
                          .IsRequired()
                          .HasColumnType("nvarchar(20)")
                          .HasMaxLength(20);

                            b.HasKey("Id");

                            b.ToTable("Role", "auth");

                            b.HasData(
                          new
                              {
                                    Id = new Guid("acc599f7-8397-4c27-b9f0-d9578d75a166"),
                                    Name = "user"
                              },
                          new
                              {
                                    Id = new Guid("02686086-af64-4d92-99ea-7b72415c6e72"),
                                    Name = "Manager"
                              },
                          new
                              {
                                    Id = new Guid("cfac6785-9e26-47dd-8e7a-4b3ce043e9f2"),
                                    Name = "Admin"
                              },
                          new
                              {
                                    Id = new Guid("fcba9e82-03e7-4ddf-b215-de176d4a5bac"),
                                    Name = "Developer"
                              });
                      });

                  modelBuilder.Entity("NetCoreAPI_Template_v3_with_auth.Models.User", b =>
                      {
                            b.Property<Guid>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("uniqueidentifier");

                            b.Property<byte[]>("PasswordHash")
                          .IsRequired()
                          .HasColumnType("varbinary(max)");

                            b.Property<byte[]>("PasswordSalt")
                          .IsRequired()
                          .HasColumnType("varbinary(max)");

                            b.Property<string>("Username")
                          .IsRequired()
                          .HasColumnType("nvarchar(20)")
                          .HasMaxLength(20);

                            b.HasKey("Id");

                            b.ToTable("User", "auth");
                      });

                  modelBuilder.Entity("NetCoreAPI_Template_v3_with_auth.Models.UserRole", b =>
                      {
                            b.Property<Guid>("UserId")
                          .HasColumnType("uniqueidentifier");

                            b.Property<Guid>("RoleId")
                          .HasColumnType("uniqueidentifier");

                            b.HasKey("UserId", "RoleId");

                            b.HasIndex("RoleId");

                            b.ToTable("UserRole", "auth");
                      });

                  modelBuilder.Entity("SevenEleven.Models.Order", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("int")
                          .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b.Property<DateTime>("CreatedDate")
                          .HasColumnType("datetime2");

                            b.Property<float>("Discount")
                          .HasColumnType("real");

                            b.Property<bool>("IsActive")
                          .HasColumnType("bit");

                            b.Property<float>("Net")
                          .HasColumnType("real");

                            b.Property<float>("Total")
                          .HasColumnType("real");

                            b.HasKey("Id");

                            b.ToTable("Order");
                      });

                  modelBuilder.Entity("SevenEleven.Models.OrderItem", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("int")
                          .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b.Property<int>("OrderId")
                          .HasColumnType("int");

                            b.Property<float>("Price")
                          .HasColumnType("real");

                            b.Property<int>("ProductId")
                          .HasColumnType("int");

                            b.Property<float>("Quantity")
                          .HasColumnType("real");

                            b.Property<float>("Total")
                          .HasColumnType("real");

                            b.HasKey("Id");

                            b.HasIndex("OrderId");

                            b.HasIndex("ProductId");

                            b.ToTable("OrderItem");
                      });

                  modelBuilder.Entity("SevenEleven.Models.Product", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("int")
                          .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b.Property<DateTime>("CreatedDate")
                          .HasColumnType("datetime2");

                            b.Property<bool>("IsActive")
                          .HasColumnType("bit");

                            b.Property<string>("Name")
                          .IsRequired()
                          .HasColumnType("nvarchar(255)")
                          .HasMaxLength(255);

                            b.Property<float>("Price")
                          .HasColumnType("real");

                            b.Property<int>("ProductGroupId")
                          .HasColumnType("int");

                            b.HasKey("Id");

                            b.HasIndex("ProductGroupId");

                            b.ToTable("Product");
                      });

                  modelBuilder.Entity("SevenEleven.Models.ProductGroup", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("int")
                          .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b.Property<DateTime>("CreatedDate")
                          .HasColumnType("datetime2");

                            b.Property<bool>("IsActive")
                          .HasColumnType("bit");

                            b.Property<string>("Name")
                          .IsRequired()
                          .HasColumnType("nvarchar(255)")
                          .HasMaxLength(255);

                            b.HasKey("Id");

                            b.ToTable("ProductGroup");
                      });

                  modelBuilder.Entity("NetCoreAPI_Template_v3_with_auth.Models.UserRole", b =>
                      {
                            b.HasOne("NetCoreAPI_Template_v3_with_auth.Models.Role", "Role")
                          .WithMany()
                          .HasForeignKey("RoleId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();

                            b.HasOne("NetCoreAPI_Template_v3_with_auth.Models.User", "User")
                          .WithMany()
                          .HasForeignKey("UserId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();
                      });

                  modelBuilder.Entity("SevenEleven.Models.OrderItem", b =>
                      {
                            b.HasOne("SevenEleven.Models.Order", "Orders")
                          .WithMany("OrderItems")
                          .HasForeignKey("OrderId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();

                            b.HasOne("SevenEleven.Models.Product", "Products")
                          .WithMany("OrderItems")
                          .HasForeignKey("ProductId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();
                      });

                  modelBuilder.Entity("SevenEleven.Models.Product", b =>
                      {
                            b.HasOne("SevenEleven.Models.ProductGroup", "ProductGroups")
                          .WithMany("Products")
                          .HasForeignKey("ProductGroupId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();
                      });
#pragma warning restore 612, 618
            }
      }
}
