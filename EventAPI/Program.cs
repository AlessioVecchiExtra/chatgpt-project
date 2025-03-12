using EventAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Aggiungiamo il servizio CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Porta del frontend Vue
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Se necessario per autenticazione
    });
});

// 2️⃣ Aggiungiamo i servizi dell'API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IVoteRepository, InMemoryVoteRepository>();
//builder.Services.AddSingleton<IQuestionRepository, InMemoryQuestionRepository>();
builder.Services.AddSingleton<IQuestionRepository, JsonQuestionRepository>();

var app = builder.Build();

// 3️⃣ Abilitiamo CORS prima di tutto
app.UseCors("MyCorsPolicy");

// 4️⃣ Attiviamo Swagger (per testare le API)
app.UseSwagger();
app.UseSwaggerUI();

// 5️⃣ HTTPS e routing API
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
