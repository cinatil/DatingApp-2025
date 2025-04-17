using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// ✅ Middleware order matters!
app.UseHttpsRedirection();

app.UseCors("CorsPolicy"); // 🟢 Apply here BEFORE controllers are mapped

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // 🟢 This must come AFTER CORS

app.Run();
