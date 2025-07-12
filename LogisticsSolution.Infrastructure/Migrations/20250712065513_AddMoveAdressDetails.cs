using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoveAdressDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Remark",
                table: "ZN_Move_Requests",
                newName: "ToRemark");

            migrationBuilder.RenameColumn(
                name: "NumberOfFloors",
                table: "ZN_Move_Requests",
                newName: "ToNumberOfFloors");

            migrationBuilder.RenameColumn(
                name: "NeedShuttle",
                table: "ZN_Move_Requests",
                newName: "ToNeedShuttle");

            migrationBuilder.RenameColumn(
                name: "NeedHelpPacking",
                table: "ZN_Move_Requests",
                newName: "ToNeedHelpPacking");

            migrationBuilder.RenameColumn(
                name: "LongCarry",
                table: "ZN_Move_Requests",
                newName: "ToLongCarry");

            migrationBuilder.RenameColumn(
                name: "HasElevator",
                table: "ZN_Move_Requests",
                newName: "ToHasElevator");

            migrationBuilder.RenameColumn(
                name: "HasBuildingInsurance",
                table: "ZN_Move_Requests",
                newName: "ToHasBuildingInsurance");

            migrationBuilder.AddColumn<string>(
                name: "DropOffAddressNumber",
                table: "ZN_Move_Requests",
                type: "character varying(7)",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FromHasBuildingInsurance",
                table: "ZN_Move_Requests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FromHasElevator",
                table: "ZN_Move_Requests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FromLongCarry",
                table: "ZN_Move_Requests",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FromNeedHelpPacking",
                table: "ZN_Move_Requests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FromNeedShuttle",
                table: "ZN_Move_Requests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FromNumberOfFloors",
                table: "ZN_Move_Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FromRemark",
                table: "ZN_Move_Requests",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickUpAddressNumber",
                table: "ZN_Move_Requests",
                type: "character varying(7)",
                maxLength: 7,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropOffAddressNumber",
                table: "ZN_Move_Requests");

            migrationBuilder.DropColumn(
                name: "FromHasBuildingInsurance",
                table: "ZN_Move_Requests");

            migrationBuilder.DropColumn(
                name: "FromHasElevator",
                table: "ZN_Move_Requests");

            migrationBuilder.DropColumn(
                name: "FromLongCarry",
                table: "ZN_Move_Requests");

            migrationBuilder.DropColumn(
                name: "FromNeedHelpPacking",
                table: "ZN_Move_Requests");

            migrationBuilder.DropColumn(
                name: "FromNeedShuttle",
                table: "ZN_Move_Requests");

            migrationBuilder.DropColumn(
                name: "FromNumberOfFloors",
                table: "ZN_Move_Requests");

            migrationBuilder.DropColumn(
                name: "FromRemark",
                table: "ZN_Move_Requests");

            migrationBuilder.DropColumn(
                name: "PickUpAddressNumber",
                table: "ZN_Move_Requests");

            migrationBuilder.RenameColumn(
                name: "ToRemark",
                table: "ZN_Move_Requests",
                newName: "Remark");

            migrationBuilder.RenameColumn(
                name: "ToNumberOfFloors",
                table: "ZN_Move_Requests",
                newName: "NumberOfFloors");

            migrationBuilder.RenameColumn(
                name: "ToNeedShuttle",
                table: "ZN_Move_Requests",
                newName: "NeedShuttle");

            migrationBuilder.RenameColumn(
                name: "ToNeedHelpPacking",
                table: "ZN_Move_Requests",
                newName: "NeedHelpPacking");

            migrationBuilder.RenameColumn(
                name: "ToLongCarry",
                table: "ZN_Move_Requests",
                newName: "LongCarry");

            migrationBuilder.RenameColumn(
                name: "ToHasElevator",
                table: "ZN_Move_Requests",
                newName: "HasElevator");

            migrationBuilder.RenameColumn(
                name: "ToHasBuildingInsurance",
                table: "ZN_Move_Requests",
                newName: "HasBuildingInsurance");
        }
    }
}
