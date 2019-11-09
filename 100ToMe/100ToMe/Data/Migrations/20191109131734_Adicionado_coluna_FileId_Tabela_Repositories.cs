using Microsoft.EntityFrameworkCore.Migrations;

namespace _100ToMe.Data.Migrations
{
    public partial class Adicionado_coluna_FileId_Tabela_Repositories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileId",
                table: "Repositories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Repositories");
        }
    }
}
