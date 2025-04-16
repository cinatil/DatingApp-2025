using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ✅ Define CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ✅ Middleware order matters!
app.UseHttpsRedirection();

app.UseCors("CorsPolicy"); // 🟢 Apply here BEFORE controllers are mapped

app.MapControllers(); // 🟢 This must come AFTER CORS

app.Run();
