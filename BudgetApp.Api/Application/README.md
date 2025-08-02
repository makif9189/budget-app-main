# Application Layer

This layer contains the business logic and application services.

## 📁 Structure

- **Services/**: Business services implementing core interfaces
- **Validators/**: FluentValidation validators for DTOs
- **Extensions/**: Helper extensions for security and DI

## 🔧 Key Features

### Security
- Safe user ID extraction from JWT claims
- Proper authorization checks in all services
- Exception handling with custom exceptions

### Validation
- Comprehensive FluentValidation rules
- Async validation for uniqueness checks
- Consistent error messages using constants

### Services
- AutoMapper integration for clean object mapping
- ApiResponse wrapper for consistent API responses
- Transaction management for data consistency
- Proper exception handling and logging

### Best Practices
- Dependency injection with interface segregation
- Single responsibility principle
- Fail-fast validation
- Defensive programming