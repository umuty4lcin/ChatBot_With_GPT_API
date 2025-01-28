
namespace ChatBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("Error: API key is not set. Please set it as an environment variable.");
                return;
            }

            // Create a ChatGPTClient instance with the API key
            var chatGPTClient = new ChatGPTClient(apiKey);

            // Create a ChatGPTClient instance with the API key
            

            // Display a welcome message
            Console.WriteLine("Welcome to the ChatGPT chatbot! Type 'exit' to quit.");

            // Enter a loop to take user input and display chatbot responses
            while (true)
            {
                // Prompt the user for input
                Console.ForegroundColor = ConsoleColor.Green; // Set text color to green
                Console.Write("You: ");
                Console.ResetColor(); // Reset text color to default
                string input = Console.ReadLine() ?? string.Empty;

                // Exit the loop if the user types "exit"
                if (input.ToLower() == "exit")
                    break;

                // Validate user input
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please enter a valid message.");
                    Console.ResetColor();
                    continue;
                }

                try
                {
                    // Send the user's input to the ChatGPT API and receive a response
                    string response = chatGPTClient.SendMessage(input);

                    // Display the chatbot's response
                    Console.ForegroundColor = ConsoleColor.Blue; // Set text color to blue
                    Console.Write("Chatbot: ");
                    Console.ResetColor(); // Reset text color to default
                    Console.WriteLine(response);
                }
                catch (Exception ex)
                {
                    // Handle exceptions gracefully
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }
    }
}

