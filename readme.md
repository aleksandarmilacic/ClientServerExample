# Client-Server Application

This is a simple client-server application that demonstrates communication between a client and a server using TCP sockets. The client sends an object to the server, and the server processes the object and sends a response back to the client.

## Features

- Client application sends an object to the server using TCP sockets.
- Server application receives the object, processes it, and sends a response back to the client.
- Object serialization and deserialization using JSON.

## Prerequisites

- .NET 5.0 or later

## How to Use

### Server

1. Open a terminal and navigate to the `Server` directory.
2. Build the project: `dotnet build`.
3. Run the server: `dotnet run`.
4. The server will start listening for client connections on port 8888.

### Client

1. Open a terminal and navigate to the `Client` directory.
2. Build the project: `dotnet build`.
3. Run the client: `dotnet run`.
4. The client will connect to the server and send an object.
5. The client will display the response received from the server.

## Customization

- Modify the object structure in the `SomeObject` class in both the server and client projects to suit your application's requirements.
- Adjust the server IP address and port number in the `ServerIp` and `ServerPort` constants in the client project to match your server configuration.

## License

This project is licensed under the [MIT License](LICENSE).
