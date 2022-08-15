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
        public static string link = "Host = [];Port = []];Username = [];Password = [];Database = []";


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
         public static Thread StartTheThread(List<functions.Product> entities)
        {
            var t = new Thread(() => WriteToDatabase(entities));
            t.Start();
            return t;
        }
        public static void WriteToDatabase(List<Product> list)
        {    
            var client = new MongoClient("mongodb://id:password@host:port");
            var database = client.GetDatabase("brky_database");
            var collec_data = database.GetCollection<Product>("Product");
            System.Console.WriteLine(list.Count());
            collec_data.InsertMany(list);
            list.Clear();
        }
         public static void StartTheThread2(List<functions.Product> entities)
        {
            var th = new Thread(() => WriteToDatabase(entities));
            th.Start();
        }
        public static void send_50k_func(List<functions.Product> entities)
        {
            if (entities.Count == 50000)
            {
                WriteToDatabase(entities);

            }
        }
    }
}