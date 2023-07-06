namespace ClientServerExample.Server
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class Server
    {
        private const int Port = 8888;

        public static async Task Main(string[] args)
        {
            try
            {
                var ipAddress = IPAddress.Parse("127.0.0.1");
                var listener = new TcpListener(ipAddress, Port);
                listener.Start();
                Console.WriteLine($"Server started on port {Port}.");

                while (true)
                {
                    Console.WriteLine("Waiting for client connection...");
                    var client = await listener.AcceptTcpClientAsync();

                    Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

                    // Handle client connection asynchronously
                    _ = HandleClientAsync(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                var stream = client.GetStream();

                // Receive the JSON payload from the client
                var buffer = new byte[1024];
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                var receivedJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Deserialize the JSON into an object
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var receivedObject = JsonSerializer.Deserialize<SomeObject>(receivedJson, options);

                // Process the received object
                Console.WriteLine($"Received object from client {client.Client.RemoteEndPoint}: {receivedObject}");

                // Send a response back to the client
                var response = "Object received on the server.";
                var responseJson = JsonSerializer.Serialize(response);
                var responseBytes = Encoding.UTF8.GetBytes(responseJson);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred with client {client.Client.RemoteEndPoint}: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }
    }
    public class SomeObject
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }
}