using System;
using MongoDB.Bson;
using FunctionAppProcessOrders.Models;

namespace FunctionAppProcessOrders.Documents
{
    public class OrderDocument
    {
        public ObjectId _id { get; set; }
        public DateTime ProcessingDate { get; set; }
        public OrderData Data { get; set; }
    }
}