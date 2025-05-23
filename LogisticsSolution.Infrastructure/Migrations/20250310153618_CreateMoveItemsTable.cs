using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateMoveItemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZN_Move_Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveRequestId = table.Column<long>(type: "bigint", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ItemNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZN_Move_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZN_Move_Items_ZN_Move_Requests_MoveRequestId",
                        column: x => x.MoveRequestId,
                        principalTable: "ZN_Move_Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZN_Move_Items_MoveRequestId",
                table: "ZN_Move_Items",
                column: "MoveRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZN_Move_Items");
        }
    }
}
