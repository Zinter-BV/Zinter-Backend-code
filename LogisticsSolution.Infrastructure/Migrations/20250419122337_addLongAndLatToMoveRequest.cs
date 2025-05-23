using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addLongAndLatToMoveRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DropOffLatitude",
                table: "ZN_Move_Requests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DropOffLongitude",
                table: "ZN_Move_Requests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PickUpLatitude",
                table: "ZN_Move_Requests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PickUpLongitude",
                table: "ZN_Move_Requests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropOffLatitude",
                table: "ZN_Move_Requests");

            migrationBuilder.DropColumn(
                name: "DropOffLongitude",
                table: "ZN_Move_Requests");

            migrationBuilder.DropColumn(
                name: "PickUpLatitude",
                table: "ZN_Move_Requests");

            migrationBuilder.DropColumn(
                name: "PickUpLongitude",
                table: "ZN_Move_Requests");
        }
    }
}
