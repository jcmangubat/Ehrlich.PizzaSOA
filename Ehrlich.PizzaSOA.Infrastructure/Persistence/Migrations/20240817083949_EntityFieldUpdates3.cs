using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehrlich.PizzaSOA.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EntityFieldUpdates3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PizzaTypes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 16, 39, 48, 794, DateTimeKind.Local).AddTicks(2440),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 218, DateTimeKind.Local).AddTicks(5829));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Pizzas",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 16, 39, 48, 794, DateTimeKind.Local).AddTicks(5295),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 218, DateTimeKind.Local).AddTicks(8460));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Orders",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 16, 39, 48, 794, DateTimeKind.Local).AddTicks(9764),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 219, DateTimeKind.Local).AddTicks(2429));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "OrderDetails",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 16, 39, 48, 795, DateTimeKind.Local).AddTicks(2287),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 219, DateTimeKind.Local).AddTicks(4990));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PizzaTypes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 218, DateTimeKind.Local).AddTicks(5829),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 16, 39, 48, 794, DateTimeKind.Local).AddTicks(2440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Pizzas",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 218, DateTimeKind.Local).AddTicks(8460),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 16, 39, 48, 794, DateTimeKind.Local).AddTicks(5295));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Orders",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 219, DateTimeKind.Local).AddTicks(2429),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 16, 39, 48, 794, DateTimeKind.Local).AddTicks(9764));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "OrderDetails",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 219, DateTimeKind.Local).AddTicks(4990),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 16, 39, 48, 795, DateTimeKind.Local).AddTicks(2287));
        }
    }
}
