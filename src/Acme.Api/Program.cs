using Acme.Core.Data;
using Acme.Core.Interfaces;
using Acme.Core.Persistence;
using Acme.Core.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddOpenApi();

builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();

builder.Services.AddDbContext<AcmeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.WithOrigins("http://localhost:5056", "https://localhost:5056")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// ✅ Use CORS
app.UseCors("AllowClient");

app.UseAuthorization();

// ✅ Map Controllers - THIS WAS MISSING!
app.MapControllers();

app.Run();