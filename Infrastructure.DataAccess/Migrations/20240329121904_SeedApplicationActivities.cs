using Domain.Applications;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedApplicationActivities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var activities = new List<ApplicationActivity>
            {
                new ApplicationActivity(Guid.NewGuid(), "Report", "Доклад, 35-45 минут"),
                new ApplicationActivity(Guid.NewGuid(), "Masterclass", "Мастеркласс, 1-2 часа"),
                new ApplicationActivity(Guid.NewGuid(), "Discussion", "Дискуссия / круглый стол, 40-50 минут"),
            };

            foreach (var activity in activities)
            {
                migrationBuilder.InsertData(
                    table: "ApplicationActivities",
                    columns: new[] { "Id", "Name", "Description" },
                    values: new object[] { activity.Id, activity.Name, activity.Description });
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"ApplicationActivities\"");
        }
    }
}