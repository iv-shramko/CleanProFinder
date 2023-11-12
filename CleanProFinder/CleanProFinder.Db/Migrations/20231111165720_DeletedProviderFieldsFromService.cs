using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanProFinder.Db.Migrations
{
    /// <inheritdoc />
    public partial class DeletedProviderFieldsFromService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CleaningServices_CleaningServiceProviders_ServiceProviderId",
                table: "CleaningServices");

            migrationBuilder.DropIndex(
                name: "IX_CleaningServices_ServiceProviderId",
                table: "CleaningServices");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CleaningServices");

            migrationBuilder.DropColumn(
                name: "ServiceProviderId",
                table: "CleaningServices");

            migrationBuilder.AddColumn<Guid>(
                name: "CleaningServiceProviderId",
                table: "CleaningServices",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CleaningServiceServiceProvider",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CleaningServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CleaningServiceProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningServiceServiceProvider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CleaningServiceServiceProvider_CleaningServiceProviders_Cle~",
                        column: x => x.CleaningServiceProviderId,
                        principalTable: "CleaningServiceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CleaningServiceServiceProvider_CleaningServices_CleaningSer~",
                        column: x => x.CleaningServiceId,
                        principalTable: "CleaningServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CleaningServices_CleaningServiceProviderId",
                table: "CleaningServices",
                column: "CleaningServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningServiceServiceProvider_CleaningServiceId",
                table: "CleaningServiceServiceProvider",
                column: "CleaningServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningServiceServiceProvider_CleaningServiceProviderId",
                table: "CleaningServiceServiceProvider",
                column: "CleaningServiceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningServices_CleaningServiceProviders_CleaningServicePr~",
                table: "CleaningServices",
                column: "CleaningServiceProviderId",
                principalTable: "CleaningServiceProviders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CleaningServices_CleaningServiceProviders_CleaningServicePr~",
                table: "CleaningServices");

            migrationBuilder.DropTable(
                name: "CleaningServiceServiceProvider");

            migrationBuilder.DropIndex(
                name: "IX_CleaningServices_CleaningServiceProviderId",
                table: "CleaningServices");

            migrationBuilder.DropColumn(
                name: "CleaningServiceProviderId",
                table: "CleaningServices");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CleaningServices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderId",
                table: "CleaningServices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceProviderId",
                table: "CleaningServices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CleaningServices_ServiceProviderId",
                table: "CleaningServices",
                column: "ServiceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningServices_CleaningServiceProviders_ServiceProviderId",
                table: "CleaningServices",
                column: "ServiceProviderId",
                principalTable: "CleaningServiceProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
