using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanProFinder.Db.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CleaningServices_CleaningServiceProviders_CleaningServicePr~",
                table: "CleaningServices");

            migrationBuilder.DropIndex(
                name: "IX_CleaningServices_CleaningServiceProviderId",
                table: "CleaningServices");

            migrationBuilder.DropColumn(
                name: "CleaningServiceProviderId",
                table: "CleaningServices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CleaningServiceProviderId",
                table: "CleaningServices",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CleaningServices_CleaningServiceProviderId",
                table: "CleaningServices",
                column: "CleaningServiceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningServices_CleaningServiceProviders_CleaningServicePr~",
                table: "CleaningServices",
                column: "CleaningServiceProviderId",
                principalTable: "CleaningServiceProviders",
                principalColumn: "Id");
        }
    }
}
