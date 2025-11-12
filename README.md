# Calendar API (C#)

A secure ASP.NET Core Minimal API for serving calendar files (.ics format) with JWT authentication and API key protection.

## What It Does

This API reads `.ics` calendar files from a `calendars` folder and serves them through a web API. It's built using C# and ASP.NET Core 9.0 with JWT Bearer token authentication and API key-protected token generation.

## Features

- ✅ List all available calendars
- ✅ Retrieve individual calendar files
- ✅ JWT authentication for secure access
- ✅ API key-protected token generation
- ✅ Environment variable configuration with .env support
- ✅ Interactive API documentation with Swagger
- ✅ Health check endpoint
- ✅ Organized endpoints with tags

## Prerequisites

- .NET 9.0 SDK

## Getting Started

### 1. Clone/Download the Project

```bash
cd um-calendar-api-cs
```

### 2. Set Up Environment Variables

Create a `.env` file in the project root:

```bash
# Generate secure keys (Linux/Mac):
openssl rand -base64 32  # For JWT_Key
openssl rand -hex 16     # For API_Key

# Or use PowerShell (Windows):
# [Convert]::ToBase64String((1..32 | ForEach-Object { Get-Random -Maximum 256 }))
```

Create `.env` file with your generated values:

```
Jwt__Key=YOUR_GENERATED_JWT_KEY_HERE
Jwt__ApiKey=YOUR_GENERATED_API_KEY_HERE
Jwt__Issuer=calendar-api
Jwt__Audience=calendar-api-users
```

**Important:** The `.env` file is already in `.gitignore` and won't be committed to Git.

### 3. Create the Calendars Folder

The API will create it automatically if it doesn't exist, or you can create it manually:

```bash
mkdir calendars
```

Add your `.ics` files to this folder.

### 4. Install Dependencies

```bash
dotnet restore
```

### 5. Run the API

```bash
dotnet run
```

The API will start at `http://localhost:5000` (or the port shown in your terminal).

## API Endpoints

### `GET /`
Redirects to Swagger documentation.

### `GET /health` 🔓 Public
Health check endpoint.

**Response:**
```json
{
  "status": "healthy"
}
```

### `GET /generate-token/` � Protected with API Key
Generates a JWT token valid for 1 year. This endpoint is protected with an API key.

**Headers:**
```
X-API-Key: YOUR_API_KEY_FROM_ENV
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Example with curl:**
```bash
curl -H "X-API-Key: YOUR_API_KEY" \
  http://localhost:5000/generate-token/
```

**Security Notes:** 
- This endpoint requires the API key from your `.env` file
- The generated token is valid for 1 year
- Store the token securely and use it for all protected endpoints
- You can regenerate tokens anytime by calling this endpoint with your API key

### `GET /names` 🔒 Protected
Returns a list of all available calendar names (without `.ics` extension).

**Authentication:** Requires JWT Bearer token

**Headers:**
```
Authorization: Bearer YOUR_TOKEN_HERE
```

**Response:**
```json
["calendar1", "calendar2", "my-schedule"]
```

### `GET /cal/{name}` 🔒 Protected
Returns the content of a specific calendar file.

**Authentication:** Requires JWT Bearer token

**Parameters:**
- `name` - The calendar name (without `.ics` extension)

**Headers:**
```
Authorization: Bearer YOUR_TOKEN_HERE
```

**Response:**
- Content-Type: `text/calendar; charset=utf-8`
- Body: The `.ics` file content

**Example:**
```bash
curl -H "Authorization: Bearer YOUR_TOKEN" \
  http://localhost:5000/cal/my-schedule
```

Returns the content of `calendars/my-schedule.ics`

## Authentication

This API uses a two-layer authentication system:

1. **API Key** - Required to generate JWT tokens (stored in `.env`)
2. **JWT Token** - Required to access protected calendar endpoints

### Getting Your Token:

**Step 1:** Generate a JWT token using your API key:

```bash
curl -H "X-API-Key: YOUR_API_KEY_FROM_ENV" \
  http://localhost:5000/generate-token/
```

**Step 2:** Copy the token from the response and save it securely (password manager, environment variable, etc.)

**Step 3:** Use the token for all protected endpoints

### Using the Token:

**With curl:**
```bash
curl -H "Authorization: Bearer YOUR_JWT_TOKEN_HERE" \
  http://localhost:5000/names
```

**With Swagger:**
1. First, get your JWT token from `/generate-token/` endpoint (use the API key)
2. Click the 🔒 **Authorize** button at the top
3. Enter your JWT token
4. Click **Authorize** and **Close**
5. Now you can test protected endpoints

### Token Lifecycle:

- Tokens are valid for **1 year** from generation
- When a token expires, generate a new one using your API key
- You can generate new tokens anytime without affecting existing ones
- Keep your API key secure - it can generate unlimited tokens

## How It Works

1. **File-Based Storage**: Calendar files are stored as `.ics` files in the `calendars` folder
2. **Minimal API**: Uses ASP.NET Core Minimal API for simple, clean endpoint definitions
3. **JWT Authentication**: Protected endpoints require valid JWT Bearer tokens
4. **No Database**: Files are read directly from disk - simple and effective for calendar files

## Configuration

This API uses environment variables for secure configuration management.

### Development Setup (.env file)

Configuration is loaded from a `.env` file (gitignored) in the project root:

```env
Jwt__Key=your-jwt-signing-key-here
Jwt__ApiKey=your-api-key-here
Jwt__Issuer=calendar-api
Jwt__Audience=calendar-api-users
```

**Note:** Double underscores (`__`) in environment variable names map to nested JSON structure.

### Production Setup

In production, set environment variables directly on your hosting platform:

**Docker:**
```bash
docker run -e Jwt__Key="prod-key" -e Jwt__ApiKey="prod-api-key" your-image
```

**Linux/systemd:**
```bash
export Jwt__Key="your-production-jwt-key"
export Jwt__ApiKey="your-production-api-key"
```

**Cloud Platforms:**
- AWS: Configuration → Environment Properties
- Azure: Settings → Configuration → Application Settings
- Use your platform's secrets management for sensitive values

### Configuration Values Explained

- **Jwt__Key** - Secret key for signing JWT tokens (min 32 characters)
- **Jwt__ApiKey** - Required header value to generate new tokens
- **Jwt__Issuer** - Identifies who issued the token
- **Jwt__Audience** - Identifies who the token is for

**Security Notes:**
- Never commit `.env` file to Git (already in `.gitignore`)
- Use strong, randomly generated keys
- Different keys for development and production
- Rotate keys periodically for better security

## Technologies Used

- **C# / .NET 9.0** - Programming language and framework
- **ASP.NET Core Minimal API** - Web API framework
- **JWT Bearer Authentication** - Token-based security
- **DotNetEnv** - Environment variable management
- **NSwag** - OpenAPI/Swagger documentation

## Learning Points

This project demonstrates:
- Minimal API endpoint creation with tags for organization
- JWT authentication and authorization
- Two-layer security (API Key + JWT Token)
- Token generation and validation
- Environment variable configuration with .env files
- File I/O operations in C#
- Route parameters and HTTP context
- Content-Type handling for different file types
- Header-based authentication
- Swagger/OpenAPI documentation
- Middleware configuration (UseAuthentication, UseAuthorization)

## Next Steps

Potential enhancements:
- Add CORS configuration for web client access
- Implement background service for automatic calendar updates
- Add caching for better performance
- Implement refresh tokens for better security
- Add rate limiting to prevent abuse
- Add logging and monitoring
- Support for multiple API keys with different permissions
- Calendar upload/management endpoints

## License

Free to use for learning purposes.