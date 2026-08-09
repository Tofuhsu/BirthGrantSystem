using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirthGrant.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BirthGrantCases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaseNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApplyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildIdNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ChildName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ChildOrder = table.Column<int>(type: "int", nullable: false),
                    ApplicantIdNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicantRegisterDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ApplicantPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ApplicantAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SpouseIdNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SpouseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SpouseRegisterDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SpousePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SpouseAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AgentIdNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AgentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgentPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PayeeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PayeePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BankAccount = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GrantAmount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ReceiptOfficePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReceiptQrText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirthGrantCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BirthGrantCaseHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthGrantCaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActionDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OperatorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OperatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirthGrantCaseHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BirthGrantCaseHistories_BirthGrantCases_BirthGrantCaseId",
                        column: x => x.BirthGrantCaseId,
                        principalTable: "BirthGrantCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BirthGrantCaseHistories_BirthGrantCaseId",
                table: "BirthGrantCaseHistories",
                column: "BirthGrantCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_BirthGrantCases_CaseNo",
                table: "BirthGrantCases",
                column: "CaseNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BirthGrantCaseHistories");

            migrationBuilder.DropTable(
                name: "BirthGrantCases");
        }
    }
}
