using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMovingCodeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemNumber",
                table: "ZN_Move_Items",
                newName: "ItemCount");

            migrationBuilder.AddColumn<string>(
                name: "MoveCode",
                table: "ZN_Move_Requests",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoveCode",
                table: "ZN_Move_Requests");

            migrationBuilder.RenameColumn(
                name: "ItemCount",
                table: "ZN_Move_Items",
                newName: "ItemNumber");
        }
    }
}
