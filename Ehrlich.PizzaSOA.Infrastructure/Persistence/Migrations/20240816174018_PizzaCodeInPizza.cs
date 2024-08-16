using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehrlich.PizzaSOA.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PizzaCodeInPizza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PizzaItemCode",
                schema: "dbo",
                table: "Pizzas",
                newName: "PizzaTypeCode");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PizzaTypes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 161, DateTimeKind.Local).AddTicks(9423),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 351, DateTimeKind.Local).AddTicks(6660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Pizzas",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(2030),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 351, DateTimeKind.Local).AddTicks(9118));

            migrationBuilder.AddColumn<string>(
                name: "PizzaCode",
                schema: "dbo",
                table: "Pizzas",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Orders",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(6050),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 352, DateTimeKind.Local).AddTicks(2671));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "OrderDetails",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(8382),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 352, DateTimeKind.Local).AddTicks(4750));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PizzaCode",
                schema: "dbo",
                table: "Pizzas");

            migrationBuilder.RenameColumn(
                name: "PizzaTypeCode",
                schema: "dbo",
                table: "Pizzas",
                newName: "PizzaItemCode");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PizzaTypes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 351, DateTimeKind.Local).AddTicks(6660),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 161, DateTimeKind.Local).AddTicks(9423));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Pizzas",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 351, DateTimeKind.Local).AddTicks(9118),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(2030));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Orders",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 352, DateTimeKind.Local).AddTicks(2671),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(6050));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "OrderDetails",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 352, DateTimeKind.Local).AddTicks(4750),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 1, 40, 18, 162, DateTimeKind.Local).AddTicks(8382));
        }
    }
}
