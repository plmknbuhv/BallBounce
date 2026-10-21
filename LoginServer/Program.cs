var builder = WebApplication.CreateBuilder(args);  // 서버의 설정과 기능을 준비할 객체를 생성

// OpenAPI 문서 생성을 위한 서비스를 DI 컨테이너에 등록합니다.
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, ct) =>
    {
        document.Info.Title = "CasualRPG Game API";
        document.Info.Version = "0.0.1";
        document.Info.Description = "캐주얼 RPG 서버 REST API";
        //비동기 작업을 위한 람다이지만, 우리가 지금한건 동기작업이라 그냥 완료된 빈태스크를 리턴.
        return Task.CompletedTask;
    });
});


var app = builder.Build();  // 준비한 구성을 바탕으로 웹 애플리케이션 객체를 생성

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "CasualRpgServer");
    });

}

app.UseHttpsRedirection(); 

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
