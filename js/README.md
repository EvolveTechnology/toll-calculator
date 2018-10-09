# Getting started

To install dependencies: 

`docker run --rm --mount "type=bind,src=$(pwd),dst=/app" node:latest npm install`

To run test in watch mode:

`docker-compose up`

## Issues
Docker for mac can be a bit slow with mounts.. :(

