using Microsoft.EntityFrameworkCore.Migrations;

namespace MeTube.Data.Migrations
{
    public partial class InitialMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tubes",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tubes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
