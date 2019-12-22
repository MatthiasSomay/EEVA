using Microsoft.EntityFrameworkCore.Migrations;

namespace EEVA.Domain.Migrations
{
    public partial class UpdateAllEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Teachers_TeacherId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExam_Exams_ExamId",
                table: "StudentExam");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExam_Student_StudentId",
                table: "StudentExam");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExamAnswer_StudentExam_StudentExamId",
                table: "StudentExamAnswer");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentExamAnswer",
                table: "StudentExamAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentExam",
                table: "StudentExam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "StudentExamAnswer",
                newName: "StudentExamAnswers");

            migrationBuilder.RenameTable(
                name: "StudentExam",
                newName: "StudentExams");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "Contacts");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExamAnswer_StudentExamId",
                table: "StudentExamAnswers",
                newName: "IX_StudentExamAnswers_StudentExamId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExam_StudentId",
                table: "StudentExams",
                newName: "IX_StudentExams_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExam_ExamId",
                table: "StudentExams",
                newName: "IX_StudentExams_ExamId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Contacts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentExamAnswers",
                table: "StudentExamAnswers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentExams",
                table: "StudentExams",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Contacts_TeacherId",
                table: "Exams",
                column: "TeacherId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExamAnswers_StudentExams_StudentExamId",
                table: "StudentExamAnswers",
                column: "StudentExamId",
                principalTable: "StudentExams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExams_Exams_ExamId",
                table: "StudentExams",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExams_Contacts_StudentId",
                table: "StudentExams",
                column: "StudentId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Contacts_TeacherId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExamAnswers_StudentExams_StudentExamId",
                table: "StudentExamAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExams_Exams_ExamId",
                table: "StudentExams");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExams_Contacts_StudentId",
                table: "StudentExams");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentExams",
                table: "StudentExams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentExamAnswers",
                table: "StudentExamAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Contacts");

            migrationBuilder.RenameTable(
                name: "StudentExams",
                newName: "StudentExam");

            migrationBuilder.RenameTable(
                name: "StudentExamAnswers",
                newName: "StudentExamAnswer");

            migrationBuilder.RenameTable(
                name: "Contacts",
                newName: "Teachers");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExams_StudentId",
                table: "StudentExam",
                newName: "IX_StudentExam_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExams_ExamId",
                table: "StudentExam",
                newName: "IX_StudentExam_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentExamAnswers_StudentExamId",
                table: "StudentExamAnswer",
                newName: "IX_StudentExamAnswer_StudentExamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentExam",
                table: "StudentExam",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentExamAnswer",
                table: "StudentExamAnswer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Teachers_TeacherId",
                table: "Exams",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExam_Exams_ExamId",
                table: "StudentExam",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExam_Student_StudentId",
                table: "StudentExam",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExamAnswer_StudentExam_StudentExamId",
                table: "StudentExamAnswer",
                column: "StudentExamId",
                principalTable: "StudentExam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
