using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubmittedApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Plan = table.Column<string>(type: "text", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittedApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmittedApplications_ApplicationActivities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ApplicationActivities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnsubmittedApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Plan = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsubmittedApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnsubmittedApplications_ApplicationActivities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ApplicationActivities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationActivities_Name",
                table: "ApplicationActivities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedApplications_ActivityId",
                table: "SubmittedApplications",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedApplications_UserId",
                table: "SubmittedApplications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UnsubmittedApplications_ActivityId",
                table: "UnsubmittedApplications",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_UnsubmittedApplications_UserId",
                table: "UnsubmittedApplications",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmittedApplications");

            migrationBuilder.DropTable(
                name: "UnsubmittedApplications");

            migrationBuilder.DropTable(
                name: "ApplicationActivities");
        }
    }
}
