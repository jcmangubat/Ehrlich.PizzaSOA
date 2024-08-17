# `Ehrlich.PizzaSOA` Architecture Overview

## `Ehrlich Pizza Sales Order Analytics`

`Ehrlich.PizzaSOA` is a Web API built in ASP.Net Core in .Net 8. Its aim is to provide analytics reporting of the a Pizza business proprietor. It follows Domain-Driven Design (DDD) principles to facilitate easy management and scaling.

## Key Components

### 1. **Domain-Driven Design (DDD)**

- **Domain**: This layer contains the core business logic. It includes entities such as `Order`, `Customer`, and `Pizza`.
- **Application Layer**: This layer coordinates actions between the domain and external interactions. It includes services like `OrderService`.
- **Infrastructure Layer**: This layer manages data storage and external integrations.
- **Presentation Layer**: This layer includes API controllers that interact with users.

### 2. **Referenced Packages**

- **Entity Framework Core**: Manages database operations.
- **AutoMapper**: Handles conversion between different data formats.
- **FluentValidation**: Validates data before processing.
- **Serilog**: Manages logging and error tracking.
- **Swagger / Swashbuckle**: Provides interactive API documentation.
- **SMEAppHouse.Core.Patterns.EF**: Library for implementing entity structural and adapter pattern.
- **SMEAppHouse.Core.Patterns.Repo**: Library for implementing generic data composite modeling and repostitory pattern strategy.

### 3. **Layers in the Architecture**

- **Domain Layer**: Contains core business rules and data models.
- **Application Layer**: Contains services that utilize domain logic.
- **Infrastructure Layer**: Manages data access and external systems.
- **Presentation Layer**: Includes API controllers for user interactions.

### 4. **Configuration and Dependency Injection**

The `Startup` class configures settings and manages dependencies.

### 5. **Testing**

The project includes:
- **Integration Tests**: Tests interactions between components.
- **Unit Tests**: Tests individual components of the code (Not implemented yet).
- **End-to-End Tests**: Tests the application as a whole (Not implemented yet).

## Summary

`Ehrlich.PizzaSOA` utilizes DDD principles to structure its code around business domains. It uses tools such as Entity Framework Core and AutoMapper to handle data management and format conversion. The architecture is organized into distinct layers for domain logic, application services, data access, and user interactions.
