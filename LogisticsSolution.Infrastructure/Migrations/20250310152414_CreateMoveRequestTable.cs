using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateMoveRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZN_Move_Requests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    MoveStatus = table.Column<int>(type: "int", nullable: false),
                    PickUpAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DropOffAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MoveTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PickUpTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfFloors = table.Column<int>(type: "int", nullable: false),
                    LongCarry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HasElevator = table.Column<bool>(type: "bit", nullable: false),
                    NeedShuttle = table.Column<bool>(type: "bit", nullable: false),
                    HasBuildingInsurance = table.Column<bool>(type: "bit", nullable: false),
                    NeedHelpPacking = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZN_Move_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZN_Move_Requests_ZN_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ZN_Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZN_Move_Requests_ProvinceId",
                table: "ZN_Move_Requests",
                column: "ProvinceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZN_Move_Requests");
        }
    }
}
