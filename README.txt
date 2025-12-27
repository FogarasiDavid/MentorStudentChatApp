Mentor–Diák Platform API

Backend rendszer mentor–diák kapcsolatok kezelésére, valós idejű chattel és értesítésekkel.

Fő funkciók

Mentor–Diák kapcsolatok kezelése (roles + jogosultságok)

Valós idejű chat SignalR-rel

Értesítési rendszer (real-time + késleltetett)

JWT alapú autentikáció és role-kezelés

CQRS + Clean Architecture + DDD szemlélet

Tech stack

ASP.NET Core Web API

CQRS (MediatR)

SignalR (real-time chat & notifications)

Entity Framework Core

JWT Authentication

FluentValidation

Docker + Docker Compose

Swagger

Architektúra
Api
 ├── Controllers       
 ├── Hubs              
 └── Middleware         

Application
 ├── Commands / Queries
 ├── Handlers
 ├── Behaviors
 └── Interfaces

Domain
 ├── Entities           
 ├── ValueObjects      
 └── Aggregates

Infrastructure
 ├── Persistence
 ├── Repositories
 ├── Security(JWT)
 └── Background 

Chat működés

REST API: régi üzenetek lekérése

SignalR: új üzenetek valós időben

Üzenetküldés → Domain Event → Notification + SignalR push

Auth & Roles

Mentor:

látja diákjait

írhat üzenetet

Diák:

csak a saját mentorával kommunikálhat

Futtatás
docker-compose up


Swagger:
/swagger

Tesztelés

Domain és Application réteg unit tesztekkel lefedve

Repositoryk mockolva

Üzleti szabályok explicit tesztelve