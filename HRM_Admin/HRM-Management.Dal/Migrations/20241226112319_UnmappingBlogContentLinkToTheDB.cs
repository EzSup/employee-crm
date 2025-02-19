using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM_Management.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UnmappingBlogContentLinkToTheDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeputyLeaderName",
                table: "Hubs");

            migrationBuilder.DropColumn(
                name: "DirectorName",
                table: "Hubs");

            migrationBuilder.DropColumn(
                name: "LeaderName",
                table: "Hubs");

            migrationBuilder.AddColumn<int>(
                name: "DeputyLeaderId",
                table: "Hubs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DirectorId",
                table: "Hubs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeaderId",
                table: "Hubs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hubs_DeputyLeaderId",
                table: "Hubs",
                column: "DeputyLeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hubs_DirectorId",
                table: "Hubs",
                column: "DirectorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hubs_LeaderId",
                table: "Hubs",
                column: "LeaderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hubs_Employees_DeputyLeaderId",
                table: "Hubs",
                column: "DeputyLeaderId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Hubs_Employees_DirectorId",
                table: "Hubs",
                column: "DirectorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Hubs_Employees_LeaderId",
                table: "Hubs",
                column: "LeaderId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hubs_Employees_DeputyLeaderId",
                table: "Hubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Hubs_Employees_DirectorId",
                table: "Hubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Hubs_Employees_LeaderId",
                table: "Hubs");

            migrationBuilder.DropIndex(
                name: "IX_Hubs_DeputyLeaderId",
                table: "Hubs");

            migrationBuilder.DropIndex(
                name: "IX_Hubs_DirectorId",
                table: "Hubs");

            migrationBuilder.DropIndex(
                name: "IX_Hubs_LeaderId",
                table: "Hubs");

            migrationBuilder.DropColumn(
                name: "DeputyLeaderId",
                table: "Hubs");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "Hubs");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "Hubs");

            migrationBuilder.AddColumn<string>(
                name: "DeputyLeaderName",
                table: "Hubs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DirectorName",
                table: "Hubs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LeaderName",
                table: "Hubs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
