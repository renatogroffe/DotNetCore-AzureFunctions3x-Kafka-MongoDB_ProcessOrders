using System;

namespace FunctionAppProcessOrders.Models
{
    public class OrderData
    {
        public string CorrelationId { get; set; }
        public DateTime? When { get; set; }
        public Payload Payload { get; set; }
    }

    public class Payload
    {
        public string CustomerName { get; set; }
        public decimal? Total { get; set; }
        public string ID { get; set; }
    }
}