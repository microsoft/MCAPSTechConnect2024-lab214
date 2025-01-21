using Microsoft.Agents.Protocols.Adapter;
using Microsoft.Agents.Protocols.Primitives;
using TravelAgency.Agents;

namespace TravelAgency.Bots
{
    public class BasicBot: ActivityHandler
    {
        private TravelAgent _travelAgent;

        public BasicBot(TravelAgent travelAgent)
        {
            _travelAgent = travelAgent;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var response = await _travelAgent.InvokeAgentAsync(turnContext.Activity.Text);
            if (response == null)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Sorry, I couldn't get the travel suggestion at the moment."), cancellationToken);
                return;
            }

            var textResponse = MessageFactory.Text(response);

            await turnContext.SendActivityAsync(textResponse, cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            // When someone (or something) connects to the bot, a MembersAdded activity is received.
            // For this sample,  we treat this as a welcome event, and send a message saying hello.
            // For more details around the membership lifecycle, please see the lifecycle documentation.
            IActivity message = MessageFactory.Text("Hello and Welcome! I'm here to help with all your travel needs!");

            // Send the response message back to the user. 
            await turnContext.SendActivityAsync(message, cancellationToken);
        }
    }
}
