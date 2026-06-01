using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerMonitor.Migrations
{
    /// <inheritdoc />
    public partial class AddAlertFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Alerts",
                type: "text",
                nullable: false,
                defaultValue: "General");

            migrationBuilder.AddColumn<double>(
                name: "Valor",
                table: "Alerts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Alerts");
        }
    }
}