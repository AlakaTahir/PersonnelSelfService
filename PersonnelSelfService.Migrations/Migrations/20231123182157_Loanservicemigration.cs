using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonnelSelfService.Migrations.Migrations
{
    public partial class Loanservicemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    AmountRequested = table.Column<decimal>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    NoOfInstallment = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    InterestRate = table.Column<double>(nullable: false),
                    AmountApproved = table.Column<decimal>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    InstallmentAmount = table.Column<decimal>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanInfos");
        }
    }
}
