using Microsoft.EntityFrameworkCore.Migrations;

namespace EEVA.Domain.Migrations
{
    public partial class update23122019e : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Answers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnswerCorrect",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword",
                table: "Answers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "IsAnswerCorrect",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Keyword",
                table: "Answers");
        }
    }
}
