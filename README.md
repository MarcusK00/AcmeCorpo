# AcmeCorpo - Prize Draw Web Application

ASP.NET (.NET 10) prize draw web application with EF Core, layered architecture, business rule validation, and unit tests.

## 📦 Project Structure

This project follows a layered architecture pattern:
```
AcmeCorpo/
├── src/
│   ├── Acme.Api/          # Web API project
│   ├── Acme.Client/       # Blazor Server Web App
│   ├── Acme.Core/         # Business logic & data access
│   └── Acme.Shared/       # Shared models
├── tests/
│   └── Acme.Tests/        # Unit tests
└── Acme.Draw.slnx         # Solution file
```

## 📋 Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for SQL Server)
- Install EF Core CLI tools (one-time setup):
  ```bash
  dotnet tool install --global dotnet-ef

## 🐳 Docker SQL Server Setup

This application uses a Docker container for SQL Server instead of a local installation.

# Getting Started

### 1. Clone and Restore Repository

1. Clone the repository: `git clone https://github.com/MarcusK00/AcmeCorpo.git`
2. Navigate to the project: `cd AcmeCorpo`
3. Restore NuGet packages: `dotnet restore`
   
### 2. Start the SQL Server Container

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

### 3. Verify the Container is Running

```bash
docker ps
```

You should see the `acme-sql` container in the list.

## ⚙️ Configuration

The connection string is already configured in `src/Acme.Api/appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=AcmeDraw;User Id=sa;Password=Str0ng?Pass123;TrustServerCertificate=True;"
}
```

## 🗄️ Database Setup

### Create Initial Migration

- Please make sure Docker container is running for this to work 
From the root directory, run:

```bash
dotnet ef migrations add InitialCreate --project src/Acme.Core --startup-project src/Acme.Api

```
```bash
dotnet ef database update --project src/Acme.Core --startup-project src/Acme.Api
```

This will create the `AcmeDraw` database and apply all migrations.

## 🚀 Running the Application

The application consists of two parts that need to run simultaneously:

Using Visual Studio / Rider

1. Open `Acme.Draw.slnx` in your IDE
2. Configure multiple startup projects:
   - Right-click the solution → Properties
   - Select "Multiple startup projects"
   - Set both `Acme.Api` and `Acme.Client` to "Start"
3. Click Run

### Access the Application

Once both are running:
- **Web Application:** http://localhost:5056
- **API:** http://localhost:5093

## 🧪 Running Tests

From the root directory:

```bash
dotnet test
```

Or to run tests in a specific project:

```bash
dotnet test tests/Acme.Tests/Acme.Tests.csproj
```
##  Serial Numbers

First time the program is run, it will create a .txt file containing 100 serial keys in src/Acme.Api folder.

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

## 📝 License

This project is for educational/demonstration purposes.
