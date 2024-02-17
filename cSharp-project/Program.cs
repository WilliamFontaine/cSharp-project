using cSharp_project;
using Microsoft.EntityFrameworkCore;
using Serilog;

DotNetEnv.Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database connection
builder.Services.AddDbContext<DbContext, ProgramContext>(options =>
       options.UseSqlite(DotNetEnv.Env.GetString("DATABASE_URL"))
       );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient",
        builder =>
        {
            builder.WithOrigins("https://localhost:7085")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DbContext>();
    context.Database.Migrate();
}

app.UseCors("AllowClient");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
