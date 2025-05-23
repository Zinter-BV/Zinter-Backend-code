using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateMoveHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZN_MoveHostories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveRequestId = table.Column<long>(type: "bigint", nullable: false),
                    MoveAgentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoveStatus = table.Column<int>(type: "int", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScheduledTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZN_MoveHostories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZN_MoveHostories_ZN_Move_Requests_MoveRequestId",
                        column: x => x.MoveRequestId,
                        principalTable: "ZN_Move_Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZN_MoveHostories_ZN_Moving_Agents_MoveAgentId",
                        column: x => x.MoveAgentId,
                        principalTable: "ZN_Moving_Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZN_MoveHostories_MoveAgentId",
                table: "ZN_MoveHostories",
                column: "MoveAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ZN_MoveHostories_MoveRequestId",
                table: "ZN_MoveHostories",
                column: "MoveRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZN_MoveHostories");
        }
    }
}
