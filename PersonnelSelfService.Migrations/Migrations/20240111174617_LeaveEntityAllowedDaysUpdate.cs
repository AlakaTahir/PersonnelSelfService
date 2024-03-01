using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonnelSelfService.Migrations.Migrations
{
    public partial class LeaveEntityAllowedDaysUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfDays",
                table: "LeaveInfos");

            migrationBuilder.AddColumn<int>(
                name: "AllowedLeaveDays",
                table: "LeaveInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "LeaveInfos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RemainingLeaveDays",
                table: "LeaveInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestedLeaveDays",
                table: "LeaveInfos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedLeaveDays",
                table: "LeaveInfos");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "LeaveInfos");

            migrationBuilder.DropColumn(
                name: "RemainingLeaveDays",
                table: "LeaveInfos");

            migrationBuilder.DropColumn(
                name: "RequestedLeaveDays",
                table: "LeaveInfos");

            migrationBuilder.AddColumn<int>(
                name: "NoOfDays",
                table: "LeaveInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
