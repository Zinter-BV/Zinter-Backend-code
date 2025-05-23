using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateAgentAndProvincesCoveredTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZN_Moving_Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KvkNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyOverView = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZN_Moving_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZN_AgentsProvince",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentId = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZN_AgentsProvince", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZN_AgentsProvince_ZN_Moving_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "ZN_Moving_Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZN_AgentsProvince_ZN_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ZN_Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZN_AgentsProvince_AgentId",
                table: "ZN_AgentsProvince",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ZN_AgentsProvince_ProvinceId",
                table: "ZN_AgentsProvince",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ZN_Moving_Agents_Email",
                table: "ZN_Moving_Agents",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZN_Moving_Agents_KvkNumber",
                table: "ZN_Moving_Agents",
                column: "KvkNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZN_AgentsProvince");

            migrationBuilder.DropTable(
                name: "ZN_Moving_Agents");
        }
    }
}
