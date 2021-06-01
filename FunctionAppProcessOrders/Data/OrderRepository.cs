using System;
using MongoDB.Driver;
using FunctionAppProcessOrders.Documents;
using FunctionAppProcessOrders.Models;

namespace FunctionAppProcessOrders.Data
{
    public static class OrderRepository
    {
        public static void Save(OrderData orderData)
        {
            var client = new MongoClient(
                Environment.GetEnvironmentVariable("MongoDB_Connection"));
            var db = client.GetDatabase(
                Environment.GetEnvironmentVariable("MongoDB_Database"));
            var history = db.GetCollection<OrderDocument>(
                Environment.GetEnvironmentVariable("MongoDB_Collection"));
            history.InsertOne(new OrderDocument()            
            {
                ProcessingDate = DateTime.Now,
                Data = orderData
            });
        }
    }
}