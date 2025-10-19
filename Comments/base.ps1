param(
  [string]$SolutionName = "Comments",
  [string]$RootPath     = (Get-Location).Path,
  [switch]$WhatIf
)

$ErrorActionPreference = "Stop"

function Add-Pkg($proj, $pkg) {
  $cmd = "dotnet add `"$proj`" package $pkg"
  if ($WhatIf) { Write-Host "[DRYRUN] $cmd" -ForegroundColor Yellow; return }
  Write-Host "→ $cmd" -ForegroundColor Cyan
  & dotnet add "$proj" package $pkg | Out-Null
}

# Resolve project paths
$src = Join-Path $RootPath "src"

$proj = @{
  Domain         = Join-Path $src "$SolutionName.Domain\$SolutionName.Domain.csproj"
  Application    = Join-Path $src "$SolutionName.Application\$SolutionName.Application.csproj"
  Infrastructure = Join-Path $src "$SolutionName.Infrastructure\$SolutionName.Infrastructure.csproj"
  SharedKernel   = Join-Path $src "$SolutionName.SharedKernel\$SolutionName.SharedKernel.csproj"
  Contracts      = Join-Path $src "$SolutionName.Contracts\$SolutionName.Contracts.csproj"
  WebApi         = Join-Path $src "$SolutionName.WebApi\$SolutionName.WebApi.csproj"
  Consumers      = Join-Path $src "$SolutionName.Consumers\$SolutionName.Consumers.csproj"
}

# Validate existence (skip silently if a project is missing)
$proj.Keys | ForEach-Object {
  if (-not (Test-Path $proj[$_])) {
    Write-Host "Skipping '$_' (not found at $($proj[$_]))" -ForegroundColor DarkYellow
    $proj.Remove($_)
  }
}

Write-Host "`n=== Adding baseline packages for .NET 9 ===`n" -ForegroundColor Green

# ---------------------------
# Application (CQRS + Validation)
# ---------------------------
if ($proj.Application) {
  Add-Pkg $proj.Application "MediatR"
  Add-Pkg $proj.Application "FluentValidation"
  Add-Pkg $proj.Application "Scrutor"                     # simple assembly scanning (decorators/behaviors)
}

# ---------------------------
# Infrastructure (DB, cache, messaging, files, security, resiliency)
# ---------------------------
if ($proj.Infrastructure) {
  # EF Core (SQL Server)
  Add-Pkg $proj.Infrastructure "Microsoft.EntityFrameworkCore"
  Add-Pkg $proj.Infrastructure "Microsoft.EntityFrameworkCore.SqlServer"
  Add-Pkg $proj.Infrastructure "Microsoft.EntityFrameworkCore.Design"  # for migrations
  Add-Pkg $proj.Infrastructure "Microsoft.EntityFrameworkCore.Relational"

  # Dapper for read-model/projections
  Add-Pkg $proj.Infrastructure "Dapper"

  # Redis cache / rate limiting stores
  Add-Pkg $proj.Infrastructure "StackExchange.Redis"

  # Kafka (broker)
  Add-Pkg $proj.Infrastructure "Confluent.Kafka"

  # Azure Blob Storage (attachments)
  Add-Pkg $proj.Infrastructure "Azure.Storage.Blobs"

  # Image resize (320x240) & HTML sanitizer
  Add-Pkg $proj.Infrastructure "SixLabors.ImageSharp"
  Add-Pkg $proj.Infrastructure "Ganss.XSS"

  # Resilience
  Add-Pkg $proj.Infrastructure "Polly"
  Add-Pkg $proj.Infrastructure "Polly.Extensions.Http"

  # Logging
  Add-Pkg $proj.Infrastructure "Serilog"
  Add-Pkg $proj.Infrastructure "Serilog.Sinks.Console"
}

# ---------------------------
# WebApi (hosting, DI, Swagger, AuthN/Z, GraphQL, logging)
# ---------------------------
if ($proj.WebApi) {
  # MediatR DI + FluentValidation integration for ASP.NET Core
  Add-Pkg $proj.WebApi "MediatR.Extensions.Microsoft.DependencyInjection"
  Add-Pkg $proj.WebApi "FluentValidation.AspNetCore"

  # Swagger/OpenAPI
  Add-Pkg $proj.WebApi "Swashbuckle.AspNetCore"

  # JWT bearer auth (baseline)
  Add-Pkg $proj.WebApi "Microsoft.AspNetCore.Authentication.JwtBearer"

  # GraphQL (Hot Chocolate)
  Add-Pkg $proj.WebApi "HotChocolate.AspNetCore"
  Add-Pkg $proj.WebApi "HotChocolate.Data"
  Add-Pkg $proj.WebApi "HotChocolate.AspNetCore.Authorization"

  # Rate limiting primitives (optional but useful)
  Add-Pkg $proj.WebApi "Microsoft.AspNetCore.RateLimiting"

  # Serilog ASP.NET integration
  Add-Pkg $proj.WebApi "Serilog.AspNetCore"
  Add-Pkg $proj.WebApi "Serilog.Sinks.Console"
  Add-Pkg $proj.WebApi "Serilog.Sinks.Seq"
}

# ---------------------------
# Contracts (usually no packages; keep payloads simple)
# ---------------------------
if ($proj.Contracts) {
  # Intentionally left minimal to avoid coupling. Add only if you know you need something.
  # Example (uncomment if you want consistent JSON casing/attrs helpers):
  # Add-Pkg $proj.Contracts "System.Text.Json"
}

# ---------------------------
# SharedKernel (usually package-free)
# ---------------------------
if ($proj.SharedKernel) {
  # Intentionally keep clean; primitives rarely need external packages.
  # Example (uncomment if you plan to use GuardClauses):
  # Add-Pkg $proj.SharedKernel "Ardalis.GuardClauses"
}

# ---------------------------
# Consumers (Kafka worker)
# ---------------------------
if ($proj.Consumers) {
  Add-Pkg $proj.Consumers "Confluent.Kafka"
  Add-Pkg $proj.Consumers "MediatR.Extensions.Microsoft.DependencyInjection"
  Add-Pkg $proj.Consumers "Serilog.AspNetCore"
  Add-Pkg $proj.Consumers "Serilog.Sinks.Console"
}

Write-Host "`n✅ Baseline packages added. Run: dotnet restore && dotnet build" -ForegroundColor Green
