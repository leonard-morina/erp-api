using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Data.Migrations
{
    public partial class AddedCompanyJoinRequestTableAndAddedSomeMoreColumnsToCompanyTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "inserted_by_user_id",
                table: "user_company",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "inserted_date_time",
                table: "user_company",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_owner",
                table: "user_company",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "modified_by_user_id",
                table: "user_company",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date_time",
                table: "user_company",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "company_address",
                table: "company",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "company_owner_first_name",
                table: "company",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "company_owner_last_name",
                table: "company",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "company_join_request",
                columns: table => new
                {
                    company_join_request_id = table.Column<string>(type: "text", nullable: false),
                    company_id = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    request_approved = table.Column<bool>(type: "boolean", nullable: false),
                    request_cancelled = table.Column<bool>(type: "boolean", nullable: false),
                    request_approved_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    request_cancelled_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    request_approved_by_user_id = table.Column<string>(type: "text", nullable: false),
                    request_cancelled_by_user_id = table.Column<string>(type: "text", nullable: false),
                    request_initiated_by_company = table.Column<bool>(type: "boolean", nullable: false),
                    inserted_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inserted_by_user_id = table.Column<string>(type: "text", nullable: false),
                    modified_by_user_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_company_join_request", x => x.company_join_request_id);
                    table.ForeignKey(
                        name: "fk_company_join_request_user_inserted_by_user_id",
                        column: x => x.inserted_by_user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_company_join_request_user_modified_by_user_id",
                        column: x => x.modified_by_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_company_join_request_user_request_approved_by_user_id",
                        column: x => x.request_approved_by_user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_company_join_request_user_request_cancelled_by_user_id",
                        column: x => x.request_cancelled_by_user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_company_inserted_by_user_id",
                table: "user_company",
                column: "inserted_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_company_modified_by_user_id",
                table: "user_company",
                column: "modified_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_company_join_request_inserted_by_user_id",
                table: "company_join_request",
                column: "inserted_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_company_join_request_modified_by_user_id",
                table: "company_join_request",
                column: "modified_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_company_join_request_request_approved_by_user_id",
                table: "company_join_request",
                column: "request_approved_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_company_join_request_request_cancelled_by_user_id",
                table: "company_join_request",
                column: "request_cancelled_by_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_company_user_inserted_by_user_id",
                table: "user_company",
                column: "inserted_by_user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_company_user_modified_by_user_id",
                table: "user_company",
                column: "modified_by_user_id",
                principalTable: "user",
                principalColumn: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_company_user_inserted_by_user_id",
                table: "user_company");

            migrationBuilder.DropForeignKey(
                name: "fk_user_company_user_modified_by_user_id",
                table: "user_company");

            migrationBuilder.DropTable(
                name: "company_join_request");

            migrationBuilder.DropIndex(
                name: "ix_user_company_inserted_by_user_id",
                table: "user_company");

            migrationBuilder.DropIndex(
                name: "ix_user_company_modified_by_user_id",
                table: "user_company");

            migrationBuilder.DropColumn(
                name: "inserted_by_user_id",
                table: "user_company");

            migrationBuilder.DropColumn(
                name: "inserted_date_time",
                table: "user_company");

            migrationBuilder.DropColumn(
                name: "is_owner",
                table: "user_company");

            migrationBuilder.DropColumn(
                name: "modified_by_user_id",
                table: "user_company");

            migrationBuilder.DropColumn(
                name: "modified_date_time",
                table: "user_company");

            migrationBuilder.DropColumn(
                name: "company_address",
                table: "company");

            migrationBuilder.DropColumn(
                name: "company_owner_first_name",
                table: "company");

            migrationBuilder.DropColumn(
                name: "company_owner_last_name",
                table: "company");
        }
    }
}
