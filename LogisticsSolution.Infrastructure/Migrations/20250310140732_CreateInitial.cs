using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LogisticsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZN_Province",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZN_Province", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ZN_Province",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Drenthe" },
                    { 2, "Flevoland" },
                    { 3, "Friesland (Fryslân)" },
                    { 4, "Gelderland" },
                    { 5, "Groningen" },
                    { 6, "Limburg" },
                    { 7, "Noord-Brabant" },
                    { 8, "Noord-Holland" },
                    { 9, "Overijssel" },
                    { 10, "Utrecht" },
                    { 11, "Zeeland" },
                    { 12, "Zuid-Holland" },
                    { 13, "Assen" },
                    { 14, "Lelystad" },
                    { 15, "Arnhem" },
                    { 16, "Groningen" },
                    { 17, "Maastricht" },
                    { 18, "'s-Hertogenbosch (Den Bosch)" },
                    { 19, "Haarlem" },
                    { 20, "Zwolle" },
                    { 21, "Middelburg" },
                    { 22, "The Hague (Den Haag)" },
                    { 23, "Leeuwarden" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZN_Province");
        }
    }
}
