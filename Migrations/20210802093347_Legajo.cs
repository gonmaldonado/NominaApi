using Microsoft.EntityFrameworkCore.Migrations;

namespace NominaApi.Migrations
{
    public partial class Legajo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Legajo",
                table: "Empleados",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Legajo",
                table: "Empleados");
        }
    }
}
