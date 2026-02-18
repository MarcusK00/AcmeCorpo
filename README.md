# AcmeCorpo - Prize Draw Web Application

ASP.NET (.NET 10) prize draw web application with EF Core, layered architecture, business rule validation, and unit tests.

## 🏗️ Architecture

This project follows a layered architecture pattern:

- **Acme.Client** - Blazor Server web application (frontend)
- **Acme.Api** - ASP.NET Core Web API (backend API)
- **Acme.Core** - Business logic, services, data access, and EF Core
- **Acme.Shared** - Shared models and DTOs
- **Acme.Tests** - MSTest unit tests

## 📋 Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for SQL Server)

## 🐳 Docker SQL Server Setup

This application uses a Docker container for SQL Server instead of a local installation.

### 1. Start the SQL Server Container

Run this command to create and start the SQL Server container:

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Str0ng?Pass123' 
  -p 1433:1433 --name acme-sql -d mcr.microsoft.com/mssql/server:2022-latest
```

This command:
- Accepts the SQL Server EULA
- Sets the SA password to `Str0ng?Pass123`
- Maps port 1433 from the container to your localhost
- Names the container `acme-sql`
- Runs SQL Server 2022 in detached mode

### 2. Verify the Container is Running

```bash
docker ps
```

You should see the `acme-sql` container in the list.

### 3. Manage the Container

**Stop the container:**
```bash
docker stop acme-sql
```

**Start the container (after stopping):**
```bash
docker start acme-sql
```

**Remove the container (WARNING: This deletes all data):**
```bash
docker stop acme-sql
docker rm acme-sql
```

**View container logs:**
```bash
docker logs acme-sql
```

## ⚙️ Configuration

The connection string is already configured in `src/Acme.Client/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=AcmeDraw;User Id=sa;Password=Str0ng?Pass123;TrustServerCertificate=True;"
}
```

**Note:** The API (`Acme.Api`) does not have a connection string configured. If needed, add the connection string to `src/Acme.Api/appsettings.json`.

## 🗄️ Database Setup

### Create Initial Migration

From the root directory, run:

```bash
dotnet ef migrations add InitialCreate --project src/Acme.Core --startup-project src/Acme.Client
```

### Apply Migration to Database

```bash
dotnet ef database update --project src/Acme.Core --startup-project src/Acme.Client
```

This will create the `AcmeDraw` database and apply all migrations.

## 🚀 Running the Application

The application consists of two parts that need to run simultaneously:

### Option 1: Using Two Terminal Windows

**Terminal 1 - Run the API:**
```bash
cd src/Acme.Api
dotnet run
```
The API will start on `http://localhost:5093`

**Terminal 2 - Run the Client:**
```bash
cd src/Acme.Client
dotnet run
```
The Client will start on `http://localhost:5056`

### Option 2: Using Visual Studio / Rider

1. Open `Acme.Draw.slnx` in your IDE
2. Configure multiple startup projects:
   - Right-click the solution → Properties
   - Select "Multiple startup projects"
   - Set both `Acme.Api` and `Acme.Client` to "Start"
3. Press F5 or click Run

### Access the Application

Once both are running:
- **Web Application:** http://localhost:5056
- **API:** http://localhost:5093
- **API Documentation (Swagger):** http://localhost:5093/openapi/v1.json (in Development mode)

## 🧪 Running Tests

From the root directory:

```bash
dotnet test
```

Or to run tests in a specific project:

```bash
dotnet test tests/Acme.Tests/Acme.Tests.csproj
```

## 📦 Project Structure

```
AcmeCorpo/
├── src/
│   ├── Acme.Api/          # Web API project
│   ├── Acme.Client/       # Blazor Server web app
│   ├── Acme.Core/         # Business logic & data access
│   └── Acme.Shared/       # Shared models
├── tests/
│   └── Acme.Tests/        # Unit tests
└── Acme.Draw.slnx         # Solution file
```

## 🔧 Troubleshooting

### Docker Container Issues

**Container won't start:**
- Check if port 1433 is already in use: `netstat -an | findstr 1433` (Windows) or `lsof -i :1433` (Mac/Linux)
- Check Docker Desktop is running
- View container logs: `docker logs acme-sql`

**Can't connect to SQL Server:**
- Verify the container is running: `docker ps`
- Ensure the password matches in both the docker command and appsettings.json
- Try connecting with a database tool (Azure Data Studio, SQL Server Management Studio, DBeaver) using:
  - Server: `localhost,1433`
  - User: `sa`
  - Password: `Str0ng?Pass123`

### Application Issues

**Port conflicts:**
- If ports 5093 or 5056 are in use, you can specify different ports:
  ```bash
dotnet run --urls "http://localhost:PORT"
  ```

**Migration errors:**
- Ensure the Docker container is running
- Verify the connection string is correct
- Try deleting the database and re-running migrations:
  ```bash
dotnet ef database drop --project src/Acme.Core --startup-project src/Acme.Client
  dotnet ef database update --project src/Acme.Core --startup-project src/Acme.Client
  ```

**API can't connect to Client:**
- Verify both applications are running
- Check CORS settings in `src/Acme.Api/Program.cs`
- Ensure the Client URL matches the CORS policy (currently set to port 5056)

## 🗃️ Database Management

You can connect to the database using any SQL client:

- **Server:** `localhost,1433`
- **Database:** `AcmeDraw`
- **Authentication:** SQL Server Authentication
- **Username:** `sa`
- **Password:** `Str0ng?Pass123`

Recommended tools:
- [Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio) (cross-platform)
- [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (Windows only)
- [DBeaver](https://dbeaver.io/) (cross-platform)

## 📝 License

This project is for educational/demonstration purposes.