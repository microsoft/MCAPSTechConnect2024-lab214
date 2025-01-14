// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Agents.Protocols.Adapter;
using Microsoft.Agents.Protocols.Primitives;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot
{
    // This is the core handler for the Bot Message loop. Each new request will be processed by this class.
    public class MyBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            //respond to the user
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            //welcome the user
        }
    }
}