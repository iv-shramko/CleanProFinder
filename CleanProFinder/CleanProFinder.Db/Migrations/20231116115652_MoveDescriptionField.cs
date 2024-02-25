using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanProFinder.Db.Migrations
{
    /// <inheritdoc />
    public partial class MoveDescriptionField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CleaningServices");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CleaningServiceServiceProviders",
                type: "text",
                nullable: false,
                defaultValue: "");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CleaningServiceServiceProviders_CleaningServiceProviders_Cl~",
                table: "CleaningServiceServiceProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_CleaningServiceServiceProviders_CleaningServices_CleaningSe~",
                table: "CleaningServiceServiceProviders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CleaningServiceServiceProviders",
                table: "CleaningServiceServiceProviders");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CleaningServiceServiceProviders");

            migrationBuilder.RenameTable(
                name: "CleaningServiceServiceProviders",
                newName: "CleaningServiceServiceProvider");

            migrationBuilder.RenameIndex(
                name: "IX_CleaningServiceServiceProviders_CleaningServiceProviderId",
                table: "CleaningServiceServiceProvider",
                newName: "IX_CleaningServiceServiceProvider_CleaningServiceProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_CleaningServiceServiceProviders_CleaningServiceId",
                table: "CleaningServiceServiceProvider",
                newName: "IX_CleaningServiceServiceProvider_CleaningServiceId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CleaningServices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CleaningServiceServiceProvider",
                table: "CleaningServiceServiceProvider",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningServiceServiceProvider_CleaningServiceProviders_Cle~",
                table: "CleaningServiceServiceProvider",
                column: "CleaningServiceProviderId",
                principalTable: "CleaningServiceProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningServiceServiceProvider_CleaningServices_CleaningSer~",
                table: "CleaningServiceServiceProvider",
                column: "CleaningServiceId",
                principalTable: "CleaningServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
