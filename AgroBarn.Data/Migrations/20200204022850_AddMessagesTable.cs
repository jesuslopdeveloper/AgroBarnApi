using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroBarn.Data.Migrations
{
    public partial class AddMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Module = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserCreate = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserModify = table.Column<int>(nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
