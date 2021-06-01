using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;
using FunctionAppProcessOrders.Data;
using FunctionAppProcessOrders.Models;
using FunctionAppProcessOrders.Validators;

namespace FunctionAppProcessOrders
{
    public static class ProcessOrders
    {
        [FunctionName("ProcessOrders")]
        public static void Run([KafkaTrigger(
            "Kafka_Broker", "orderevents",
            ConsumerGroup = "groffelocal",
            Protocol = BrokerProtocol.SaslSsl,
            AuthenticationMode = BrokerAuthenticationMode.Plain,
            Username = "Kafka_User",
            Password = "Kafka_Password"
            )]KafkaEventData<string> kafkaEvent,
            ILogger log)
        {
            var data = kafkaEvent.Value.ToString(); 
            log.LogInformation($"Dados recebidos: {data}");

            OrderData orderData = null;
            try
            {
                orderData = JsonSerializer.Deserialize<OrderData>(data,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch
            {
                log.LogError("Erro durante a deserializacao!");
            }

            if (orderData != null)
            {
                var validationResult = new OrderDataValidator().Validate(orderData);
                if (validationResult.IsValid)
                {
                    OrderRepository.Save(orderData);
                    log.LogInformation("Order registrada com sucesso!");
                }
                else
                {
                    log.LogError("Dados invalidos!");
                    foreach (var error in validationResult.Errors)
                        log.LogError($" ## {error.ErrorMessage}");
                }
            }
        }        
    }
}