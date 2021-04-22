using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
     
    class VendingMachine
    {
        public int[] cashValues = new int[8]
        {
            1000,
            500,
            100,
            50,
            20,
            10,
            5,
            1
        };
        
        public double MoneyPool { get; private set; }

        public void Deposit(int money)
        {
            MoneyPool += money;
        }

        private Dictionary<int, int> _returnCash = new Dictionary<int, int>();

        public Dictionary<int, int> Withdraw()
        {
            _returnCash.Clear();
            if (MoneyPool <= 0)
            {
                Console.WriteLine("You didn't have any money deposited");
                foreach (var value in cashValues)
                {
                    _returnCash.Add(value, 0);
                }
                return _returnCash;
            }

            for (int i= 0; i < cashValues.Length; i++ )
            {
                MoneyPool = Math.Floor(MoneyPool);
                int amount = Convert.ToInt32(MoneyPool) / cashValues[i];
                MoneyPool =(amount != 0) ? MoneyPool % (amount*cashValues[i]) : MoneyPool;
                _returnCash.Add(cashValues[i], amount);
            }

            

            MoneyPool = 0;
            Console.WriteLine("You recieved:");
            foreach (var cash in _returnCash)
            {
                if (cash.Value != 0) Console.WriteLine($"{cash.Value} {cash.Key}kr {((cash.Key >= 20) ? "bills" : "coins")}");
            }
            Console.WriteLine("Which will be added to your wallet.");
            Console.ReadKey();
            return _returnCash;
        }
        
        public void PrintMoneyList(Dictionary<int,int> moneyList)
        {
            Console.WriteLine("You have:");
            foreach (var value in moneyList)
            {
                Console.WriteLine($"{value.Value} {value.Key}kr {((value.Key >= 20) ? "bills" : "coins")}");

            }
        }

        public void PurchaseProduct(ref Product chosenProduct)
        {
            if (chosenProduct.Amount <= 0)
            {
                Console.WriteLine($"There were not enough {chosenProduct.Name} left to buy");
                Console.ReadKey();
                return;
            }
            if (MoneyPool < chosenProduct.Price)
            {
                Console.WriteLine("You don't have enough money. Deposit more.");
                Console.ReadKey();
                return;
            }

            MoneyPool -= chosenProduct.Price;
            chosenProduct.Amount -= 1;
            ProductList.AddToOwnedProducts(chosenProduct);
            
            Console.WriteLine($"One {chosenProduct.Name} was bought.");
            Console.ReadKey();

        }


    }
}



