using FluentValidation;
using FluentValidation.AspNetCore;
using MyApp.Comments.Core;
using MyApp.Comments.Core.Application;
using MyApp.Comments.Host.Api;              
using MyApp.Comments.Infrastructure;
using MyApp.Comments.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

#region Core & Infra
builder.Services.AddCommentsPersistence(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddCore();
#endregion

#region MediatR + FluentValidation
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);
#endregion

#region Swagger + JWT + Controllers
builder.Services.AddCommentsApi(builder.Configuration);
#endregion

var app = builder.Build();

#region Health endpoints
app.MapGet("/healthz", () => Results.Ok(new { status = "OK" }));
app.MapGet("/healthz/db", async (CommentsDbContext db, CancellationToken ct) =>
{
    var ok = await db.Database.CanConnectAsync(ct);
    return ok ? Results.Ok(new { status = "OK", db = "Up" }) : Results.StatusCode(503);
});
#endregion

#region Swagger/Auth/Controllers pipeline
app.UseCommentsApi(app.Environment);
#endregion

app.Run();
