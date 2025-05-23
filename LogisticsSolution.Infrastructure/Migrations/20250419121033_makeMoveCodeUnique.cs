using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class makeMoveCodeUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ZN_Move_Requests_MoveCode",
                table: "ZN_Move_Requests",
                column: "MoveCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ZN_Move_Requests_MoveCode",
                table: "ZN_Move_Requests");
        }
    }
}
