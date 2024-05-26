# Person and Favorite Color Management REST API

This project is a RESTful web API for managing persons and their favorite colors. 
The API is designed to read data from a CSV file and provide endpoints to retrieve and manage this data. It is implemented using .NET (C#) and follows best practices for dependency injection and unit testing.

## Features

- **Manage persons and their favorite colors**: Add, retrieve, and manage data on persons and their favorite colors.
- **Read data from a CSV file**: The API reads person data from a CSV file without modifying the source file.
- **Identify persons by favorite color**: Retrieve all persons with a specific favorite color.

## Configuration

The location of the CSV file and the option to enable database usage should be defined in the `appsettings.json` file. Here is an example configuration:

```json
{
  "ResourceOptions": {
    "IsDataBaseEnable": false,
    "Path": "D:\\sample-input.csv"
  }
}
```

## Endpoints

The API provides the following endpoints:

### **GET** /persons

Retrieve a list of all persons.

#### Response
```json
[
    {
        "id": 1,
        "name": "Hans",
        "lastname": "Müller",
        "zipcode": "67742",
        "city": "Lauterecken",
        "color": "blau"
    },
    {
        "id": 2,
        ...
    }
]
```

### **GET** /persons/{id}

Retrieve a person by their ID. The ID corresponds to the line number in the CSV file.

#### Response
```json
{
    "id": 1,
    "name": "Hans",
    "lastname": "Müller",
    "zipcode": "67742",
    "city": "Lauterecken",
    "color": "blau"
}
```

### **GET** /persons/color/{color}

Retrieve all persons with the specified favorite color.

#### Response
```
[
    {
        "id": 1,
        "name": "Hans",
        "lastname": "Müller",
        "zipcode": "67742",
        "city": "Lauterecken",
        "color": "blau"
    },
    {
        "id": 2,
        ...
    }
]
```

### **POST** /persons

Add a new persons.


## Project Structure

- **Presentation/Presentation.API** : Contains the API controllers that handle HTTP requests and responses.
- **Domain/Entities** : Defines the data models used in the application.
- **Domain/Contracts** : Defines the repository interfaces.
- **Infrastructure/Repository** : Handles data access and abstracts the source of the data (CSV file in this case).
- **Service/Service.Contract** : Defines the service interfaces.
- **Service/Service** : Contains the business logic for managing persons and their favorite colors.
- **Service/Shared** : Defines the Data Transfer Objects.
- **FavouriteColour** : Configures services and repository dependencies and middlewares for the application.
- **Tests** : Contains tests to ensure the correctness of the API endpoints and business logic.

## Implementation Details
### CSV File Handling
The data is read from a sample-input.csv file, which includes person details and their favorite colors. The first column of the CSV corresponds to a person's favorite color as per the following mapping:

| ID | Farbe |
| --- | --- |
| 1 | blau |
| 2 | grün |
| 3 | violett |
| 4 | rot |
| 5 | gelb |
| 6 | türkis |
| 7 | weiß |


## Setup and Installation

### Prerequisites
- .NET SDK
- Any required database (if using database option)

### Running Locally
1. Clone the project:
   ```bash
   git clone https://github.com/behrouznd/Assecor.FavouriteColour.git
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Update appsettings.json with the correct path for your CSV file:
   ```json
   "ResourceOptions": {
      "IsDataBaseEnable": false,
      "Path": "D:\\sample-input.csv"
    }
   ```
4. Run the application:
   ```bash
    dotnet run
   ```

### Example Requests
#### GET /persons
```bash
curl -X GET "https://yourapiurl/persons" -H "accept: application/json"
```
Example Response
```json
[
    {
        "id": 1,
        "name": "Hans",
        "lastname": "Müller",
        "zipcode": "67742",
        "city": "Lauterecken",
        "color": "blau"
    }
]
```
#### POST /persons
```bash
curl -X POST "https://yourapiurl/persons" -H "accept: application/json" -H "Content-Type: application/json" -d "{\"name\":\"John\",\"lastname\":\"Doe\",\"zipcode\":\"12345\",\"city\":\"SampleCity\",\"color\":\"rot\"}"
```
Example Response
```json
{
    "id": 3,
    "name": "John",
    "lastname": "Doe",
    "zipcode": "12345",
    "city": "SampleCity",
    "color": "rot"
}
```
