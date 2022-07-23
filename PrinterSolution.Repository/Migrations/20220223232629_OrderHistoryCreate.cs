using Microsoft.EntityFrameworkCore.Migrations;

namespace PrinterSolution.Repository.Migrations
{
    public partial class OrderHistoryCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Printer_Material_CoupledMaterialId",
                table: "Printer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Printer",
                table: "Printer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceRule",
                table: "PriceRule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Configuration",
                table: "Configuration");

            migrationBuilder.RenameTable(
                name: "Printer",
                newName: "Printers");

            migrationBuilder.RenameTable(
                name: "PriceRule",
                newName: "PriceRules");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "Materials");

            migrationBuilder.RenameTable(
                name: "Configuration",
                newName: "Configurations");

            migrationBuilder.RenameIndex(
                name: "IX_Printer_CoupledMaterialId",
                table: "Printers",
                newName: "IX_Printers_CoupledMaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Printers",
                table: "Printers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceRules",
                table: "PriceRules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configurations",
                table: "Configurations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_OrderId",
                table: "OrderHistories",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_Materials_CoupledMaterialId",
                table: "Printers",
                column: "CoupledMaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Printers_Materials_CoupledMaterialId",
                table: "Printers");

            migrationBuilder.DropTable(
                name: "OrderHistories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Printers",
                table: "Printers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceRules",
                table: "PriceRules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Configurations",
                table: "Configurations");

            migrationBuilder.RenameTable(
                name: "Printers",
                newName: "Printer");

            migrationBuilder.RenameTable(
                name: "PriceRules",
                newName: "PriceRule");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Material");

            migrationBuilder.RenameTable(
                name: "Configurations",
                newName: "Configuration");

            migrationBuilder.RenameIndex(
                name: "IX_Printers_CoupledMaterialId",
                table: "Printer",
                newName: "IX_Printer_CoupledMaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Printer",
                table: "Printer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceRule",
                table: "PriceRule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configuration",
                table: "Configuration",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Printer_Material_CoupledMaterialId",
                table: "Printer",
                column: "CoupledMaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
