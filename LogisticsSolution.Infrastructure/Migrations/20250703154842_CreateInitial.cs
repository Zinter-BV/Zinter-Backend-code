using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                name: "ZN_MailingList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZN_MailingList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZN_Moving_Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CompanyName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    KvkNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    CompanyOverView = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZN_Moving_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZN_Province",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZN_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZN_AgentsProvince",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgentId = table.Column<int>(type: "integer", nullable: false),
                    ProvinceId = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ZN_Move_Requests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ProvinceId = table.Column<int>(type: "integer", nullable: false),
                    MoveStatus = table.Column<int>(type: "integer", nullable: false),
                    PickUpAddress = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    DropOffAddress = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    MoveTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PickUpTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumberOfFloors = table.Column<int>(type: "integer", nullable: false),
                    LongCarry = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PickUpLongitude = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PickUpLatitude = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DropOffLongitude = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DropOffLatitude = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MoveCode = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    HasElevator = table.Column<bool>(type: "boolean", nullable: false),
                    NeedShuttle = table.Column<bool>(type: "boolean", nullable: false),
                    HasBuildingInsurance = table.Column<bool>(type: "boolean", nullable: false),
                    NeedHelpPacking = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ZN_Move_Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MoveRequestId = table.Column<long>(type: "bigint", nullable: false),
                    RoomName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    ItemName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    ItemCount = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ZN_MoveHostories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MoveRequestId = table.Column<long>(type: "bigint", nullable: false),
                    MoveAgentId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MoveStatus = table.Column<int>(type: "integer", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ScheduledTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ZN_Quotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MoveRequestId = table.Column<long>(type: "bigint", nullable: false),
                    MovingAgentId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsAccepted = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProposedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AdditonalInformation = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_ZN_AgentsProvince_AgentId",
                table: "ZN_AgentsProvince",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ZN_AgentsProvince_ProvinceId",
                table: "ZN_AgentsProvince",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ZN_Move_Items_MoveRequestId",
                table: "ZN_Move_Items",
                column: "MoveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ZN_Move_Requests_MoveCode",
                table: "ZN_Move_Requests",
                column: "MoveCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZN_Move_Requests_ProvinceId",
                table: "ZN_Move_Requests",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ZN_MoveHostories_MoveAgentId",
                table: "ZN_MoveHostories",
                column: "MoveAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ZN_MoveHostories_MoveRequestId",
                table: "ZN_MoveHostories",
                column: "MoveRequestId");

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
                name: "ZN_AgentsProvince");

            migrationBuilder.DropTable(
                name: "ZN_MailingList");

            migrationBuilder.DropTable(
                name: "ZN_Move_Items");

            migrationBuilder.DropTable(
                name: "ZN_MoveHostories");

            migrationBuilder.DropTable(
                name: "ZN_Quotes");

            migrationBuilder.DropTable(
                name: "ZN_Move_Requests");

            migrationBuilder.DropTable(
                name: "ZN_Moving_Agents");

            migrationBuilder.DropTable(
                name: "ZN_Province");
        }
    }
}
