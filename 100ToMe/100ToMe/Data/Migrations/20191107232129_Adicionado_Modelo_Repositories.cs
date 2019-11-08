using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _100ToMe.Data.Migrations
{
    public partial class Adicionado_Modelo_Repositories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    RepoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    QuantFiles = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    DataRegister = table.Column<DateTime>(nullable: false),
                    DataLastChange = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.RepoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repositories");
        }
    }
}
