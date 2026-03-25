# ProductManagement API

A .NET 8 Web API for product management (e-commerce backend). This repository provides the basic backend services for managing products, categories, brands, orders, users, notifications, and more.

## Key features
- .NET 8
- Entity Framework Core (SQL Server)
- JWT authentication
- MailKit for email delivery
- SignalR for realtime features
- AutoMapper and Swagger (Swashbuckle)

## Prerequisites
- .NET 8 SDK: `https://dotnet.microsoft.com/download`
- SQL Server (or a compatible SQL Server instance)
- (Optional) Docker for containerized deployments

## Configuration
This project uses configuration files. If you prefer not to use environment variables, create an `appsettings.Production.json` (or edit `appsettings.json`) and add production values there.

Suggested approach:
- Keep a template `appsettings.json` in source control with empty placeholders.
- Create `appsettings.Production.json` with real values and do NOT commit it. Add `appsettings.Production.json` to `.gitignore`.
- For local development use `appsettings.Development.json` or `dotnet user-secrets`.

Example `appsettings.Production.json`:
```json
{
  "ConnectionStrings": { "ProductManagement": "Server=...;Database=...;User Id=...;Password=...;" },
  "Jwt": { "Key": "<your_jwt_key>", "Issuer": "<issuer>" },
  "EmailSettings": { "SmtpServer": "smtp.example.com", "Port": 587, "SenderEmail": "noreply@example.com", "SenderName": "ProductManagement", "Password": "<password>" },
  "GeminiSettings": { "ApiKey": "<api_key>", "ApiUrl": "https://..." }
}
```

Notes:
- Make sure `appsettings.Production.json` is excluded from version control.
- Using a secret manager (Azure Key Vault, AWS Secrets Manager, etc.) is still the recommended production practice even if you don't use environment variables.

## Database
The project uses EF Core migrations (see the `Migrations` folder). From the project directory run:

```
cd ProductManagement
dotnet tool restore
dotnet ef database update
```

If `dotnet ef` is not installed globally, install it:

```
dotnet tool install --global dotnet-ef
```

## Run (development)
From the repository root:

```
cd ProductManagement
dotnet run
```

Swagger UI is available in development mode (typically at `/swagger`).

