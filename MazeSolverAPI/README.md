# Maze Solver API

This project is a Maze Solver API that provides endpoints to solve mazes using various algorithms. The API is built with ASP.NET Core and includes Swagger for API documentation.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or any other preferred IDE

## Setup Instructions

1. **Clone the repository**

git clone https://github.com/ruirodrigues1971/ParcelTechnicalAssignment

cd mazesolverapi


2. **Restore dependencies**

dotnet restore


3. **Build the project**

dotnet build

4. **Run the project**

dotnet run --project MazeSolverAPI


Alternatively, you can run the project from Visual Studio by opening the solution file (`MazeSolverAPI.sln`) and pressing `F5`.

## Running Tests

To run the unit tests, use the following command:

dotnet test

## API Documentation

The API documentation is available via Swagger. Once the project is running, navigate to the following URL in your browser:

https://localhost:5001/swagger


## Endpoints

### Solve Maze

- **URL:** `POST /api/MazeSolver`
- **Description:** Solves a maze using the specified algorithm.
- **Request Body:**  
        Example 1: (no path found)   
             "S_________\n_XXXXXXXX_\n_X______X_\nX_XXXX_X_\n_X_X__X_X_\n_X_X__X_X_\n_X_X____X_\n_X_XXXXXX_\n_X________\nXXXXXXXXG_"   
        Example 2: (has path)    
             "S_________\n_XXXXXXXX_\n_X______X_\nX_XXXX_X__\n_X_X__X_X_\n_X_X__X_X_\n_X_X____X_\n_X_XXXXXX_\n_X________\nXXXXXXXXG_"   
        
```
curl -X 'POST' \
  'https://localhost:7149/api/MazeSolver?algorithmStrategyEnum=BFSPathFinding' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '"S_________\n_XXXXXXXX_\n_X______X_\nX_XXXX_X__\n_X_X__X_X_\n_X_X__X_X_\n_X_X____X_\n_X_XXXXXX_\n_X________\nXXXXXXXXG_"'
```

- **Responses:**
  - `200 OK`: Returns the solution to the maze.
  - `400 Bad Request`: If the maze is invalid or the algorithm is not supported.
  - `500 Internal Server Error`: If there is an error while solving the maze.

### Get Previous Solutions

- **URL:** `GET /api/MazeSolver/GetPreviousSolutions`
- **Description:** Retrieves previous maze solutions from the cache.
- **Responses:**
  - `200 OK`: Returns a list of previous solutions.
  - `500 Internal Server Error`: If there is an error while retrieving the solutions.

 ```
 curl -X 'GET' \
  'https://localhost:7149/api/MazeSolver/GetPreviousSolutions' \
  -H 'accept: text/plain'
 ```

## Project Structure

- **MazeSolverAPI**: The main API project.
- **Application**: Contains the algorithm implementations and services.
- **Domain**: Contains the domain models and exceptions.
- **Technical AssignmentTest**: Contains the unit tests for the project.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

