#!/bin/bash

echo "🚀 Setting up Secure Budget App - Development Environment"

# Set environment variables
export ASPNETCORE_ENVIRONMENT=Development
export JWT_KEY=U2VjdXJlQnVkZ2V0QXBwS2V5UmVhbGx5TG9uZ0FuZFNlY3VyZUtleUZvckpXVFRva2VuU2lnbmluZ1B1cnBvc2Vz

echo "✅ Environment variables set"

# Restore packages
echo "📦 Restoring NuGet packages..."
dotnet restore

# Create logs directory
mkdir -p Logs
echo "📁 Logs directory created"

# Run database migrations (if needed)
echo "🗄️ Running database migrations..."
dotnet ef database update --verbose

echo "🎉 Development environment setup complete!"
echo "🌐 Run 'dotnet run' to start the application"
echo "📖 Visit https://localhost:5001/swagger for API documentation"