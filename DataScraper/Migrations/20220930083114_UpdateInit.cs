using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataScraper.Migrations
{
    public partial class UpdateInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Platform",
                columns: new[] { "Id", "PlatformName" },
                values: new object[,]
                {
                    { 1, "PlayStation 5" },
                    { 2, "PlayStation 4" },
                    { 3, "PC" },
                    { 4, "Xbox Series X" },
                    { 5, "Xbox One" },
                    { 6, "Switch" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
