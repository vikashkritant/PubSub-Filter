using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using MessageEvent;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PRCReceiverFunc
{
    public class PRCMessageHandler
    {
        private readonly ILogger<PRCMessageHandler> _logger;

        public PRCMessageHandler(ILogger<PRCMessageHandler> logger)
        {
            _logger = logger;
        }

        [Function(nameof(PRCMessageHandler))]
        public async Task Run(
            [ServiceBusTrigger("dot-archive-earliest-issuance-date-change", "dos-archive-earliest-issuance-date-change-prc", Connection = "ServiceBusConnectionString")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);

            var messageBody = Encoding.UTF8.GetString(message.Body);

            var data = JsonConvert.DeserializeObject<ReportIssuanceDateChangedEvent>(messageBody);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
