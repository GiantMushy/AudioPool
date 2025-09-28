# AudioPool
Vefþjónustur 2025 HR - Large Assignment 1 

A layered ASP.NET Core Web API for managing artists, albums, songs, and genres. Built with:
- Web API (presentation)
- Services (business logic)
- Repositories (EF Core data access)
- Models (DTOs, Input Models, and Entities)

SQLite is used for persistence. Swagger UI is included.

## Solution Structure

- AudioPool.WebApi
  - Controllers, Program.cs, Swagger, JSON converters
- AudioPool.Services
  - Service interfaces and implementations
- AudioPool.Repositories
  - EF Core DbContext, entities, repositories
- AudioPool.Models
  - DTOs, input models, HATEOAS link helpers

## Prerequisites

- .NET SDK (LTS or current)
- SQLite
- EF Core Tools
  - dotnet tool install --global dotnet-ef

## Configuration

Set the SQLite connection string in AudioPool.WebApi/appsettings.json:

```json
{
  "ConnectionStrings": {
    "AudioDbConnectionString": "Data Source=audio.db"
  }
}
```

## Build, Migrate, Run

From the repo root:

```bash
# Restore and build
dotnet restore
dotnet build

# Apply migrations (Repositories as data project, WebApi as startup)
dotnet ef migrations add Initial # if not already created
dotnet ef database update

# Run the API
dotnet run
#or 
dotnet watch
```

Swagger UI:
- http://localhost:5000/swagger or https://localhost:5001/swagger

## Authentication (API Token)

All requests must include the API Token header:

- Header name: api-token
- Token value: AudioPoolSecretToken2025

Example cURL:

```bash
curl -H "api-token: AudioPoolSecretToken2025" https://localhost:5001/api/artists
```

Swagger UI lets you set this once via the Authorize button.

## API Overview

- GET /api/artists
  - Paging: pageNumber (default 1), pageSize (default 25, max 100)
  - Ordered by DateOfStart descending
  - Returns a paged envelope with items and HATEOAS links (self, edit, delete, albums, genres)
- GET /api/artists/{id}
  - Returns artist details with albums and genre links
- GET /api/albums/{id}, GET /api/albums/{id}/songs
- GET /api/songs/{id}
- POST/PUT/DELETE endpoints for albums, artists, songs, genres (as implemented)

Validation:
- [ApiController] automatically returns 400 for invalid input models
- Not found conditions return 404

## JSON Conventions

TimeSpan serialization
- The API uses a custom TimeSpan converter.
- Send durations as strings in “c” format: "hh:mm:ss[.fffffff]".

Examples:
```json
{
  "name": "Hello World",
  "duration": "00:03:30",
  "albumId": 3
}
```

100 ticks (10,000 ns):
```json
{
  "name": "Short Track",
  "duration": "00:00:00.0000100",
  "albumId": 3
}
```

## EF Notes

- Migrations are created/applied with Repositories as the data project and WebApi as startup:
  - dotnet ef migrations add Initial -p AudioPool.Repositories -s AudioPool.WebApi
  - dotnet ef database update -p AudioPool.Repositories -s AudioPool.WebApi
- The DbContext is configured for SQLite in Program.cs.

## Development Notes

- Paging envelope is returned for list endpoints (PageNumber, PageSize, MaxPages, Items).
- HATEOAS links are added in the service layer.
- “Not found” flows throw KeyNotFoundException in repositories and are mapped to 404 in controllers/middleware.
- Validation attributes on input models enforce client errors (400).

## Running Tests

No unit tests are included yet. You can add test projects under a tests/ folder and reference the class libraries.

---
```// filepath: /Users/totomcfrodo/Documents/GitHub/AudioPool/README.md
# AudioPool

A layered ASP.NET Core Web API for managing artists, albums, songs, and genres. Built with:
- Web API (presentation)
- Services (business logic)
- Repositories (EF Core data access)
- Models (DTOs and input models)

SQLite is used for persistence. Swagger UI is included.

## Solution Structure

- AudioPool.WebApi
  - Controllers, Program.cs, Swagger, JSON converters
- AudioPool.Services
  - Service interfaces and implementations
- AudioPool.Repositories
  - EF Core DbContext, entities, repositories
- AudioPool.Models
  - DTOs, input models, HATEOAS link helpers

## Prerequisites

- .NET SDK (LTS or current)
- SQLite
- EF Core Tools
  - dotnet tool install --global dotnet-ef

## Configuration

Set the SQLite connection string in AudioPool.WebApi/appsettings.json:

```json
{
  "ConnectionStrings": {
    "AudioDbConnectionString": "Data Source=AudioPool.db"
  }
}
```

## EF Notes

- Migrations are created/applied with Repositories as the data project and WebApi as startup:
  - dotnet ef migrations add Initial -p AudioPool.Repositories -s AudioPool.WebApi
  - dotnet ef database update -p AudioPool.Repositories -s AudioPool.WebApi
- The DbContext is configured for SQLite in Program.cs.

## Development Notes

- Paging envelope is returned for list endpoints (PageNumber, PageSize, MaxPages, Items).
- HATEOAS links are added in the service layer.
- “Not found” flows throw KeyNotFoundException in repositories and are mapped to 404 in controllers/middleware.
- Validation attributes on input models enforce client errors (400).

## Running Tests

No unit tests are included yet.

---