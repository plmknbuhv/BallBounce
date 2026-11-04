using GameServer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);  // 서버의 설정과 기능을 준비할 객체를 생성

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, ct) =>
    {
        document.Info.Title = "Bounce Ball Server";
        document.Info.Version = "0.0.1";
        document.Info.Description = "공부용 서버 REST API";
        return Task.CompletedTask;
    });
});

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 46)));
});

var app = builder.Build();  // 준비한 구성을 바탕으로 웹 애플리케이션 객체를 생성

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "BounceBallServer");
    });
}

app.UseHttpsRedirection();

app.MapGet("/", () => "BounceBallServer is running.");

app.Run();