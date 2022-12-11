using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoadedApps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AgentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadedApps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoadedApps_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoadedApps_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "Login", "Password" },
                values: new object[] { 1, "Test", "Test" });

            migrationBuilder.InsertData(
                table: "Computers",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "LoadedApps",
                columns: new[] { "Id", "AgentId", "ComputerId", "DateTime", "Name" },
                values: new object[] { 1, 1, 1, new DateTime(2022, 12, 11, 13, 30, 40, 218, DateTimeKind.Local).AddTicks(8194), "Приложение 1" });

            migrationBuilder.InsertData(
                table: "LoadedApps",
                columns: new[] { "Id", "AgentId", "ComputerId", "DateTime", "Name" },
                values: new object[] { 2, 1, 1, new DateTime(2022, 12, 11, 13, 30, 40, 218, DateTimeKind.Local).AddTicks(8210), "Приложение 2" });

            migrationBuilder.InsertData(
                table: "LoadedApps",
                columns: new[] { "Id", "AgentId", "ComputerId", "DateTime", "Name" },
                values: new object[] { 3, 1, 1, new DateTime(2022, 12, 11, 13, 30, 40, 218, DateTimeKind.Local).AddTicks(8211), "Приложение 3" });

            migrationBuilder.InsertData(
                table: "LoadedApps",
                columns: new[] { "Id", "AgentId", "ComputerId", "DateTime", "Name" },
                values: new object[] { 4, 1, 1, new DateTime(2022, 12, 11, 13, 30, 40, 218, DateTimeKind.Local).AddTicks(8212), "Приложение 4" });

            migrationBuilder.CreateIndex(
                name: "IX_LoadedApps_AgentId",
                table: "LoadedApps",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadedApps_ComputerId",
                table: "LoadedApps",
                column: "ComputerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoadedApps");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Computers");
        }
    }
}
