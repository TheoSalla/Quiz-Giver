using System.Text.Json;
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
            modelBuilder.Entity<IncorrectAnswer>().ToTable("IncorrectAnswers").HasOne(i => i.QuestionInfo).WithMany(c => c.IncorrectAnswers).HasForeignKey(k => k.QuestionId);

            string q = File.ReadAllText("questions.json");
            List<QuestionInfo> listOfQuestions =  JsonSerializer.Deserialize<List<QuestionInfo>>(q);
            foreach(QuestionInfo question in listOfQuestions)
            {
                modelBuilder.Entity<QuestionInfo>().HasData(question);
            }

            string ia = File.ReadAllText("incorrectAnswers.json");
            List<IncorrectAnswer> listOfIncorrectAnswers = JsonSerializer.Deserialize<List<IncorrectAnswer>>(ia);
            foreach(IncorrectAnswer answer in listOfIncorrectAnswers)
            {
                modelBuilder.Entity<IncorrectAnswer>().HasData(answer);
            }
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=QuizAPI;Trusted_Connection=True;");
        //     base.OnConfiguring(optionsBuilder);
        // }
    }
}
