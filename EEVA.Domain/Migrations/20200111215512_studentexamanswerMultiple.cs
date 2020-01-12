using Microsoft.EntityFrameworkCore.Migrations;

namespace EEVA.Domain.Migrations
{
    public partial class studentexamanswerMultiple : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswersMultipleChoice_StudentExamAnswers_StudentExamAnswerMultipleChoiceId",
                table: "AnswersMultipleChoice");

            migrationBuilder.DropIndex(
                name: "IX_AnswersMultipleChoice_StudentExamAnswerMultipleChoiceId",
                table: "AnswersMultipleChoice");

            migrationBuilder.DropColumn(
                name: "StudentExamAnswerMultipleChoiceId",
                table: "AnswersMultipleChoice");

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "StudentExamAnswers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamAnswers_AnswerId",
                table: "StudentExamAnswers",
                column: "AnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExamAnswers_AnswersMultipleChoice_AnswerId",
                table: "StudentExamAnswers",
                column: "AnswerId",
                principalTable: "AnswersMultipleChoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentExamAnswers_AnswersMultipleChoice_AnswerId",
                table: "StudentExamAnswers");

            migrationBuilder.DropIndex(
                name: "IX_StudentExamAnswers_AnswerId",
                table: "StudentExamAnswers");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "StudentExamAnswers");

            migrationBuilder.AddColumn<int>(
                name: "StudentExamAnswerMultipleChoiceId",
                table: "AnswersMultipleChoice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswersMultipleChoice_StudentExamAnswerMultipleChoiceId",
                table: "AnswersMultipleChoice",
                column: "StudentExamAnswerMultipleChoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswersMultipleChoice_StudentExamAnswers_StudentExamAnswerMultipleChoiceId",
                table: "AnswersMultipleChoice",
                column: "StudentExamAnswerMultipleChoiceId",
                principalTable: "StudentExamAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
