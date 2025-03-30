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
builder.Services.AddScoped<YandexS3Service>();

// Настройка Swagger для API документации
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавление контроллеров для работы с API
builder.Services.AddControllers();

// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:88081", "http://localhost:88081") // указывайте адреса вашего фронтенда
              .AllowAnyMethod()   // Разрешаем все HTTP методы
              .AllowAnyHeader();  // Разрешаем все заголовки
    });
});

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

// Применение CORS политики
app.UseCors("AllowAllOrigins");

// Настройка сервера для прослушивания всех интерфейсов (0.0.0.0)
app.Run("http://0.0.0.0:5252");  // Сервер будет слушать на всех IP-адресах на порту 5252