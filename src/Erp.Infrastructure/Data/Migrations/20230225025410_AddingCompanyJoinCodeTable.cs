using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Data.Migrations
{
    public partial class AddingCompanyJoinCodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "company_join_code",
                columns: table => new
                {
                    company_join_code_id = table.Column<string>(type: "text", nullable: false),
                    company_id = table.Column<string>(type: "text", nullable: false),
                    join_code = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    inserted_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inserted_by_user_id = table.Column<string>(type: "text", nullable: false),
                    modified_by_user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_company_join_code", x => x.company_join_code_id);
                    table.ForeignKey(
                        name: "fk_company_join_code_company_company_id",
                        column: x => x.company_id,
                        principalTable: "company",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_company_join_code_user_inserted_by_user_id",
                        column: x => x.inserted_by_user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_company_join_code_user_modified_by_user_id",
                        column: x => x.modified_by_user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_company_join_code_company_id",
                table: "company_join_code",
                column: "company_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_company_join_code_inserted_by_user_id",
                table: "company_join_code",
                column: "inserted_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_company_join_code_modified_by_user_id",
                table: "company_join_code",
                column: "modified_by_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company_join_code");
        }
    }
}
