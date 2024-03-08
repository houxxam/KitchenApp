using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repas.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RepasServices_DateFornitureId",
                table: "RepasServices",
                column: "DateFornitureId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepasServices_DateFornitures_DateFornitureId",
                table: "RepasServices",
                column: "DateFornitureId",
                principalTable: "DateFornitures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepasServices_DateFornitures_DateFornitureId",
                table: "RepasServices");

            migrationBuilder.DropIndex(
                name: "IX_RepasServices_DateFornitureId",
                table: "RepasServices");
        }
    }
}
