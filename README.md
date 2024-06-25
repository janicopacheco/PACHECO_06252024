# ProcessUploadedFile API

## Overview

This is a RESTful web service built using ASP.NET Core that securely processes uploaded files and returns the results. The service supports JSON file formats and includes an API Key-based security mechanism. It tracks the files processed and provides basic reporting of these files upon request.

## Features

1. **Secure Web Service**:
   - Implemented using ASP.NET Core.
   - File upload endpoint is secured with an API Key authentication mechanism.

2. **File Processing**:
   - Supports JSON file formats.
   - For JSON, performs a simple data transformation (filtering based on a condition).

3. **API Key Authentication**:
   - Basic API key check using middleware.
   - Service validates the API key sent in the request header.

4. **File Tracking and Reporting**:
   - Tracks the number of files processed.
   - Logs basic file processing details (filename, processing time).

5. **Containerization**:
   - Service is containerized using Docker.
   - Provides a Dockerfile for building and running the container.

## API Endpoints

### 1. Upload File

- **URL**: `/api/fileprocessing/upload`
- **Method**: `POST`
- **Headers**:
  - `X-API-KEY`: Your API Key
- **Form Data**:
  - `file`: The file to be uploaded (JSON format)
  - `filter`: (Optional) The filter condition to be applied to the JSON data.
- **Response**:
  - `200 OK`: Returns a success message and the result.
  - `400 Bad Request`: If the file is missing or invalid.
  - `401 Unauthorized`: If the API Key is invalid.

#### Example Request

```bash
curl -X POST "http://localhost:8080/api/fileprocessing/upload" -H "X-API-KEY: your_api_key" -F "file=@/path/to/your/file.json" -F "filter=some_filter"
```

### 2. Generate Reports

- **URL**: `/api/fileprocessing/report`
- **Method**: `GET`
- **Headers**:
  - `X-API-KEY`: Your API Key
- **Response**:
  - `200 OK`: Returns a report of the processed files.
  - `401 Unauthorized`: If the API Key is invalid.

#### Example Request

```bash
curl -X GET "http://localhost:8080/api/fileprocessing/report"
```


## Including API Keys in Program.cs

The API keys required to access the API endpoints are included in the `Program.cs` file using the following steps:

1. **Declaration of API Keys**:
   - In the `Program.cs` file, a variable named `apikeys` is declared to store the API keys required for authentication.
   - Example: `var apikeys = new List<string> { "sample-1-apikey", "sample-2-apikey", "janicopacheco" };`

2. **Use of API Key Middleware**:
   - The `apikeys` variable is then passed to the `ApiKeyMiddleware` using the `UseMiddleware<ApiKeyMiddleware>(apikeys)` method in the ASP.NET Core application builder.

With these configurations, the API keys are validated by the `ApiKeyMiddleware` before allowing access to the API endpoints.


```bash
curl -X POST "http://localhost:8080/api/fileprocessing/upload" -H "X-API-KEY: your_generated_api_key" -F "file=@/path/to/your/file.json" -F "filter=some_filter"
```


## Using the API
- Use the Swagger UI to test the endpoints.
- Alternatively, use curl or any HTTP client to send requests to the API.

## Contact
- **Author**: `Janico Pacheco`
- **Email**: `janicopacheco@yahoo.com`