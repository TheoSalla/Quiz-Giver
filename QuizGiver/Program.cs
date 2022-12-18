using Microsoft.EntityFrameworkCore;
using QuizGiver;
using QuizGiver.Middlewares;
using QuizGiver.Repository;
using RestClientLib;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
// builder.Services.AddDbContext<QuizContext>(options => options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=QuizAPI;Trusted_Connection=True;"));
builder.Services.AddDbContext<QuestionsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("QuizDB2")));
builder.Services.AddControllers();
builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddSingleton<IJsonToModel, JsonToModel>();
// builder.Services.AddSingleton<Token>();
builder.Services.AddTransient<Token>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins("http://localhost:3000").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
        policy.WithOrigins("http://127.0.0.1:3000").AllowCredentials().AllowAnyHeader();
    });
});
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}
app.UseQuizSessionToken();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
