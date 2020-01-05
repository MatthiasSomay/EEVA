using Microsoft.EntityFrameworkCore.Migrations;

namespace EEVA.Domain.Migrations
{
    public partial class updateAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionMultipleChoiceId",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionOpenId",
                table: "Answers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionMultipleChoiceId",
                table: "Answers",
                column: "QuestionMultipleChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionOpenId",
                table: "Answers",
                column: "QuestionOpenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionMultipleChoiceId",
                table: "Answers",
                column: "QuestionMultipleChoiceId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionOpenId",
                table: "Answers",
                column: "QuestionOpenId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionMultipleChoiceId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionOpenId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionMultipleChoiceId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionOpenId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuestionMultipleChoiceId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuestionOpenId",
                table: "Answers");
        }
    }
}
