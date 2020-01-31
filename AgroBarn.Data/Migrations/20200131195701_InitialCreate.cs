using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroBarn.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Breeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserCreate = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserModify = table.Column<int>(nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conceptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserCreate = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserModify = table.Column<int>(nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conceptions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Breeds");

            migrationBuilder.DropTable(
                name: "Conceptions");
        }
    }
}
