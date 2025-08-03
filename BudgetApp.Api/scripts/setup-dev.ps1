Write-Host "🚀 Setting up Secure Budget App - Development Environment" -ForegroundColor Green

# Set environment variables
$env:ASPNETCORE_ENVIRONMENT = "Development"
$env:JWT_KEY = "U2VjdXJlQnVkZ2V0QXBwS2V5UmVhbGx5TG9uZ0FuZFNlY3VyZUtleUZvckpXVFRva2VuU2lnbmluZ1B1cnBvc2Vz"

Write-Host "✅ Environment variables set" -ForegroundColor Green

# Restore packages
Write-Host "📦 Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore

# Create logs directory
New-Item -ItemType Directory -Force -Path "Logs"
Write-Host "📁 Logs directory created" -ForegroundColor Green

# Run database migrations (if needed)
Write-Host "🗄️ Running database migrations..." -ForegroundColor Yellow
dotnet ef database update --verbose

Write-Host "🎉 Development environment setup complete!" -ForegroundColor Green
Write-Host "🌐 Run 'dotnet run' to start the application" -ForegroundColor Cyan
Write-Host "📖 Visit https://localhost:5001/swagger for API documentation" -ForegroundColor Cyan