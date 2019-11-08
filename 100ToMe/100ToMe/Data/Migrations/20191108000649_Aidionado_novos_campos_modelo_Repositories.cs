using Microsoft.EntityFrameworkCore.Migrations;

namespace _100ToMe.Data.Migrations
{
    public partial class Aidionado_novos_campos_modelo_Repositories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Repositories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Repositories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Repositories");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Repositories");
        }
    }
}
