# To-Do App

A simple to-do application built with Blazor WebAssembly, .NET 8 API, and MongoDB. This project allows users to create, update, and manage their To-Do list.

## Tech Stack

- **Frontend**: Blazor WebAssembly
- **Backend**: .NET 8 API
- **Database**: MongoDB

## Features

- **Add Tasks**: Create new tasks with title and description.
- **Edit Tasks**: Update task details.
- **Delete Tasks**: Remove tasks from the list.
- **Task Completion**: Mark tasks as completed or pending.
- **Filter Tasks by Status**: Mark tasks as completed or pending.
- **Responsive Design**: Works on various devices and screen sizes.

## Getting Started

### Prerequisites

Before you begin, ensure you have met the following requirements:

- .NET 8 SDK
- Docker

### Installation

1. **Pull and run MongoDb in a docker container**
   ```sh
   docker pull mongo
   docker run -d --name c-todo-app-mongo-db -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=myadmin -e MONGO_INITDB_ROOT_PASSWORD=mypassword mongo

2. **Clone the repository**

   ```sh
   git clone https://github.com/ElMehdiSmiri/TodoApp.git
   cd .\TodoApp\

3. **Restore and build the solution**
   ```sh
   dotnet build --configuration Release

4. **Run the Api**
   ```sh
   dotnet run --project .\TodoApp.Api\TodoApp.Api.csproj --launch-profile "https" --configuration Release

5. **Open a new cmd window in the root of the solution and run the web project**
   ```sh
   dotnet run --project .\TodoApp.Web\TodoApp.Web.csproj --launch-profile "https" --configuration Release

6. **Navigate to** https://localhost:7229/

### Tests
**To run the tests excute the command from the root of the solution**
   ```sh
   dotnet test
   ```
## LICENSE
This project is licensed under the MIT License - see the LICENSE file for details.
