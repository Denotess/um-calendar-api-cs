# Calendar API (C#)

A secure ASP.NET Core Minimal API for serving calendar files (.ics format) with JWT authentication.

## What It Does

This API reads `.ics` calendar files from a `calendars` folder and serves them through a web API. It's built using C# and ASP.NET Core 9.0 with JWT Bearer token authentication.

## Features

- ✅ List all available calendars
- ✅ Retrieve individual calendar files
- ✅ JWT authentication for secure access
- ✅ Interactive API documentation with Swagger
- ✅ Health check endpoint
- ✅ Token generation for authorized users

## Prerequisites

- .NET 9.0 SDK

## Getting Started

### 1. Clone/Download the Project

```bash
cd um-calendar-api-cs
```

### 2. Create the Calendars Folder

The API will create it automatically if it doesn't exist, or you can create it manually:

```bash
mkdir calendars
```

Add your `.ics` files to this folder.

### 3. Run the API

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

### `GET /generate-my-token` 🔓 Public (Remove after use!)
Generates a JWT token valid for 1 year. **Use this once to get your token, then remove/comment out this endpoint!**

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Security Note:** Delete or comment out this endpoint after generating your token!

### `GET /names.json` 🔒 Protected
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

This API uses JWT (JSON Web Token) Bearer authentication.

### Getting Your Token:

1. Run the API: `dotnet run`
2. Visit `http://localhost:5000/generate-my-token` (or use curl/Postman)
3. Copy the token from the response
4. **Important:** Remove or comment out the `/generate-my-token` endpoint in `Program.cs`
5. Use the token for all protected endpoints

### Using the Token:

**With curl:**
```bash
curl -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  http://localhost:5000/names.json
```

**With Swagger:**
1. Click the 🔒 **Authorize** button
2. Enter your token (without "Bearer " prefix)
3. Click **Authorize** and **Close**
4. Now you can test protected endpoints

## How It Works

1. **File-Based Storage**: Calendar files are stored as `.ics` files in the `calendars` folder
2. **Minimal API**: Uses ASP.NET Core Minimal API for simple, clean endpoint definitions
3. **JWT Authentication**: Protected endpoints require valid JWT Bearer tokens
4. **No Database**: Files are read directly from disk - simple and effective for calendar files

## Configuration

JWT settings are stored in `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "your-secret-key-here",
    "Issuer": "calendar-api",
    "Audience": "calendar-api-users",
    "ExpirationInMinutes": 60
  }
}
```

**Security Notes:**
- The JWT `Key` should be at least 32 characters
- For production, use environment variables instead of appsettings.json
- Never commit real production keys to version control

## Technologies Used

- **C# / .NET 9.0** - Programming language and framework
- **ASP.NET Core Minimal API** - Web API framework
- **JWT Bearer Authentication** - Token-based security
- **NSwag** - OpenAPI/Swagger documentation

## Learning Points

This project demonstrates:
- Minimal API endpoint creation
- JWT authentication and authorization
- Token generation and validation
- File I/O operations in C#
- Route parameters
- Content-Type handling for different file types
- Swagger/OpenAPI documentation
- Middleware configuration (UseAuthentication, UseAuthorization)

## Next Steps

Potential enhancements:
- Add CORS configuration
- Implement background service for automatic calendar updates
- Add caching for better performance
- Add user management with roles
- Implement refresh tokens

## License

Free to use for learning purposes.