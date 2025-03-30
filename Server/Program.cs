using Microsoft.EntityFrameworkCore;
using Server.DataBase;
using Server.Repositories;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Получение строки подключения
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Настройка DbContext с использованием PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Добавление сервисов для регистрации пользователей
builder.Services.AddScoped<UserRepository>(); 
builder.Services.AddScoped<SignUpService>();  
builder.Services.AddScoped<SignInService>();
builder.Services.AddScoped<HashPasswordService>();

// Настройка Swagger для API документации
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавление контроллеров для работы с API
builder.Services.AddControllers();

var app = builder.Build();

// Настройка Swagger в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Включение маршрутизации для контроллеров
app.MapControllers();

app.Run();