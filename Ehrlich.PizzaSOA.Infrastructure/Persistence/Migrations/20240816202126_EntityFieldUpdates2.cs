using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehrlich.PizzaSOA.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EntityFieldUpdates2 : Migration
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
                defaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 218, DateTimeKind.Local).AddTicks(5829),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 387, DateTimeKind.Local).AddTicks(8967));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Pizzas",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 218, DateTimeKind.Local).AddTicks(8460),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(1837));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Orders",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 219, DateTimeKind.Local).AddTicks(2429),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(6321));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "OrderDetails",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 219, DateTimeKind.Local).AddTicks(4990),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(8914));

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailNo",
                schema: "dbo",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDetailNo",
                schema: "dbo",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PizzaTypes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 387, DateTimeKind.Local).AddTicks(8967),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 218, DateTimeKind.Local).AddTicks(5829));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Pizzas",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(1837),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 218, DateTimeKind.Local).AddTicks(8460));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Orders",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(6321),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 219, DateTimeKind.Local).AddTicks(2429));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "OrderDetails",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 3, 23, 58, 388, DateTimeKind.Local).AddTicks(8914),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 4, 21, 26, 219, DateTimeKind.Local).AddTicks(4990));
        }
    }
}
