using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appoinment",
                columns: table => new
                {
                    AppoinemntId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpecialisationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AppinementTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Chiefcomplaint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoinment", x => x.AppoinemntId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appoinment");
        }
    }
}
