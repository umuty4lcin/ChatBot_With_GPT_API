using System;
using RestSharp;
using Newtonsoft.Json;

namespace ChatBot
{
    public class ChatGPTClient
    {
        private readonly string _apiKey;
        private readonly RestClient _client;

        public ChatGPTClient(string apiKey)
        {
            _apiKey = apiKey;
            _client = new RestClient("https://api.openai.com/v1/chat/completions");
        }

        public string SendMessage(string message)
        {
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            var requestBody = new
            {
                model = "gpt-3.5-turbo-1106",
                messages = new[]
                {
                        new { role = "system", content = "You are a helpful assistant." },
                        new { role = "user", content = message }
                    },
                max_tokens = 100,
                temperature = 0.7,
                n = 1
            };

            request.AddJsonBody(JsonConvert.SerializeObject(requestBody));

            var response = _client.Execute(request);

            if (response == null || !response.IsSuccessful)
            {
                return $"Error: Unable to get a valid response from the API. Status: {response?.StatusCode}, Message: {response?.ErrorMessage}";
            }

            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content ?? string.Empty);

            if (jsonResponse?.choices != null && jsonResponse.choices.Count > 0)
            {
                return jsonResponse.choices[0]?.message?.content?.ToString()?.Trim() ?? string.Empty;
            }

            return "Error: Invalid response structure.";
        }
    }
}

