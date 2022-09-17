using Microsoft.EntityFrameworkCore;
using QuizGiver;
using RestClientLib;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

// builder.Services.AddDbContext<QuizContext>(options => options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=QuizAPI;Trusted_Connection=True;"));
builder.Services.AddDbContext<QuizContext>(options => options.UseSqlServer(configuration.GetConnectionString("QuizDB")));
builder.Services.AddControllers();
builder.Services.AddSingleton<IJsonToModel, JsonToModel>();
builder.Services.AddSingleton<Token>();
//builder.Services.AddTransient<Token>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
