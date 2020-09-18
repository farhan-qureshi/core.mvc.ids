using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace core.mvc.ids.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "LastLogin", "Password", "Status", "Username" },
                values: new object[] { 1, null, "pragma1", false, "farhan.qureshi" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "LastLogin", "Password", "Status", "Username" },
                values: new object[] { 2, null, "pragma1", false, "fqureshi" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
