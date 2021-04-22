using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public static class CSVreader
    {
        private static int _counter = 1;
        public static string[] GetFiles()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string path = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, "ItemLists");
            string[] files = Directory.GetFiles(path);

            return files;
        }

        public static List<Product> ReadFromFile(string path)
        {
            List<Product> newList = new List<Product>();
            using(StreamReader file = new StreamReader(@path))
            {
                string headerLine = file.ReadLine();
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    var elements = line.Split(',');

                    Product item = GetProductType(elements[5]);

                    item.Id = _counter;
                    item.Name = elements[1];
                    item.Volume = int.Parse(elements[2]);
                    item.Price = double.Parse(elements[3]);
                    item.Amount = int.Parse(elements[4]);
                    item.Type = elements[5];

                    newList.Add(item);
                    _counter++;
                }
            }

            return newList;
        }

        public static Product GetProductType(string type)
        {
            Product repository = null;

            switch (type)
            {
                case "drink": repository = new Drink();
                    break;
                case "candy":
                    repository = new Candy();
                    break;
                case "food":
                    repository = new Food();
                    break;
                default:
                    throw new ArgumentException("Invalid type");
            }
            return repository;
        }
    }
}
