using Microsoft.EntityFrameworkCore;
using QuizGiver.Data;

namespace QuizGiver
{
    public class QuestionsDbContext : DbContext
    {
        public QuestionsDbContext(DbContextOptions<QuestionsDbContext> options) : base(options)
        {}

        public DbSet<QuestionInfo> Questions { get; set; } = null!;
        public DbSet<IncorrectAnswer> IncorrectAnswers { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<QuestionInfo>().ToTable("Questions");
            modelBuilder.Entity<IncorrectAnswer>().ToTable("IncorrectAnswers");


            string q = System.IO.File.ReadAllText("questions.json");
            List<QuestionInfo> listOfQuestions =  System.Text.Json.JsonSerializer.Deserialize<List<QuestionInfo>>(q);
            foreach(QuestionInfo question in listOfQuestions)
            {
                modelBuilder.Entity<QuestionInfo>().HasData(question);
            }

            string ia = System.IO.File.ReadAllText("incorrectAnswers.json");
            List<IncorrectAnswer> listOfIncorrectAnswers = System.Text.Json.JsonSerializer.Deserialize<List<IncorrectAnswer>>(ia);
            foreach(IncorrectAnswer answer in listOfIncorrectAnswers)
            {
                modelBuilder.Entity<IncorrectAnswer>().HasData(answer);
            }

            //Seed to Questions
            // modelBuilder.Entity<QuestionInfo>().HasData(new QuestionInfo() {
            //     Id = 1,
            //     Category = "general",
            //     Type = "boolean",
            //     Difficulty = "easy",
            //     Question = "What is the capital of Sweden",
            //     CorrectAnswer = "Stockholm",
            //     IncorrectAnswers = new List<string>()
            //     {
            //         "Paris",
            //         "Oslo",
            //         "London"
            //     }
            // });
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=QuizAPI;Trusted_Connection=True;");
        //     base.OnConfiguring(optionsBuilder);
        // }
    }
}
