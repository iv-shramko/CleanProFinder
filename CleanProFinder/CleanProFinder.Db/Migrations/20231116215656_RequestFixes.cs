using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanProFinder.Db.Migrations
{
    /// <inheritdoc />
    public partial class RequestFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_CleaningServiceProviders_CleaningServiceProviderId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Requests_ProviderId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CleaningServiceProviderId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CleaningServiceProviderId",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_CleaningServiceProviders_ProviderId",
                table: "Requests",
                column: "ProviderId",
                principalTable: "CleaningServiceProviders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_CleaningServiceProviders_ProviderId",
                table: "Requests");

            migrationBuilder.AddColumn<Guid>(
                name: "CleaningServiceProviderId",
                table: "Requests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CleaningServiceProviderId",
                table: "Requests",
                column: "CleaningServiceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_CleaningServiceProviders_CleaningServiceProviderId",
                table: "Requests",
                column: "CleaningServiceProviderId",
                principalTable: "CleaningServiceProviders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Requests_ProviderId",
                table: "Requests",
                column: "ProviderId",
                principalTable: "Requests",
                principalColumn: "Id");
        }
    }
}
