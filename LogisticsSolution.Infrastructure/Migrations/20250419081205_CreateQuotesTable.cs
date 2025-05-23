using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateQuotesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZN_Quotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveRequestId = table.Column<long>(type: "bigint", nullable: false),
                    MovingAgentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZN_Quotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZN_Quotes_ZN_Move_Requests_MoveRequestId",
                        column: x => x.MoveRequestId,
                        principalTable: "ZN_Move_Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZN_Quotes_ZN_Moving_Agents_MovingAgentId",
                        column: x => x.MovingAgentId,
                        principalTable: "ZN_Moving_Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZN_Quotes_MoveRequestId",
                table: "ZN_Quotes",
                column: "MoveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ZN_Quotes_MovingAgentId",
                table: "ZN_Quotes",
                column: "MovingAgentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZN_Quotes");
        }
    }
}
