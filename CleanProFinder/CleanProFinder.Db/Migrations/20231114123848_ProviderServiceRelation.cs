using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanProFinder.Db.Migrations
{
    /// <inheritdoc />
    public partial class ProviderServiceRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CleaningServiceServiceProvider_CleaningServiceProviders_Cle~",
                table: "CleaningServiceServiceProvider");

            migrationBuilder.DropForeignKey(
                name: "FK_CleaningServiceServiceProvider_CleaningServices_CleaningSer~",
                table: "CleaningServiceServiceProvider");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CleaningServiceServiceProvider",
                table: "CleaningServiceServiceProvider");

            migrationBuilder.RenameTable(
                name: "CleaningServiceServiceProvider",
                newName: "CleaningServiceServiceProviders");

            migrationBuilder.RenameIndex(
                name: "IX_CleaningServiceServiceProvider_CleaningServiceProviderId",
                table: "CleaningServiceServiceProviders",
                newName: "IX_CleaningServiceServiceProviders_CleaningServiceProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_CleaningServiceServiceProvider_CleaningServiceId",
                table: "CleaningServiceServiceProviders",
                newName: "IX_CleaningServiceServiceProviders_CleaningServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CleaningServiceServiceProviders",
                table: "CleaningServiceServiceProviders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningServiceServiceProviders_CleaningServiceProviders_Cl~",
                table: "CleaningServiceServiceProviders",
                column: "CleaningServiceProviderId",
                principalTable: "CleaningServiceProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningServiceServiceProviders_CleaningServices_CleaningSe~",
                table: "CleaningServiceServiceProviders",
                column: "CleaningServiceId",
                principalTable: "CleaningServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
