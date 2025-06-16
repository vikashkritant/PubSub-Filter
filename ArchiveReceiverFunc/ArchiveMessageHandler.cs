using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using MessageEvent;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ArchiveReceiverFunc
{
    public class ArchiveMessageHandler
    {
        private readonly ILogger<ArchiveMessageHandler> _logger;

        public ArchiveMessageHandler(ILogger<ArchiveMessageHandler> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ArchiveMessageHandler))]
        public async Task Run(
            [ServiceBusTrigger("dot-archive-earliest-issuance-date-change", "dos-archive-earliest-issuance-date-change-archive", Connection = "ServiceBusConnectionString")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            
            var messageBody= Encoding.UTF8.GetString(message.Body);

            var data = JsonConvert.DeserializeObject<ReportIssuanceDateChangedEvent>(messageBody);

             // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
