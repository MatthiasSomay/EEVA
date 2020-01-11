using Microsoft.EntityFrameworkCore.Migrations;

namespace EEVA.Domain.Migrations
{
    public partial class studentexamAdjustment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "StudentExamAnswers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "StudentExamAnswers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "StudentExamAnswers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "StudentExamAnswers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamAnswers_QuestionId",
                table: "StudentExamAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamAnswers_AnswerId",
                table: "StudentExamAnswers",
                column: "AnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExamAnswers_Questions_QuestionId",
                table: "StudentExamAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_StudentExamAnswers_Questions_QuestionId",
                table: "StudentExamAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExamAnswers_AnswersMultipleChoice_AnswerId",
                table: "StudentExamAnswers");

            migrationBuilder.DropIndex(
                name: "IX_StudentExamAnswers_QuestionId",
                table: "StudentExamAnswers");

            migrationBuilder.DropIndex(
                name: "IX_StudentExamAnswers_AnswerId",
                table: "StudentExamAnswers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "StudentExamAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "StudentExamAnswers");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "StudentExamAnswers");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "StudentExamAnswers");
        }
    }
}
