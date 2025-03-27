using EventAPI.Repositories;
using EventAPI.Data;
using Microsoft.EntityFrameworkCore;
using MyEventApi.MappingProfiles;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// 1 Aggiungiamo il servizio CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173",        
        "https://meeting-with-survey.dev.extra.it",
        "https://meeting-with-survey-production.dev.extra.it")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Se necessario per autenticazione
    });
});

// Configura AutoMapper
builder.Services.AddAutoMapper(typeof(CommonProfile));
//builder.Services.AddAutoMapper(typeof(EventAPI).Assembly);

// Configura il database SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseLazyLoadingProxies();
});

// 2 Aggiungiamo i servizi dell'API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IVoteRepository, VoteRepository>();
//builder.Services.AddSingleton<IVoteRepository, InMemoryVoteRepository>();
//builder.Services.AddSingleton<IQuestionRepository, InMemoryQuestionRepository>();
//builder.Services.AddSingleton<IQuestionRepository, JsonQuestionRepository>();

var app = builder.Build();

// 3 Abilitiamo CORS prima di tutto
app.UseCors("MyCorsPolicy");

// 4 Attiviamo Swagger (per testare le API)
app.UseSwagger();
app.UseSwaggerUI();

// 5 HTTPS e routing API
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
