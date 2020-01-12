using Microsoft.EntityFrameworkCore.Migrations;

namespace EEVA.Domain.Migrations
{
    public partial class question : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentExamAnswerOpen_QuestionId",
                table: "StudentExamAnswers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamAnswers_StudentExamAnswerOpen_QuestionId",
                table: "StudentExamAnswers",
                column: "StudentExamAnswerOpen_QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExamAnswers_Questions_StudentExamAnswerOpen_QuestionId",
                table: "StudentExamAnswers",
                column: "StudentExamAnswerOpen_QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentExamAnswers_Questions_StudentExamAnswerOpen_QuestionId",
                table: "StudentExamAnswers");

            migrationBuilder.DropIndex(
                name: "IX_StudentExamAnswers_StudentExamAnswerOpen_QuestionId",
                table: "StudentExamAnswers");

            migrationBuilder.DropColumn(
                name: "StudentExamAnswerOpen_QuestionId",
                table: "StudentExamAnswers");
        }
    }
}
