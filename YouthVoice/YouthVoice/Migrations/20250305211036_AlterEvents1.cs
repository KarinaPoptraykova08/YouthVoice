using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouthVoice.Migrations
{
    /// <inheritdoc />
    public partial class AlterEvents1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "RegistrationDeadline",
                table: "Events",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "RegistrationLink",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RegistrationDeadline",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RegistrationLink",
                table: "Events");
        }
    }
}
