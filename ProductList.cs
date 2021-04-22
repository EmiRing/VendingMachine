using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public static class ProductList
    {
        public static List<Product> productList = new List<Product>();
        public static List<Product> GenerateProducts() 
        {
            string[] files=CSVreader.GetFiles();

            foreach (var file in files)
            {
                productList.AddRange(CSVreader.ReadFromFile(file));
            }

            return productList;
        }
        public static List<Product> ownedProducts = new List<Product>();
        private static int _counter = 1;
        public static void AddToOwnedProducts(Product product)
        {
            if(ownedProducts.Exists(x => x.Name == product.Name))
            {
                ownedProducts.Find(x => x.Name == product.Name).Amount += 1;
            }
            else
            {
                Product p = product.ShallowCopy();
                ownedProducts.Add(p);
                ownedProducts.Find(x => x.Name == product.Name).Amount = 1;
                ownedProducts.Find(x => x.Name == product.Name).Id = _counter;
                _counter++;
            }
            
        }

        public static List<Product> GetOwnedProducts()
        {
            return ownedProducts;
        }

        public static void PresentOwnedProduct(Product ownedProduct)
        {
            Console.Clear();
            Console.WriteLine("--------------------");
            Console.WriteLine($"{"Name",6} : {ownedProduct.Name,11}");
            Console.WriteLine($"{"Volume",6} : {ownedProduct.DisplayVolume(),11}");
            Console.WriteLine($"{"Amount",6} : {ownedProduct.Amount,11}");
            Console.WriteLine("--------------------");
        }
    }
}
