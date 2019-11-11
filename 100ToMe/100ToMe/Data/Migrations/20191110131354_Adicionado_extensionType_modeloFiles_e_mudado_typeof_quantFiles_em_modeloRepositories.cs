using Microsoft.EntityFrameworkCore.Migrations;

namespace _100ToMe.Data.Migrations
{
    public partial class Adicionado_extensionType_modeloFiles_e_mudado_typeof_quantFiles_em_modeloRepositories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "QuantFiles",
                table: "Repositories",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtensionType",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtensionType",
                table: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "QuantFiles",
                table: "Repositories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
