using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanProFinder.Db.Migrations
{
    /// <inheritdoc />
    public partial class CleaningServiceModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CleaningServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CleaningServices_CleaningServiceProviders_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "CleaningServiceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CleaningServices_ServiceProviderId",
                table: "CleaningServices",
                column: "ServiceProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CleaningServices");
        }
    }
}
