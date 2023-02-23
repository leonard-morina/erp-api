using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Data.Migrations
{
    public partial class ModifiedTheCountryNamingAndAddedMoreColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "company_website",
                table: "company",
                newName: "website");

            migrationBuilder.RenameColumn(
                name: "company_phone",
                table: "company",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "company_owner_last_name",
                table: "company",
                newName: "owner_last_name");

            migrationBuilder.RenameColumn(
                name: "company_owner_first_name",
                table: "company",
                newName: "owner_first_name");

            migrationBuilder.RenameColumn(
                name: "company_name",
                table: "company",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "company_logo",
                table: "company",
                newName: "logo");

            migrationBuilder.RenameColumn(
                name: "company_email",
                table: "company",
                newName: "initials");

            migrationBuilder.RenameColumn(
                name: "company_address",
                table: "company",
                newName: "email");

            migrationBuilder.AddColumn<string>(
                name: "address_line1",
                table: "company",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_line2",
                table: "company",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "company",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "company",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address_line1",
                table: "company");

            migrationBuilder.DropColumn(
                name: "address_line2",
                table: "company");

            migrationBuilder.DropColumn(
                name: "city",
                table: "company");

            migrationBuilder.DropColumn(
                name: "country",
                table: "company");

            migrationBuilder.RenameColumn(
                name: "website",
                table: "company",
                newName: "company_website");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "company",
                newName: "company_phone");

            migrationBuilder.RenameColumn(
                name: "owner_last_name",
                table: "company",
                newName: "company_owner_last_name");

            migrationBuilder.RenameColumn(
                name: "owner_first_name",
                table: "company",
                newName: "company_owner_first_name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "company",
                newName: "company_name");

            migrationBuilder.RenameColumn(
                name: "logo",
                table: "company",
                newName: "company_logo");

            migrationBuilder.RenameColumn(
                name: "initials",
                table: "company",
                newName: "company_email");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "company",
                newName: "company_address");
        }
    }
}
