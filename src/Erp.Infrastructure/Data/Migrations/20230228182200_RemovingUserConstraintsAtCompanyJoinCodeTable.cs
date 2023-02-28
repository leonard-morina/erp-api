using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Data.Migrations
{
    public partial class RemovingUserConstraintsAtCompanyJoinCodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_company_join_code_user_inserted_by_user_id",
                table: "company_join_code");

            migrationBuilder.DropForeignKey(
                name: "fk_company_join_code_user_modified_by_user_id",
                table: "company_join_code");

            migrationBuilder.AlterColumn<string>(
                name: "modified_by_user_id",
                table: "company_join_code",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "inserted_by_user_id",
                table: "company_join_code",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_code_user_inserted_by_user_id",
                table: "company_join_code",
                column: "inserted_by_user_id",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_code_user_modified_by_user_id",
                table: "company_join_code",
                column: "modified_by_user_id",
                principalTable: "user",
                principalColumn: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_company_join_code_user_inserted_by_user_id",
                table: "company_join_code");

            migrationBuilder.DropForeignKey(
                name: "fk_company_join_code_user_modified_by_user_id",
                table: "company_join_code");

            migrationBuilder.AlterColumn<string>(
                name: "modified_by_user_id",
                table: "company_join_code",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "inserted_by_user_id",
                table: "company_join_code",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_code_user_inserted_by_user_id",
                table: "company_join_code",
                column: "inserted_by_user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_code_user_modified_by_user_id",
                table: "company_join_code",
                column: "modified_by_user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
