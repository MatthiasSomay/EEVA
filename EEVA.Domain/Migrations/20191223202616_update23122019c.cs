using Microsoft.EntityFrameworkCore.Migrations;

namespace EEVA.Domain.Migrations
{
    public partial class update23122019c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnswerCorrect",
                table: "Answers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
