using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuizGiver
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {}

        public DbSet<QuestionInfo> Questions { get; set; } = null!;

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=QuizAPI;Trusted_Connection=True;");
        //     base.OnConfiguring(optionsBuilder);
        // }
        
    }
}