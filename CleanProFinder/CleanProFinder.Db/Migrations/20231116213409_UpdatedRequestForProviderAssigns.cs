using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanProFinder.Db.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRequestForProviderAssigns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CleaningServiceProviderId",
                table: "Requests",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderId",
                table: "Requests",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ProviderPrice",
                table: "Requests",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CleaningServiceProviderId",
                table: "Requests",
                column: "CleaningServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ProviderId",
                table: "Requests",
                column: "ProviderId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Requests_ProviderId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CleaningServiceProviderId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ProviderPrice",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Requests");
        }
    }
}
