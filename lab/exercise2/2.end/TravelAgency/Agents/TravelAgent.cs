using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using System.Text;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using TravelAgency.Plugins;

namespace TravelAgency.Agents
{
    public class TravelAgent
    {
        private readonly ChatHistory _chatHistory;
        private readonly ChatCompletionAgent _agent;

        private const string AgentName = "TravelAgent";
        private const string AgentInstructions = """
                You are a friendly assistant that helps people planning a trip.
                Your goal is to provide suggestions for a place to go based on the trip description of the user. 
                You have access to a tool that gives you a list of available places you can suggest. 
                You can suggest only a place which is included in this list.
                Make sure to include in the response also the current temperature in the locations you suggest.
                You have access to a tool that gives you the current temperature given its coordinate in latitude and longitude, so you'll need to retrieve first the coordinates of the city.
                """;    

        public TravelAgent(Kernel kernel)
        {
            this._chatHistory = [];

            // Define the agent
            this._agent =
                new()
                {
                    Instructions = AgentInstructions,
                    Name = AgentName,
                    Kernel = kernel,
                    Arguments = new KernelArguments(new OpenAIPromptExecutionSettings()
                    {
                        FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
                    })
                };

            // Give the agent some tools to work with
            this._agent.Kernel.ImportPluginFromType<DestinationsPlugin>();
            this._agent.Kernel.ImportPluginFromType<WeatherPlugin>();
        }

        /// <summary>
        /// Invokes the agent with the given input and returns the response.
        /// </summary>
        /// <param name="input">A message to process.</param>
        /// <returns>An instance of <see cref="WeatherForecastAgentResponse"/></returns>
        public async Task<string> InvokeAgentAsync(string input)
        {
            ChatMessageContent message = new(AuthorRole.User, input);
            this._chatHistory.Add(message);

            StringBuilder sb = new();
            await foreach (ChatMessageContent response in this._agent.InvokeAsync(this._chatHistory))
            {
                this._chatHistory.Add(response);
                sb.Append(response.Content);
            }

            return sb.ToString();
        }
    }
}
