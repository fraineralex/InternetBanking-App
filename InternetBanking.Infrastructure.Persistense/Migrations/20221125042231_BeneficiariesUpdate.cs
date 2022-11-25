using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBanking.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BeneficiariesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerAccount",
                table: "Beneficiaries",
                newName: "BeneficiaryName");

            migrationBuilder.RenameColumn(
                name: "BeneficiaryCode",
                table: "Beneficiaries",
                newName: "BeneficiaryAccountNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BeneficiaryName",
                table: "Beneficiaries",
                newName: "OwnerAccount");

            migrationBuilder.RenameColumn(
                name: "BeneficiaryAccountNumber",
                table: "Beneficiaries",
                newName: "BeneficiaryCode");
        }
    }
}
