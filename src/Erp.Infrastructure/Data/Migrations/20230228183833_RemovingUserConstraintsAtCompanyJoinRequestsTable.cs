using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Data.Migrations
{
    public partial class RemovingUserConstraintsAtCompanyJoinRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_company_join_request_user_request_approved_by_user_id",
                table: "company_join_request");

            migrationBuilder.DropForeignKey(
                name: "fk_company_join_request_user_request_cancelled_by_user_id",
                table: "company_join_request");

            migrationBuilder.AlterColumn<string>(
                name: "request_cancelled_by_user_id",
                table: "company_join_request",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "request_approved_by_user_id",
                table: "company_join_request",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_request_user_request_approved_by_user_id",
                table: "company_join_request",
                column: "request_approved_by_user_id",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_request_user_request_cancelled_by_user_id",
                table: "company_join_request",
                column: "request_cancelled_by_user_id",
                principalTable: "user",
                principalColumn: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_company_join_request_user_request_approved_by_user_id",
                table: "company_join_request");

            migrationBuilder.DropForeignKey(
                name: "fk_company_join_request_user_request_cancelled_by_user_id",
                table: "company_join_request");

            migrationBuilder.AlterColumn<string>(
                name: "request_cancelled_by_user_id",
                table: "company_join_request",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "request_approved_by_user_id",
                table: "company_join_request",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_request_user_request_approved_by_user_id",
                table: "company_join_request",
                column: "request_approved_by_user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_request_user_request_cancelled_by_user_id",
                table: "company_join_request",
                column: "request_cancelled_by_user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
