using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonnelSelfService.Migrations.Migrations
{
    public partial class leaveservice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    BonusAmount = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    NoOfDays = table.Column<int>(nullable: false),
                    LeaveType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ApprovedBy = table.Column<string>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    ResumptionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveInfos");
        }
    }
}
