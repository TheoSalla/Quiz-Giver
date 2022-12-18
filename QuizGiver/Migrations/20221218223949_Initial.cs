using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizGiver.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncorrectAnswers",
                columns: table => new
                {
                    IncorrectAnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncorrectAnswers", x => x.IncorrectAnswerId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                });

            migrationBuilder.InsertData(
                table: "IncorrectAnswers",
                columns: new[] { "IncorrectAnswerId", "Answer", "QuestionId" },
                values: new object[,]
                {
                    { new Guid("60de8c27-dea1-4771-a6f5-52e56c93e522"), "Charles Babbage", new Guid("8fe0f29d-b584-4377-8789-9c6949c2db3a") },
                    { new Guid("6e6ec212-fc01-4012-a481-35b2cea36548"), "Chaser", new Guid("89ba9b17-c858-4949-870e-537dddb7e2da") },
                    { new Guid("88ad9503-0e69-4da6-9dce-0aebe28f67bc"), "Isaac Newton", new Guid("8fe0f29d-b584-4377-8789-9c6949c2db3a") },
                    { new Guid("998d95b5-d7e7-4958-ad22-a6dbfaf623fe"), "Beater", new Guid("89ba9b17-c858-4949-870e-537dddb7e2da") },
                    { new Guid("be745508-bd8e-46ea-baff-f47f2777b106"), "J.J Thomson", new Guid("8fe0f29d-b584-4377-8789-9c6949c2db3a") },
                    { new Guid("d51b834b-99c0-4c4f-83ab-0185e65d1739"), "Keeper", new Guid("89ba9b17-c858-4949-870e-537dddb7e2da") }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "Category", "CorrectAnswer", "Difficulty", "Question", "Type" },
                values: new object[,]
                {
                    { new Guid("89ba9b17-c858-4949-870e-537dddb7e2da"), "Entertainment: Books", "Seeker", "medium", "What position does Harry Potter play in Quidditch?", "multiple" },
                    { new Guid("8fe0f29d-b584-4377-8789-9c6949c2db3a"), "History", "Jethro Tull", "medium", "The seed drill was invented by which British inventor?", "multiple" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncorrectAnswers");

            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
