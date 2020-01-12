using Microsoft.EntityFrameworkCore.Migrations;

namespace EEVA.Domain.Migrations
{
    public partial class updatingTablesV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ExamStudent",
                columns: table => new
                {
                    ExamId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamStudent", x => new { x.StudentId, x.ExamId });
                    table.ForeignKey(
                        name: "FK_ExamStudent_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamStudent_Contacts_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamStudent_ExamId",
                table: "ExamStudent",
                column: "ExamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamStudent");

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "Contacts",
                type: "int",
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
    }
}
