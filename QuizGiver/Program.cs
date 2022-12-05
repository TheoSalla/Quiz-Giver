using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using QuizGiver;
using QuizGiver.Middlewares;
using QuizGiver.Repository;
using RestClientLib;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
// builder.Services.AddDbContext<QuizContext>(options => options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=QuizAPI;Trusted_Connection=True;"));
builder.Services.AddDbContext<QuizContext>(options => options.UseSqlServer(configuration.GetConnectionString("QuizDB")));
builder.Services.AddControllers();
builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddSingleton<IJsonToModel, JsonToModel>();
builder.Services.AddSingleton<Token>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins("http://localhost:3000").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
        policy.WithOrigins("http://127.0.0.1:3000").AllowCredentials().AllowAnyHeader();
        policy.WithOrigins("http://hello.therelocalfront.com:3000").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddHttpClient();


//builder.Services.AddTransient<Token>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.Use(async (context, next) =>
  {
      var cookieOptions = new CookieOptions()
      {
          Domain = "hello.therelocalfront.com",
          Path = "/",
          SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None,
          IsEssential = true,
          HttpOnly = true,
          Secure = true,
      };
      context.Response.Cookies.Append("MyCookie", "TheValue", cookieOptions);
      

      await next();
  });


// app.UseQuizSessionToken();




// app.UseMiddleware<CookieSet>();

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



app.Use(async (context, next) =>
  {
      var cookieOptions = new CookieOptions()
      {
        //   Domain = "hello.therelocalfront.com",
          Path = "/",
          IsEssential = true,
          HttpOnly = true,

      };
      context.Response.Cookies.Append("MyCookie", "TheValue", cookieOptions);
      

      await next();
  });



// app.UseQuizSessionToken();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();


//app.Run(async context =>
//{
//    context.Response.Cookies.Append("name", "theo");
//});

//app.Use(async (context, next) =>
//{
//    context.Response.Cookies.Append("name", "theo");
//});

//app.Use(async (context, next) =>
//{
//    context.Response.Cookies.Append("name", "theo");
//    context.Response.Cookies.Append("location", "sthlm");
//    if (context.Request.Cookies.TryGetValue("name", out string value))
//    {
//        if(value == "theo")
//        {
//            context.Response.Cookies.Append("name", "fred");
//        }
//    }
//    await next(context);
//});

app.Run();
