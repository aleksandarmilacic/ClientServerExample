namespace ClientServerExample
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.Json;

    public class Client
    {
        private const string ServerIp = "127.0.0.1";
        private const int ServerPort = 8888;

        public static void Main(string[] args)
        {
            try
            {
                // Create a TcpClient and connect to the server
                using (var client = new TcpClient(ServerIp, ServerPort))
                {
                    Console.WriteLine("Connected to server.");

                    // Get the network stream
                    var stream = client.GetStream();

                    // Create an object to send to the server
                    var dataToSend = new SomeObject { Name = "John Doe", Age = 30 };

                    // Serialize the object to JSON
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var dataJson = JsonSerializer.Serialize(dataToSend, options);
                    var dataBytes = Encoding.UTF8.GetBytes(dataJson);

                    // Send the JSON payload to the server
                    stream.Write(dataBytes, 0, dataBytes.Length);

                    // Receive and process the server's response
                    var responseBytes = new byte[1024];
                    var bytesRead = stream.Read(responseBytes, 0, responseBytes.Length);
                    var responseJson = Encoding.UTF8.GetString(responseBytes, 0, bytesRead);
                    var response = JsonSerializer.Deserialize<string>(responseJson, options);
                    Console.WriteLine($"Response from server: {response}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
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