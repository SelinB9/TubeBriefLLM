using TubeBriefLLM.API.Summaries.Services;
using DotNetEnv;

// Uygulama başlamadan hemen önce şunu çağır:
Env.Load();

var builder = WebApplication.CreateBuilder(args);

//APIKEY için
builder.Configuration.AddEnvironmentVariables();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services
builder.Services.AddScoped<ISummaryService, SummaryService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Uygulama çalışınca direkt anasayfada açılmasını sağlar
    });
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();
app.Run();
