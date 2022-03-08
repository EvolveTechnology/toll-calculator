# Toll Fee Calculator 1.0

## Background

This codebase will represent the microservice implementation of the toll calculation. Mediator pattern and dependency injection are used in the service implementation to achieve simplicity, de-coupling, and testability.

### Used dependencies

Below dependencies are used in project

```
FluentValidation - 10.3.6
MediatR - 10.0.1
xUnit - 2.4.1
```

### Running the API

Use docker to build and run the API

```
docker build -f Evolve.TollCalculator.API\Dockerfile -t tollfeecalculatorapi .

docker run -p 5000:80 -t tollfeecalculatorapi
```
Then import the postman collection file ```tollcalculation.postman_collection.json``` and execute the APIs.
