using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using System.IO;
using System.Data.SqlClient;
using PostgreSQLCopyHelper;

namespace file1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            // Start with XmlReader object  
            //here, we try to setup Stream between the XML file nad xmlReader  
            using (XmlReader reader = XmlReader.Create("C:\\Users\\BerkayPehlivan\\Desktop\\users.xml"))
            {
                List<Thread> threads = new List<Thread>();
                List<functions.Product> entities = new List<functions.Product>();
                functions.Product tempProduct = new functions.Product();
                while (reader.Read())
                {

                    if (reader.IsStartElement())
                    {

                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {

                            case "sku":
                                tempProduct.sku = reader.ReadString();
                                break;
                            case "url":
                                tempProduct.url = reader.ReadString();
                                break;
                            case "name":
                                tempProduct.name1 = reader.ReadString();
                                break;
                            case "price":
                                tempProduct.price = reader.ReadString();
                                break;
                            case "imgUrl":
                                tempProduct.imgUrl = reader.ReadString();
                                break;
                            case "barcode":
                                tempProduct.barcode = reader.ReadString();
                                break;
                            case "shipPrice":
                                tempProduct.shipPrice = reader.ReadString();
                                break;
                            case "description":
                                tempProduct.description = reader.ReadString();
                                break;
                            case "productBrand":
                                tempProduct.productBrand = reader.ReadString();
                                break;
                            case "dayOfDelivery":
                                tempProduct.dayOfDelivery = reader.ReadString();
                                break;
                            case "productCategory":
                                tempProduct.productCategory = reader.ReadString();
                                break;
                            case "product":
                                entities.Add(tempProduct);
                                if (entities.Count == 50000)
                                {
                                    threads.Add(functions.StartTheThread(entities.ToList()));
                                    entities.Clear();
                                }
                                break;
                        }
                    }

                }
                System.Console.WriteLine(entities.Count());
                if (entities.Count != 0)
                {
                    threads.Add(functions.StartTheThread(entities));
                    entities.Clear();
                }
                foreach(Thread t in threads){
                t.Join();
            }
            }
            DateTime end = DateTime.Now;
            TimeSpan ts = (end - start);
            Console.WriteLine("Elapsed Time is {0}m-{1}s-{2}ms", ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
    }
}