using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraastructure.Migrations
{
    /// <inheritdoc />
    public partial class officesec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Appoinment",
                newName: "ProviderId");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Appoinment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appoinment");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Appoinment",
                newName: "UserId");
        }
    }
}
