using System;
using System.Xml;
using System.IO;
using Npgsql;
using System.Data.SqlClient;
using PostgreSQLCopyHelper;
/*about time
using System.Diagnostics;
using System.Threading;
*/
namespace file1
{
    public class functions
    {
        public static string link = "Host = [];Port = [];Username = [];Password = [];Database = brky_database2";
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

        public static ulong WriteToDatabase(List<functions.Product> entities)
        {
            PostgreSQLCopyHelper<functions.Product> copyHelper = new PostgreSQLCopyHelper<functions.Product>("public", "Product")
            //var copyHelper = new PostgreSQLCopyHelper<functions.Product>("Product")
                .MapVarchar("sku", x => x.sku)
                .MapVarchar("url", x => x.url)
                .MapVarchar("name1", x => x.name1)
                .MapVarchar("price", x => x.price)
                .MapVarchar("imgUrl", x => x.imgUrl)
                .MapVarchar("barcode", x => x.barcode)
                .MapVarchar("shipPrice", x => x.shipPrice)
                .MapVarchar("description", x => x.description)
                .MapVarchar("productBrand", x => x.productBrand)
                .MapVarchar("dayOfDelivery", x => x.dayOfDelivery)
                .MapVarchar("productCategory", x => x.productCategory);
            using (var connection = new NpgsqlConnection(link))
            {
                connection.Open();
                // Returns count of rows written 
                ulong count = copyHelper.SaveAll(connection, entities);
                entities.Clear();
                System.Console.WriteLine("50k sent");
                return count;
            }
        }

        public static void send_50k_func(List<Product> entities)
        {

            if (entities.Count == 50000)
            {
                WriteToDatabase(entities);
            }
        }

    }
}
