using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanProFinder.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddedPremises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Premise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Square = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premise_ServiceUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ServiceUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Premise_UserId",
                table: "Premise",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Premise");
        }
    }
}
