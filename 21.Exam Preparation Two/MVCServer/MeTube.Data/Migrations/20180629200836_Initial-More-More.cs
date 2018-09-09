using Microsoft.EntityFrameworkCore.Migrations;

namespace MeTube.Data.Migrations
{
    public partial class InitialMoreMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YoutubeId",
                table: "Tubes",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 11);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YoutubeId",
                table: "Tubes",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
