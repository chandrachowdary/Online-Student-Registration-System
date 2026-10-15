# Student Registration System

A C# Windows Forms desktop application for managing student registration records with Microsoft SQL Server.

## Technologies

- C#
- .NET Framework 4.8.1
- Windows Forms
- Microsoft SQL Server
- ADO.NET / `System.Data.SqlClient`

## Features

- Add student records
- View student records in a data grid
- Edit existing records
- Delete records with confirmation
- Input validation for required fields and fees
- Parameterised SQL commands for database operations
- Database connection configuration kept outside the source code

## Project structure

- `StudentRegistrationForm.cs` — application logic and database operations
- `StudentRegistrationForm.Designer.cs` — Windows Forms UI layout
- `Program.cs` — application entry point
- `App.config` — local database connection configuration (contains a placeholder only)
- `database/schema.sql` — SQL Server table definition

## Setup

1. Install Visual Studio with the .NET Framework desktop development workload.
2. Open `online student registration system.csproj`.
3. Create or select a SQL Server database named `Osrs`.
4. Run `database/schema.sql` in SQL Server Management Studio.
5. Update `StudentDb` in `App.config` with your own local SQL Server connection details.
6. Build and run the application.

> **Security note:** This repository intentionally contains no real database username, password, or client credentials. Do not commit secrets to a public repository.

## Portfolio note

This is a personal/public portfolio project demonstrating C#, Windows Forms, SQL Server and CRUD application development. Professional client projects cannot be published when they contain confidential or proprietary information.
