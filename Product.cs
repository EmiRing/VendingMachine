using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public abstract class Product
    {
        private double _price;

        public double Price
        {
            get
            {
                if (OnSale) return _price * 0.75;
                return _price;
            }
            set { _price = value; }
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public int Amount { get; set; }

        public int Volume { get; set; }

        public string Type { get; set; }

        public bool OnSale { get; set; }

        public abstract void Consume();

        public abstract string DisplayVolume();

        //public void ShowProduct()
        //{
        //    if (productList.Exists(x => x.Id == value))
        //    {
        //        var product = productList.Find(product => product.Id == value);

        //        PurchaseProduct.GiveInfo(product);
        //        Console.Write("Purchase? (y/n)");
        //        string purchase = Console.ReadLine();
        //        if (purchase.ToLower() == "y") vm.PurchaseProduct(ref product);
        //    }
        //    else if (aNumber == false) break;
        //}
        public Product ShallowCopy()
        {
            return (Product)this.MemberwiseClone();
        }
    }
}
