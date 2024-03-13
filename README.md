# MinimalAPIWithAuthentication

This is a minimal ASP.NET Core API with authentication using JWT (JSON Web Tokens). The API allows users to log in and generates a JWT token for authentication.

## Features

- **JWT Authentication:** Secure authentication using JSON Web Tokens.
- **User Repository:** Simple in-memory user repository for demonstration purposes.
- **Authorization Policies:** Admin and user roles with basic authorization policies.
- **Dependency Injection:** Usage of dependency injection for token generation and user repository.
- **Configuration:** Configuration of JWT parameters using `appsettings.json`.
- **API Endpoints:**
  - `/login`: User authentication endpoint.
  - `/hello`: Public endpoint.
  - `/helloUser`: Authenticated user endpoint.
  - `/helloAdmin`: Authenticated admin endpoint.

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download)

## Configuration

Update the `appsettings.json` file with your JWT configuration:

```json
{
  "Jwt": {
    "Issuer": "your_issuer",
    "Audience": "your_audience",
    "Key": "your_secret_key"
    "ExpiresMinutes": "20"
  }
}
```

## Getting Started

### 1. Clone the repository:

```bash
git clone https://github.com/your_username/MinimalAPIWithAuthentication.git
cd MinimalAPIWithAuthentication
```

### 2. Restore dependencies and run the application:
```bash
dotnet restore
dotnet run
```

### 3. Open your browser and navigate to https://localhost:5001/hello to access the public endpoint.

### 4. To test authentication, use the ``/login`` endpoint with valid user credentials and use the generated token for authenticated endpoints.
