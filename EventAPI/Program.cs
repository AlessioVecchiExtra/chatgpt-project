using EventAPI.Repositories;
using EventAPI.Data;
using Microsoft.EntityFrameworkCore;
using MyEventApi.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// 1 Aggiungiamo il servizio CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173","http://localhost:7771") // Porta del frontend Vue
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Se necessario per autenticazione
    });
});

// Configura AutoMapper
builder.Services.AddAutoMapper(typeof(CommonProfile));
//builder.Services.AddAutoMapper(typeof(EventAPI).Assembly);

// Configura il database SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2 Aggiungiamo i servizi dell'API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IVoteRepository, VoteRepository>();
builder.Services.AddSingleton<IVoteRepository, InMemoryVoteRepository>();
//builder.Services.AddSingleton<IQuestionRepository, InMemoryQuestionRepository>();
builder.Services.AddSingleton<IQuestionRepository, JsonQuestionRepository>();

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
