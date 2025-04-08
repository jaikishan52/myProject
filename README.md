# University Employee Receipt Reimbursement App

This is a full-->stack web application that allows university employees to submit receipts for reimbursement. The application is designed for internal use within the university system.

--> Tech Stack

--> Backend: .NET 8 Web API with C#
--> Frontend: Angular 17 (Standalone Application)
--> Database: SQL Server (running in Docker container)
--> ORM: Entity Framework Core
--> Tools: Azure Data Studio for database management, Docker

-->Features

--> Submit receipt forms with:
  --> University email verification
  --> Purchase date, amount, description, and file upload
--> File uploads stored locally in `UploadedReceipts` folder
--> Data persisted in SQL Server database
--> Validations at both backend and frontend
--> CORS configured for seamless local development
--> Clean code structure, ready for extension (admin page, login, etc.)

--> How to Run

--> Backend (.NET API)

1. Navigate to the backend folder:
  
   cd Backend

Make sure Docker is running, and SQL Server container is up:

   docker start sqlserver

Run the backend API:

   dotnet run

Frontend (Angular)
Navigate to the frontend folder:


   cd frontend

Install dependencies:

   npm install

Run the Angular app:


  ng serve

Open in browser:

http://localhost:4200

Database Access
Open Azure Data Studio

Connect to:

Server: localhost,1433

User: sa

Password: Mypassword@52

Query database:

sql

SELECT TOP (1000) [Id]
      ,[Date]
      ,[Amount]
      ,[Description]
      ,[FilePath]
      ,[EmployeeEmail]
  FROM [UniversityReceiptsDb].[dbo].[Receipts]


 Time Estimate & Actuals

 I originally estimated around 7 hours to complete the project, including setting up the backend API, connecting to the database, building the Angular frontend form, and basic testing. 
 However, the actual time taken was approximately 8.5 hours. The slight increase was due to resolving unexpected issues like Angular standalone app configuration, 
 CORS errors between frontend and backend, and deeper debugging to ensure validations happen before file/database operations. 
 Overall, the extra time allowed me to refine the code structure and deliver a more polished, reliable solution.

Reasons for Choosing Tech Stack
--> .NET Core is powerful, performant, and integrates well with SQL Server, making it perfect for enterprise/internal tools.

--> Angular 17 (Standalone app) is modern, scalable, and has strong community support. Perfect for form-heavy internal applications.

--> SQL Server in Docker for easy local development and portability.

--> Azure Data Studio for easy query execution and database monitoring.

--> Docker to run SQL Server without complicated local installation.


Assumptions
--> Application is strictly internal, only university employees can access and submit receipts.

--> File uploads are stored on the local server, no external storage services are configured.

--> Database is accessed via Docker container for easy development setup.


   
