using Microsoft.EntityFrameworkCore.Migrations;

namespace EEVA.Domain.Migrations
{
    public partial class updatingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Exams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "Contacts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ContactId",
                table: "Exams",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ExamId",
                table: "Contacts",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Exams_ExamId",
                table: "Contacts",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Contacts_ContactId",
                table: "Exams",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Exams_ExamId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Contacts_ContactId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_ContactId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ExamId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "Contacts");
        }
    }
}
