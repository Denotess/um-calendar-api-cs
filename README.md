# Calendar API (C#)

A simple ASP.NET Core Minimal API for serving calendar files (.ics format).

## What It Does

This API reads `.ics` calendar files from a `calendars` folder and serves them through a web API. It's built using C# and ASP.NET Core 9.0.

## Features

- ✅ List all available calendars
- ✅ Retrieve individual calendar files
- ✅ Interactive API documentation with Swagger
- ✅ Health check endpoint

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

### `GET /health`
Health check endpoint.

**Response:**
```json
{
  "status": "healthy"
}
```

### `GET /names.json`
Returns a list of all available calendar names (without `.ics` extension).

**Response:**
```json
["calendar1", "calendar2", "my-schedule"]
```

### `GET /cal/{name}`
Returns the content of a specific calendar file.

**Parameters:**
- `name` - The calendar name (without `.ics` extension)

**Response:**
- Content-Type: `text/calendar; charset=utf-8`
- Body: The `.ics` file content

**Example:**
```
GET /cal/my-schedule
```

Returns the content of `calendars/my-schedule.ics`

## Project Structure

```
um-calendar-api-cs/
├── Program.cs              # Main API code
├── calendars/              # Folder for .ics files
├── appsettings.json        # Configuration
└── um-calendar-api-cs.csproj  # Project file
```

## How It Works

1. **File-Based Storage**: Calendar files are stored as `.ics` files in the `calendars` folder
2. **Minimal API**: Uses ASP.NET Core Minimal API for simple, clean endpoint definitions
3. **No Database**: Files are read directly from disk - simple and effective for calendar files

## Technologies Used

- **C# / .NET 9.0** - Programming language and framework
- **ASP.NET Core Minimal API** - Web API framework
- **NSwag** - OpenAPI/Swagger documentation

## Learning Points

This project demonstrates:
- Minimal API endpoint creation
- File I/O operations in C#
- Route parameters
- Content-Type handling for different file types
- Swagger/OpenAPI documentation

## Next Steps

Potential enhancements:
- Add authentication (Bearer token)
- Add CORS configuration
- Implement background service for automatic calendar updates
- Add caching for better performance

## License

Free to use for learning purposes.
