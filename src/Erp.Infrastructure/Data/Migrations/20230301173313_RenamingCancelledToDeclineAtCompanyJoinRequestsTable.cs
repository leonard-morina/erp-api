using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Data.Migrations
{
    public partial class RenamingCancelledToDeclineAtCompanyJoinRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_company_join_request_user_request_cancelled_by_user_id",
                table: "company_join_request");

            migrationBuilder.RenameColumn(
                name: "request_cancelled_date_time",
                table: "company_join_request",
                newName: "request_declined_date_time");

            migrationBuilder.RenameColumn(
                name: "request_cancelled_by_user_id",
                table: "company_join_request",
                newName: "request_declined_by_user_id");

            migrationBuilder.RenameColumn(
                name: "request_cancelled",
                table: "company_join_request",
                newName: "request_declined");

            migrationBuilder.RenameIndex(
                name: "ix_company_join_request_request_cancelled_by_user_id",
                table: "company_join_request",
                newName: "ix_company_join_request_request_declined_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_company_join_request_company_id",
                table: "company_join_request",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_company_join_request_user_id",
                table: "company_join_request",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_request_company_company_id",
                table: "company_join_request",
                column: "company_id",
                principalTable: "company",
                principalColumn: "company_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_request_user_request_declined_by_user_id",
                table: "company_join_request",
                column: "request_declined_by_user_id",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_request_user_user_id",
                table: "company_join_request",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_company_join_request_company_company_id",
                table: "company_join_request");

            migrationBuilder.DropForeignKey(
                name: "fk_company_join_request_user_request_declined_by_user_id",
                table: "company_join_request");

            migrationBuilder.DropForeignKey(
                name: "fk_company_join_request_user_user_id",
                table: "company_join_request");

            migrationBuilder.DropIndex(
                name: "ix_company_join_request_company_id",
                table: "company_join_request");

            migrationBuilder.DropIndex(
                name: "ix_company_join_request_user_id",
                table: "company_join_request");

            migrationBuilder.RenameColumn(
                name: "request_declined_date_time",
                table: "company_join_request",
                newName: "request_cancelled_date_time");

            migrationBuilder.RenameColumn(
                name: "request_declined_by_user_id",
                table: "company_join_request",
                newName: "request_cancelled_by_user_id");

            migrationBuilder.RenameColumn(
                name: "request_declined",
                table: "company_join_request",
                newName: "request_cancelled");

            migrationBuilder.RenameIndex(
                name: "ix_company_join_request_request_declined_by_user_id",
                table: "company_join_request",
                newName: "ix_company_join_request_request_cancelled_by_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_company_join_request_user_request_cancelled_by_user_id",
                table: "company_join_request",
                column: "request_cancelled_by_user_id",
                principalTable: "user",
                principalColumn: "user_id");
        }
    }
}
