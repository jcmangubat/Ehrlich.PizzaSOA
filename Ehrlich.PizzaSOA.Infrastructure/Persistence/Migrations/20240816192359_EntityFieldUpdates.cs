using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehrlich.PizzaSOA.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EntityFieldUpdates : Migration
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
                defaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 387, DateTimeKind.Local).AddTicks(8967),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 161, DateTimeKind.Local).AddTicks(9423));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Pizzas",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(1837),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(2030));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Orders",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(6321),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(6050));

            migrationBuilder.AddColumn<int>(
                name: "OrderNo",
                schema: "dbo",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "OrderDetails",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(8914),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(8382));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNo",
                schema: "dbo",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PizzaTypes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 161, DateTimeKind.Local).AddTicks(9423),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 387, DateTimeKind.Local).AddTicks(8967));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Pizzas",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(2030),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(1837));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Orders",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(6050),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(6321));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "OrderDetails",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(8382),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(8914));
        }
    }
}
