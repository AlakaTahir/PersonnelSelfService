using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonnelSelfService.Migrations.Migrations
{
    public partial class updatingsomerecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "EmployeeInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "EmployeeInfos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
