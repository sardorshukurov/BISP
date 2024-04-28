# Final project

## Description

This project is composed of multiple services running in Docker containers. It includes an API, a client application, a PostgreSQL database, and Seq for logging.

## Prerequisites

Make sure you have Docker installed on your system. You can download and install Docker from [here](https://www.docker.com/get-started).

## Setup

1. Clone this repository to your local machine.

2. Navigate to the project directory.

3. Run the following command to build the Docker images:

   ```bash
   docker-compose build

4. After the build completes, start the containers with the following command:

   ```bash
   docker-compose up

5. Once the containers are up and running, you can access the services at the following ports:
    PostgreSQL Database: localhost:45432
    Seq for logging: localhost:8081
    API: localhost:10000
    Client: localhost:10001 

## Usage
    Access the API endpoints by sending HTTP requests to localhost:10000.
    Access the client application by visiting localhost:10001 in your web browser.
    Use the provided ports to access the PostgreSQL database and Seq for logging.

## Notes
    Make sure no other services are running on the specified ports to avoid conflicts.
    You can customize the ports and other configurations in the docker-compose.yml file according to your requirements.