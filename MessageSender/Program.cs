using Azure.Messaging.ServiceBus;
using MessageEvent;
using System.Text.Json;

namespace MessageSender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Message sender started!");
            string con = "Endpoint=sb://vikashasb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=vL6PoS+pw+zF6ZE4PQU+ipJh4KKOM4Ntw+ASbBrMRBw=";

            try
            {
                // Create a Service Bus client
                var client = new ServiceBusClient(con);

                // Create a sender for the topic
                var sender = client.CreateSender("dot-archive-earliest-issuance-date-change");

                //var reportIssuanceDateChangedEvent = new ReportIssuanceDateChangedEvent(Guid.NewGuid(), DateTime.Now, "123", "789","ReportIssuanceDateChangedEvent");
                var reportIssuanceDateChangedEvent = new ReportIssuanceDateChangedEvent(Guid.NewGuid(), DateTime.Now, EventType.ReportIssuanceDateChangedEvent);
                // Serialize to JSON string
                var jsonString = JsonSerializer.Serialize(reportIssuanceDateChangedEvent);

                Dictionary<string, object> messageProperties = new Dictionary<string, object>();
                messageProperties["EventType"] = EventType.ReportIssuanceDateChangedEvent;

                // Create a Service Bus message
                var message = new ServiceBusMessage(jsonString);

                message.ApplicationProperties["EventType"] = EventType.ReportIssuanceDateChangedEvent;
                // Send the message
                sender.SendMessageAsync(message);

                Console.WriteLine($"Message sent: {jsonString}");
                Console.ReadLine();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }

        }
    }
}
