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
        public static string link = "Host = [];Port = [];Username = [];Password = []!;Database = brky_database; Timeout=300; CommandTimeout=300";
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
                System.Console.WriteLine(entities.Count());
                entities.Clear();
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

        public static void sendCommand(string source, string target)
        {
            
            NpgsqlConnection connection = new NpgsqlConnection(link);
            connection.Open();

            NpgsqlCommand cmd = new NpgsqlCommand(@$"Delete FROM {target} where sku in (select distinct sku from {source});", connection);
            
            NpgsqlCommand cmd2 = new NpgsqlCommand(@$"INSERT INTO {target} SELECT * FROM {source};", connection);

            NpgsqlCommand cmd3 = new NpgsqlCommand(@$"Delete FROM {target};",connection);
                                                /*(@$"Delete FROM {target}
                                                  where sku in (select sku from {target});", connection);
                                                  */
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            connection.Close();
        }
        

    }
}
