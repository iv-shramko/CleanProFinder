using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanProFinder.Db.Migrations
{
    /// <inheritdoc />
    public partial class RequestServiceFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CleaningServices_Requests_RequestId",
                table: "CleaningServices");

            migrationBuilder.DropIndex(
                name: "IX_CleaningServices_RequestId",
                table: "CleaningServices");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "CleaningServices");

            migrationBuilder.CreateTable(
                name: "CleaningServiceRequest",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningServiceRequest", x => new { x.RequestId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_CleaningServiceRequest_CleaningServices_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "CleaningServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CleaningServiceRequest_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CleaningServiceRequest_ServicesId",
                table: "CleaningServiceRequest",
                column: "ServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CleaningServiceRequest");

            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "CleaningServices",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CleaningServices_RequestId",
                table: "CleaningServices",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningServices_Requests_RequestId",
                table: "CleaningServices",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id");
        }
    }
}
