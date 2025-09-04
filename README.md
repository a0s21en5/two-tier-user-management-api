# Two-Tier Web Application

A modern ASP.NET Core Web API application demonstrating a two-tier architecture with comprehensive user management functionality using CQRS pattern, MediatR, Dapper ORM, and SQL Server.

## 🏗️ Architecture

This application follows a **two-tier architecture**:
- **Presentation Tier**: ASP.NET Core Web API with RESTful endpoints
- **Data Tier**: SQL Server database with optimized data access using Dapper

### Design Patterns & Technologies

- **CQRS (Command Query Responsibility Segregation)** with MediatR
- **Repository Pattern** with Dapper ORM
- **Validation** using FluentValidation
- **Dependency Injection** with built-in ASP.NET Core DI container
- **OpenAPI/Swagger** documentation
- **Docker** containerization support

## 🚀 Features

### User Management
- ✅ Create new users
- ✅ Retrieve all active users
- ✅ Get user by ID
- ✅ Search users by email
- ✅ Update existing users
- ✅ Soft delete users
- ✅ Input validation with detailed error messages

### Technical Features
- 🔄 Automatic database initialization
- 📊 Comprehensive API documentation with Swagger UI
- 🐳 Docker containerization
- 🛡️ Input validation and error handling
- 📈 Performance optimized with database indexing
- 🔍 Search functionality
- 📝 Structured logging

## 🛠️ Technology Stack

### **Core Framework**
- **🎯 .NET 9.0** - Latest Microsoft development platform
- **🌐 ASP.NET Core** - Cross-platform web framework

### **Data & Persistence**
- **🏗️ Dapper 2.1.35** - Lightweight, high-performance ORM
- **🗄️ SQL Server 2022** - Enterprise-grade relational database
- **📊 Microsoft.Data.SqlClient 5.2.2** - Modern SQL Server connectivity

### **Architecture & Patterns**
- **📡 MediatR 12.4.1** - CQRS and Mediator pattern implementation
- **✅ FluentValidation 11.10.0** - Fluent interface for validation rules

### **API & Documentation**
- **📖 Swashbuckle.AspNetCore 7.2.0** - OpenAPI/Swagger documentation
- **🔧 Microsoft.AspNetCore.OpenApi 9.0.8** - OpenAPI specification support

### **Development & Deployment**
- **🐳 Docker** - Containerization platform
- **🔨 Visual Studio Code** - Recommended IDE with REST Client extension
- **⚙️ PowerShell** - Windows development environment

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or Docker container)
- [Docker](https://www.docker.com/get-started) (optional, for containerization)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/a0s21en5/two-tier-user-management-api.git
cd two-tier-web-app
```

### 2. Choose Your Setup Method

You can run this application using either .NET CLI or Docker. Choose the method that best fits your environment:

#### Option A: Local Development (.NET CLI)

1. **Database Setup**: Ensure SQL Server is running locally and update the connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=TwoTierWebAppDB;User Id=sa;Password=your-password;TrustServerCertificate=True;MultipleActiveResultSets=true;"
     }
   }
   ```

2. **Run the Application**:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

#### Option B: Full Docker Setup (Recommended)

1. **Create Docker Network**:
   ```bash
   docker network create -d bridge two-tier
   ```

2. **Start SQL Server Container**:
   ```bash
   docker run -d --name sqlserver2022 --network two-tier \
     -e "ACCEPT_EULA=Y" \
     -e "MSSQL_SA_PASSWORD=Obito#9775" \
     mcr.microsoft.com/mssql/server:2022-latest
   ```

3. **Build and Run Application**:
   ```bash
   docker build -t two-tier-backend:v1 .
   docker run -d -p 5000:5000 --network two-tier two-tier-backend:v1
   ```

### 3. Access the Application

Once running, you can access the application at:

- **🌐 API Base URL**: `http://localhost:5000`
- **📖 Swagger UI**: `http://localhost:5000/swagger`
- **📋 OpenAPI Spec**: `http://localhost:5000/swagger/v1/swagger.json`

> **Note**: The application runs on HTTP port 5000 when using Docker. For local development with .NET CLI, both HTTP (5000) and HTTPS (5001) are available.

## 📚 API Documentation

### 🔗 API Endpoints

The application provides a RESTful API for user management operations:

| Method | Endpoint | Description | Status Codes |
|--------|----------|-------------|--------------|
| 🟢 GET | `/api/users` | Retrieve all active users | 200 |
| 🟢 GET | `/api/users/{id}` | Get user by ID | 200, 400, 404 |
| 🔍 GET | `/api/users/search?email={email}` | Search users by email | 200, 400 |
| ✅ POST | `/api/users` | Create a new user | 201, 400, 500 |
| 🔄 PUT | `/api/users/{id}` | Update existing user | 204, 400, 404, 500 |
| ❌ DELETE | `/api/users/{id}` | Soft delete user | 204, 400, 404, 500 |

### 📝 Example API Calls

#### Create User
```http
POST /api/users
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phone": "+1234567890"
}
```

**Response** (201 Created):
```json
3
```

#### Get All Users
```http
GET /api/users
```

**Response** (200 OK):
```json
[
  {
    "id": 1,
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "phone": "+1234567890",
    "createdDate": "2025-09-04T10:30:00Z",
    "updatedDate": null,
    "isActive": true
  },
  {
    "id": 2,
    "firstName": "Jane",
    "lastName": "Smith",
    "email": "jane.smith@example.com",
    "phone": "+1987654321",
    "createdDate": "2025-09-04T11:15:00Z",
    "updatedDate": "2025-09-04T12:00:00Z",
    "isActive": true
  }
]
```

#### Get User by ID
```http
GET /api/users/1
```

**Response** (200 OK):
```json
{
  "id": 1,
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phone": "+1234567890",
  "createdDate": "2025-09-04T10:30:00Z",
  "updatedDate": null,
  "isActive": true
}
```

#### Search Users by Email
```http
GET /api/users/search?email=john
```

#### Update User
```http
PUT /api/users/1
Content-Type: application/json

{
  "id": 1,
  "firstName": "John",
  "lastName": "Smith",
  "email": "john.smith@example.com",
  "phone": "+1234567890"
}
```

**Response** (204 No Content)

#### Delete User
```http
DELETE /api/users/1
```

**Response** (204 No Content)

### 🔧 Testing the API

1. **Swagger UI**: Navigate to `http://localhost:5000/swagger` for interactive testing
2. **HTTP Files**: Use the provided `.http` files in VS Code with REST Client extension
3. **Postman**: Import the API endpoints using the OpenAPI specification
4. **cURL**: Use command-line tools for testing

### 🛡️ Error Responses

The API returns consistent error responses:

```json
{
  "errors": [
    "FirstName is required",
    "Email must be a valid email address"
  ]
}
```

Common HTTP status codes:
- **200**: Success
- **201**: Created
- **204**: No Content (successful update/delete)
- **400**: Bad Request (validation errors)
- **404**: Not Found
- **500**: Internal Server Error

## 🗂️ Project Structure

```
two-tier-web-app/
├── 📁 Controllers/
│   └── 📄 UsersController.cs          # REST API endpoints and HTTP request handling
├── 📁 Features/                       # CQRS implementation
│   └── 📁 Users/
│       ├── 📁 Commands/               # Write operations (Create, Update, Delete)
│       ├── 📁 Queries/                # Read operations (Get, Search)
│       └── 📁 Validators/             # FluentValidation rules for requests
├── 📁 Infrastructure/                 # Core infrastructure services
│   ├── 📄 DatabaseInitializer.cs     # Automatic database setup and seeding
│   └── 📄 DbConnectionFactory.cs     # Database connection management
├── 📁 Models/                         # Data models and DTOs
│   └── 📄 User.cs                     # User entity and request/response models
├── 📁 Database/                       # Database scripts
│   └── 📄 CreateDatabase.sql          # Database schema and sample data
├── 📁 Properties/                     # Application configuration
│   └── 📄 launchSettings.json         # Development server settings
├── 📄 Program.cs                      # Application entry point and service configuration
├── 📄 Dockerfile                      # Container configuration
├── 📄 appsettings.json               # Application configuration
├── 📄 appsettings.Development.json   # Development environment settings
├── 📄 appsettings.Production.json    # Production environment settings
└── 📄 *.http                         # HTTP test files for API testing
```

### 🏛️ Architecture Layers

#### **Presentation Layer**
- **Controllers**: Handle HTTP requests and responses
- **Validation**: Input validation using FluentValidation

#### **Application Layer** 
- **Commands**: Handle write operations (CQRS)
- **Queries**: Handle read operations (CQRS)
- **MediatR**: Decouples request handling

#### **Infrastructure Layer**
- **Database**: SQL Server with Dapper ORM
- **Connection Management**: Factory pattern for database connections

#### **Domain Layer**
- **Models**: Core business entities and DTOs

## 🔧 Configuration

### Environment-Specific Settings

The application supports multiple environments with specific configuration files:

- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Development environment
- `appsettings.Production.json` - Production environment

### Key Configuration Options

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server connection string"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 🧪 Testing

### **Included Test Files**

The project includes HTTP test files for comprehensive API testing:

- **📁 `two-tier-web-app.http`** - General API endpoint tests
- **📁 `two-tier-web-app-api.http`** - Specific user management scenarios

### **Testing Tools & Methods**

#### **1. VS Code REST Client** (Recommended)
- Install the "REST Client" extension in VS Code
- Open any `.http` file and click "Send Request" above each HTTP call
- View responses directly in VS Code

#### **2. Swagger UI** (Interactive)
- Navigate to `http://localhost:5000/swagger`
- Test all endpoints with an interactive interface
- View request/response schemas and examples

#### **3. Postman/Insomnia**
- Import the OpenAPI specification: `http://localhost:5000/swagger/v1/swagger.json`
- Automatically generate a complete collection of API calls

#### **4. cURL Command Line**
```bash
# Test API health
curl -X GET "http://localhost:5000/api/users"

# Create a new user
curl -X POST "http://localhost:5000/api/users" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Test",
    "lastName": "User",
    "email": "test@example.com",
    "phone": "+1234567890"
  }'
```

### **Test Scenarios Covered**

✅ **CRUD Operations**
- Create new users with valid data
- Retrieve all users
- Get specific users by ID
- Update existing user information  
- Soft delete users

✅ **Validation Testing**
- Required field validation
- Email format validation
- Input length constraints
- Invalid ID handling

✅ **Error Handling**
- Non-existent user lookup
- Invalid request formats
- Server error scenarios

### **Sample Test Data**

The database initializes with sample users for immediate testing:
- John Doe (john.doe@example.com)
- Jane Smith (jane.smith@example.com)
- Bob Johnson (bob.johnson@example.com)
- Alice Brown (alice.brown@example.com)
- Charlie Wilson (charlie.wilson@example.com)

## 🐳 Docker Support

### Network Architecture

The Docker setup uses a custom bridge network to enable communication between containers:

```
┌─────────────────────────────────────┐
│           two-tier network          │
│                                     │
│  ┌─────────────────┐ ┌────────────┐ │
│  │  sqlserver2022  │ │ web-app    │ │
│  │  (SQL Server)   │ │ (Backend)  │ │
│  │  Port: 1433     │ │ Port: 5000 │ │
│  └─────────────────┘ └────────────┘ │
│                                     │
└─────────────────────────────────────┘
```

### Container Management

#### View Running Containers
```bash
docker ps
```

#### Check Application Logs
```bash
# Get container ID first
docker ps

# View logs
docker logs <container-id>

# Follow logs in real-time
docker logs -f <container-id>
```

#### Stop and Clean Up
```bash
# Stop containers
docker stop sqlserver2022 <web-app-container-id>

# Remove containers
docker rm sqlserver2022 <web-app-container-id>

# Remove network
docker network rm two-tier

# Remove image (optional)
docker rmi two-tier-backend:v1
```

### Alternative: Docker Compose

For easier management, create a `docker-compose.yml` file:

```yaml
version: '3.8'

networks:
  two-tier:
    driver: bridge

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: sqlserver2022
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=Obito#9775
    networks:
      - two-tier
    volumes:
      - sqlserver_data:/var/opt/mssql
    healthcheck:
      test: ["CMD-SHELL", "/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Obito#9775 -Q 'SELECT 1'"]
      interval: 30s
      timeout: 10s
      retries: 3

  web-app:
    build: .
    container_name: two-tier-backend
    ports:
      - "5000:5000"
    depends_on:
      sqlserver:
        condition: service_healthy
    networks:
      - two-tier
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Server=sqlserver2022,1433;Database=TwoTierWebAppDB;User Id=sa;Password=Obito#9775;TrustServerCertificate=True;MultipleActiveResultSets=true;

volumes:
  sqlserver_data:
```

#### Using Docker Compose
```bash
# Start all services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop all services
docker-compose down

# Stop and remove volumes
docker-compose down -v
```

## 📊 Database Schema

### Users Table

| Column | Type | Description |
|--------|------|-------------|
| Id | INT IDENTITY | Primary key |
| FirstName | NVARCHAR(50) | User's first name |
| LastName | NVARCHAR(50) | User's last name |
| Email | NVARCHAR(100) | User's email address |
| Phone | NVARCHAR(20) | User's phone number |
| CreatedDate | DATETIME2 | Record creation timestamp |
| UpdatedDate | DATETIME2 | Last update timestamp |
| IsActive | BIT | Soft delete flag |

### Indexes
- `IX_Users_Email` - Email lookup optimization
- `IX_Users_IsActive` - Active users filtering
- `IX_Users_CreatedDate` - Date-based queries

## 🔍 Validation Rules

### Create User Request
- **FirstName**: Required, max 50 characters
- **LastName**: Required, max 50 characters
- **Email**: Required, valid email format, max 100 characters
- **Phone**: Required, max 20 characters

### Update User Request
- **Id**: Required, must be positive integer
- All fields from Create User Request

## 🚀 Development

### Adding New Features

1. **Commands/Queries**: Add to `Features/Users/Commands` or `Features/Users/Queries`
2. **Validation**: Add validators to `Features/Users/Validators`
3. **API Endpoints**: Extend `UsersController`
4. **Database Changes**: Update `CreateDatabase.sql` and `DatabaseInitializer`

### Best Practices

- Follow CQRS pattern for separation of concerns
- Use FluentValidation for input validation
- Implement proper error handling and logging
- Write comprehensive API documentation
- Use async/await for database operations

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

For support and questions:
- Create an issue in the repository
- Contact the development team

---

**Happy Coding! 🎉**
