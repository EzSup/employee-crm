using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM_Management.Dal.Migrations
{
    /// <inheritdoc />
    public partial class JobColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Subscriptions",
                newName: "Job");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Job",
                table: "Subscriptions",
                newName: "Type");
        }
    }
}
