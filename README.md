# README

## Overview
A comprehensive employee management solution composed of three interconnected components: ASP.NET Core API, React client application, and Telegram bot. This system facilitates efficient employee data management with customizable notifications, interactive form completion, and optimized data retrieval.

## Architecture

The system is built on a microservices architecture with three main components:
- **Core API Service**: Handles primary business logic, data management, and authentication
- **Client Application**: React-based frontend for administrative interface
- **Telegram Bot Service**: Provides alternative interface for form completion and notifications

## Key Features

Our system is built on a foundation of ASP.NET Core Web API with secure authentication through ASP.NET Identity. We've implemented token-based authentication with cookie storage and comprehensive request validation using FluentValidation middleware. The data layer leverages PostgreSQL hosted on AWS RDS, with entity mapping handled efficiently by AutoMapper and document storage managed through AWS S3 integration.

The API design combines traditional RESTful endpoints with GraphQL capabilities (using HotChocolate) to optimize data retrieval operations. This dual approach allows for flexible data fetching on tabular pages while maintaining performance. Our notification system utilizes Quartz.NET for scheduling important alerts like employee birthday reminders, with RabbitMQ serving as the message broker between the API and Telegram bot service. This ensures reliable message delivery and maintains loose coupling between system components.

For quality assurance, we've implemented comprehensive unit testing with xUnit, utilizing Moq for dependencies and AutoFixture for generating test data. This testing approach verifies the functionality of our validation middleware, business logic, and integration points throughout the application.

The client application is developed as a React TypeScript frontend with Redux for state management. We've utilized Material UI and AgGrid component libraries for a consistent and responsive interface, with SASS providing advanced styling capabilities. The application offers intuitive navigation and comprehensive data visualization for administrators.

Our Telegram bot integration provides an alternative interface for form completion and delivers notifications directly to administrators. The bidirectional communication between Telegram and the main API is facilitated through RabbitMQ, ensuring reliable message delivery even during high system load. This conversational interface simplifies routine data entry tasks and improves notification visibility.
## Technical Stack

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL (AWS RDS)
- GraphQL (HotChocolate)
- Identity Framework
- Quartz.NET for scheduling
- FluentValidation
- AutoMapper
- xUnit, Moq, and AutoFixture for testing
- RabbitMQ for message queuing

### Frontend
- React with TypeScript
- Redux for state management
- Material UI components
- AgGrid for data tables
- SASS for styling

### Cloud & Infrastructure
- AWS RDS for database hosting
- AWS S3 for file storage
- AWS EC2 and Docker for Redis and RabbitMQ hosting
