#!/bin/bash

echo "🚀 Deploying Secure Budget App to Production"

# Check if JWT_KEY is set
if [ -z "$JWT_KEY" ]; then
    echo "❌ Error: JWT_KEY environment variable is not set"
    echo "   Please set it with: export JWT_KEY=your-secure-key"
    exit 1
fi

# Build and start with Docker Compose
echo "🏗️ Building Docker image..."
docker-compose -f docker-compose.yml build

echo "🚀 Starting services..."
docker-compose -f docker-compose.yml up -d

echo "⏳ Waiting for services to be ready..."
sleep 30

# Health check
echo "🏥 Performing health check..."
if curl -f http://localhost:8080/health > /dev/null 2>&1; then
    echo "✅ Application is healthy and running!"
    echo "🌐 API is available at: http://localhost:8080"
    echo "📖 API documentation: http://localhost:8080/swagger"
else
    echo "❌ Health check failed. Check logs with: docker-compose logs budgetapp-api"
    exit 1
fi

echo "🎉 Deployment complete!"