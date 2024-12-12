# To-Do Application with Microservices Architecture

This project is a **To-Do Application** built with a microservices architecture. It is designed to be deployed on **Azure**, using **SQL Server** as the database, an **API Gateway** for centralized routing, and **Serilog** for logging via a shared library.

---

## Table of Contents

1. [Architecture Overview](#architecture-overview)
2. [Technologies Used](#technologies-used)
3. [Microservices](#microservices)
   - [Auth Service](#auth-service)
   - [User Service](#user-service)
   - [To-Do Service](#to-do-service)
4. [API Gateway](#api-gateway)
5. [Logging](#logging)
6. [Database](#database)
7. [Development Workflow](#development-workflow)
8. [Deployment](#deployment)
9. [Future Enhancements](#future-enhancements)

---

## Architecture Overview

The system is built with the following core components:

- **API Gateway**: Acts as the single entry point for all client requests.
- **Microservices**:
  - **Auth Service**: Handles user authentication and token management.
  - **User Service**: Manages user data and profiles.
  - **To-Do Service**: CRUD operations for tasks.
- **Database**: Each microservice has its own schema in **SQL Server**, ensuring data isolation and scalability.
- **Logging**: Centralized logging with **Serilog** using a shared library.
- **Hosting**: The application is hosted on **Azure**, leveraging its robust cloud capabilities.

---

## Technologies Used

- **Backend**: ASP.NET Core
- **Database**: SQL Server
- **Logging**: Serilog
- **Cloud Platform**: Azure
- **API Gateway**: Ocelot (or Azure API Management)
- **Containerization**: Docker (optional for microservices)

---

## Microservices

### Auth Service
Handles:
- User login and registration.
- Token generation (e.g., JWT).
- Token validation.

### User Service
Handles:
- User profile management.
- User metadata storage.

### To-Do Service
Handles:
- CRUD operations for to-do tasks.
- Task categorization and priorities.

---

## API Gateway

The **API Gateway** is responsible for:
- Routing client requests to the appropriate microservices.
- Managing cross-cutting concerns such as authentication and rate limiting.

---

## Logging

- **Serilog** is used for centralized logging.
- Logs are implemented in each microservice using a shared library.
- Features:
  - Logs are stored in a central location for debugging and monitoring.
  - Supports structured logging for better insights.

---

## Database

- **SQL Server** is used as the database for all microservices.
- Each microservice maintains its own schema to ensure data encapsulation.

---