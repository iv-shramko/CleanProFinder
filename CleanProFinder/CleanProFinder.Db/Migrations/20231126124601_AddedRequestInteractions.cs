using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanProFinder.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddedRequestInteractions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_CleaningServiceProviders_ProviderId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ProviderId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ProviderPrice",
                table: "Requests");

            migrationBuilder.CreateTable(
                name: "RequestInteractions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uuid", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestInteractions_CleaningServiceProviders_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "CleaningServiceProviders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestInteractions_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestInteractions_ProviderId",
                table: "RequestInteractions",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestInteractions_RequestId",
                table: "RequestInteractions",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestInteractions");

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

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ProviderId",
                table: "Requests",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_CleaningServiceProviders_ProviderId",
                table: "Requests",
                column: "ProviderId",
                principalTable: "CleaningServiceProviders",
                principalColumn: "Id");
        }
    }
}
