using System.Net;
using FluentValidation;
using FluentValidation.AspNetCore;
using MyApp.Comments.Core.Application;
using MyApp.Comments.Infrastructure;
using MyApp.Comments.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// --- EF Core (SQL Server) ---
//builder.Services.AddDbContext<CommentsDbContext>(opt =>
//{
//    var cs = builder.Configuration.GetConnectionString("DefaultConnection")
//             ?? throw new InvalidOperationException("Missing ConnectionStrings:DefaultConnection");
//    opt.UseSqlServer(cs);
//});

// --- MediatR (scan Core.Application) ---
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));

// --- FluentValidation ---
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);

// --- Web ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommentsPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
// builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Minimal health endpoints
app.MapGet("/healthz", () => Results.Ok(new { status = "OK" }));

app.MapGet("/healthz/db", async (CommentsDbContext db, CancellationToken ct) =>
{
    try
    {
        var canConnect = await db.Database.CanConnectAsync(ct);
        return canConnect
            ? Results.Ok(new { status = "OK", db = "Up" })
            : Results.StatusCode((int)HttpStatusCode.ServiceUnavailable);
    }
    catch (Exception ex)
    {
        return Results.Problem(title: "DB unreachable", detail: ex.Message, statusCode: (int)HttpStatusCode.ServiceUnavailable);
    }
});

// app.MapControllers();

app.Run();
