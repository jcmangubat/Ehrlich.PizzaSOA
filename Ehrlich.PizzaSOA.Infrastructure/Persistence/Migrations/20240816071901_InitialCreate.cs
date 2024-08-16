using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehrlich.PizzaSOA.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "dbo",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 352, DateTimeKind.Local).AddTicks(2671)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    DateOrdered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOrdered = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "PizzaTypes",
                schema: "dbo",
                columns: table => new
                {
                    PizzaTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 351, DateTimeKind.Local).AddTicks(6660)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    PizzaTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaTypes", x => x.PizzaTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Pizzas",
                schema: "dbo",
                columns: table => new
                {
                    PizzaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 351, DateTimeKind.Local).AddTicks(9118)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    PizzaItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PizzaTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzas", x => x.PizzaId);
                    table.ForeignKey(
                        name: "FK_Pizzas_PizzaTypes_PizzaTypeId",
                        column: x => x.PizzaTypeId,
                        principalSchema: "dbo",
                        principalTable: "PizzaTypes",
                        principalColumn: "PizzaTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                schema: "dbo",
                columns: table => new
                {
                    OrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 8, 16, 15, 19, 1, 352, DateTimeKind.Local).AddTicks(4750)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PizzaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalSchema: "dbo",
                        principalTable: "Pizzas",
                        principalColumn: "PizzaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                schema: "dbo",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PizzaId",
                schema: "dbo",
                table: "OrderDetails",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_PizzaTypeId",
                schema: "dbo",
                table: "Pizzas",
                column: "PizzaTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Pizzas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PizzaTypes",
                schema: "dbo");
        }
    }
}
