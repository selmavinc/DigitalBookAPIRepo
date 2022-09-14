using Microsoft.EntityFrameworkCore;
using UserRegistartion.DatabaseEntity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DigitalBooksContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dbconn") ?? throw new InvalidOperationException("Connection string 'Dbconn' not found.")));
// Add services to the container.
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITokenService>(new TokenService());
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
