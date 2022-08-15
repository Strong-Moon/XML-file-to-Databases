using System;
using System.Text;
using System.Xml;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace file1
{
    public class functions
    {
        public static string link = "Host = [];Port = [];Username = [];Password = []!;Database = brky_database";

        [BsonIgnoreExtraElements] // search for this!!
        public class Product
        {
            public string? sku { get; set; }
            public string? url { get; set; }
            public string? name1 { get; set; }
            public string? price { get; set; }
            public string? imgUrl { get; set; }
            public string? barcode { get; set; }
            public string? shipPrice { get; set; }
            public string? description { get; set; }
            public string? productBrand { get; set; }
            public string? dayOfDelivery { get; set; }
            public string? productCategory { get; set; }
            public Product() { }
        }
        public static void WriteToDatabase(List<Product> list)
        {   
            var client = new MongoClient("mongodb://id:[]!@host:post");
            var database = client.GetDatabase("brky_database");
            var collec_data = database.GetCollection<Product>("Product");
            collec_data.InsertMany(list);
            System.Console.WriteLine(list.Count());
            list.Clear();
        }
        public static void send_50k_func(List<functions.Product> entities)
        {
            if (entities.Count == 50000)
            {
                WriteToDatabase(entities);
            }
        }
        public static void sendCommand()
        {

            MongoClient client = new MongoClient("mongodb://id:password!@host:post");
            var db = client.GetDatabase("brky_database");
            IMongoCollection<Product> source_data = db.GetCollection<Product>("Product");
            IMongoCollection<Product> target_data = db.GetCollection<Product>("Product2"); 
            //var distinctWords = source_data.AsQueryable().Select(e => e.sku).Distinct();
            List<string> distinctSkus = source_data.Distinct<string>("sku", FilterDefinition<Product>.Empty).ToList();
            target_data.DeleteMany(X => distinctSkus.Contains(X.sku!));
            target_data.InsertMany(source_data.AsQueryable());
            source_data.DeleteMany(X => true);
            
        }
    }
}