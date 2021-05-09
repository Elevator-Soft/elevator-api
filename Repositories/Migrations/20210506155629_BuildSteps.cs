using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class BuildSteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildStepScript",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Command = table.Column<string>(type: "text", nullable: true),
                    Arguments = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildStepScript", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    BuildConfigId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildStepScriptId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildSteps_BuildConfigs_BuildConfigId",
                        column: x => x.BuildConfigId,
                        principalTable: "BuildConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildSteps_BuildStepScript_BuildStepScriptId",
                        column: x => x.BuildStepScriptId,
                        principalTable: "BuildStepScript",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildSteps_BuildConfigId",
                table: "BuildSteps",
                column: "BuildConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildSteps_BuildStepScriptId",
                table: "BuildSteps",
                column: "BuildStepScriptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildSteps");

            migrationBuilder.DropTable(
                name: "BuildStepScript");
        }
    }
}
