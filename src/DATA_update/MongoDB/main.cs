using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace file1
{
    class Program
    {
        static void Main(string[] args)
        {
            Main2();
            functions.sendCommand();
        }

        static void Main2()
        {
            DateTime start = DateTime.Now;
            // Start with XmlReader object  
            //here, we try to setup Stream between the XML file nad xmlReader  
            using (XmlReader reader = XmlReader.Create("C:\\Users\\Berkay Pehlivan\\Desktop\\user.xml"))
            {
                List<functions.Product> entities = new List<functions.Product>();
                functions.Product tempProduct = new functions.Product();
                bool isFirst = true;
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
                                if(isFirst){
                                    isFirst = false;
                                    continue;
                                }
                                entities.Add(tempProduct);
                                tempProduct = new functions.Product();
                                functions.send_50k_func(entities);
                                break;
                        }
                    }  
                }
                System.Console.WriteLine(entities.Count());
                    if (entities.Count != 0)
                    {
                        functions.WriteToDatabase(entities);
                    }
            }
            DateTime end = DateTime.Now;
            TimeSpan ts = (end - start);
            Console.WriteLine("Elapsed Time is {0}m-{1}s-{2}ms", ts.Minutes,ts.Seconds,ts.Milliseconds);
        }
    }
}