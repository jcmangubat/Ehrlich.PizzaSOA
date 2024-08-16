﻿// <auto-generated />
using System;
using Ehrlich.PizzaSOA.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ehrlich.PizzaSOA.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240816192359_EntityFieldUpdates")]
    partial class EntityFieldUpdates
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ehrlich.PizzaSOA.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OrderId")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("DateTime2")
                        .HasDefaultValue(new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(6321))
                        .HasColumnName("_dateCreated")
                        .HasColumnOrder(501);

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("DateTime2")
                        .HasColumnName("_dateModified")
                        .HasColumnOrder(508);

                    b.Property<DateTime>("DateOrdered")
                        .HasColumnType("datetime2")
                        .HasColumnName("DateOrdered");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit")
                        .HasDefaultValue(true)
                        .HasColumnOrder(506);

                    b.Property<int>("OrderNo")
                        .HasColumnType("int")
                        .HasColumnName("OrderNo");

                    b.Property<TimeSpan>("TimeOrdered")
                        .HasColumnType("time")
                        .HasColumnName("TimeOrdered");

                    b.HasKey("Id");

                    b.ToTable("Orders", "dbo");
                });

            modelBuilder.Entity("Ehrlich.PizzaSOA.Domain.Entities.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OrderDetailId")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("DateTime2")
                        .HasDefaultValue(new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(8914))
                        .HasColumnName("_dateCreated")
                        .HasColumnOrder(501);

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("DateTime2")
                        .HasColumnName("_dateModified")
                        .HasColumnOrder(508);

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit")
                        .HasDefaultValue(true)
                        .HasColumnOrder(506);

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PizzaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PizzaId");

                    b.ToTable("OrderDetails", "dbo");
                });

            modelBuilder.Entity("Ehrlich.PizzaSOA.Domain.Entities.Pizza", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PizzaId")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("DateTime2")
                        .HasDefaultValue(new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(1837))
                        .HasColumnName("_dateCreated")
                        .HasColumnOrder(501);

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("DateTime2")
                        .HasColumnName("_dateModified")
                        .HasColumnOrder(508);

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit")
                        .HasDefaultValue(true)
                        .HasColumnOrder(506);

                    b.Property<string>("PizzaCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("PizzaCode");

                    b.Property<string>("PizzaTypeCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("PizzaTypeCode");

                    b.Property<Guid>("PizzaTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.Property<int>("Size")
                        .HasColumnType("int")
                        .HasColumnName("Size");

                    b.HasKey("Id");

                    b.HasIndex("PizzaTypeId");

                    b.ToTable("Pizzas", "dbo");
                });

            modelBuilder.Entity("Ehrlich.PizzaSOA.Domain.Entities.PizzaType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PizzaTypeId")
                        .HasColumnOrder(0);

                    b.Property<int>("Category")
                        .HasColumnType("int")
                        .HasColumnName("Category");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("DateTime2")
                        .HasDefaultValue(new DateTime(2024, 8, 17, 3, 23, 58, 387, DateTimeKind.Local).AddTicks(8967))
                        .HasColumnName("_dateCreated")
                        .HasColumnOrder(501);

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("DateTime2")
                        .HasColumnName("_dateModified")
                        .HasColumnOrder(508);

                    b.Property<string>("Ingredients")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Ingredients");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit")
                        .HasDefaultValue(true)
                        .HasColumnOrder(506);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasColumnName("Name");

                    b.Property<string>("PizzaTypeCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("PizzaTypeCode");

                    b.HasKey("Id");

                    b.ToTable("PizzaTypes", "dbo");
                });

            modelBuilder.Entity("Ehrlich.PizzaSOA.Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("Ehrlich.PizzaSOA.Domain.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ehrlich.PizzaSOA.Domain.Entities.Pizza", "Pizza")
                        .WithMany("OrderDetails")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("Ehrlich.PizzaSOA.Domain.Entities.Pizza", b =>
                {
                    b.HasOne("Ehrlich.PizzaSOA.Domain.Entities.PizzaType", "PizzaType")
                        .WithMany("Pizzas")
                        .HasForeignKey("PizzaTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PizzaType");
                });

            modelBuilder.Entity("Ehrlich.PizzaSOA.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Ehrlich.PizzaSOA.Domain.Entities.Pizza", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Ehrlich.PizzaSOA.Domain.Entities.PizzaType", b =>
                {
                    b.Navigation("Pizzas");
                });
#pragma warning restore 612, 618
        }
    }
}
