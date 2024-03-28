using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repas.Migrations
{
    /// <inheritdoc />
    public partial class hosp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHospitalier",
                table: "Services",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHospitalier",
                table: "Services");
        }
    }
}
