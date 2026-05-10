using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Repositories;
using Siemens.Internship2026.GradeBook.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// OLD: Register GradeRepository as the implementation of IGradeRepository
//builder.Services.AddScoped<IGradeRepository, GradeRepository>();

builder.Services.AddHttpClient<IGradeRepository, GradeRepository>();

// Register GradeService as the implementation of IGradeService
builder.Services.AddScoped<IGradeService, GradeService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
