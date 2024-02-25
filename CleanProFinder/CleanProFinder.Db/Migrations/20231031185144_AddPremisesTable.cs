using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanProFinder.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddPremisesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Premise_ServiceUsers_UserId",
                table: "Premise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Premise",
                table: "Premise");

            migrationBuilder.RenameTable(
                name: "Premise",
                newName: "Premises");

            migrationBuilder.RenameIndex(
                name: "IX_Premise_UserId",
                table: "Premises",
                newName: "IX_Premises_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Premises",
                table: "Premises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Premises_ServiceUsers_UserId",
                table: "Premises",
                column: "UserId",
                principalTable: "ServiceUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Premises_ServiceUsers_UserId",
                table: "Premises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Premises",
                table: "Premises");

            migrationBuilder.RenameTable(
                name: "Premises",
                newName: "Premise");

            migrationBuilder.RenameIndex(
                name: "IX_Premises_UserId",
                table: "Premise",
                newName: "IX_Premise_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Premise",
                table: "Premise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Premise_ServiceUsers_UserId",
                table: "Premise",
                column: "UserId",
                principalTable: "ServiceUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
