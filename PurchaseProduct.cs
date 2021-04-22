using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    static class PurchaseProduct
    {
        public static void GiveInfo(Product chosenProduct)
        {
            Console.Clear();
            Console.WriteLine($"{"Name",6} : {chosenProduct.Name,11}");
            Console.WriteLine($"{"Price",6} : {chosenProduct.Price,9}kr");
            Console.WriteLine($"{"Volume",6} : {chosenProduct.DisplayVolume(),11}");
            Console.WriteLine($"{"Amount",6} : {chosenProduct.Amount,11}");


        }

        
    }
}
