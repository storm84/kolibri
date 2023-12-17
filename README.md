# Kolibri

A simple web API that is used to store and retrieve package information.

The API is built using .net 8

<br>

### Api

| Method | Endpoint                  | Description            | Sample JSON input                                                                            |
|--------|---------------------------|------------------------|----------------------------------------------------------------------------------------------|
| GET    | /api/v1/package           | Get all packages       |                                                                                              |
| GET    | /api/v1/package/{kolliId} | Get package by kolliId |                                                                                              |
| POST   | /api/v1/package           | Create package         | {"kolliId": "999256342102212197", "weight": 20000, "length": 60, "height": 60, "width": 60 } |

### Test data

The following packages can be found in the application on startup and can be retrieved by using any of the GET methods.
The IsValid flag tells if the package dimensions are valid or not.

| KolliId            | IsValid |
|--------------------|---------|
| 999111111111111111 | true    |
| 999222222222222222 | false   |

## Development

### Build and run the application locally

To build and run the application you need to have .net 8 installed on your machine.
In Development mode the application runs `swagger` and it is found at `/swagger/index.html`

To build and run the application execute the following command in your terminal

```sh
dotnet run --project src/Kolibri.Api
```

*execute the command from the root directory*

### Test

For Unit and Integration testing `xunit` is used together with the `FakeItEasy` and `AutoFixture` to simplify writing
tests and creating test data.

To run the tests run the following command in your terminal

```sh
dotnet test
```

*execute the command from the root directory*

## Docker

### Build

run the following command in your terminal to build the docker image

```sh
docker build -t kolibri -f Dockerfile .
```

*execute the command from the root directory*

### Run

run the following command in your terminal to run the docker image.
You should then be able to reach the api on `localhost:5001/api/v1/package`.

```sh
docker run -it --rm -p 5001:8080 kolibri
```
