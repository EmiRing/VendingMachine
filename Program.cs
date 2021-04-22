using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();
            List<Product> productList = ProductList.GenerateProducts();
            Random rnd = new Random();
            Dictionary<int, int> moneyAtHand =new Dictionary<int, int>();
            
            for (int i=0; i<vm.cashValues.Length; i++)
            {
                moneyAtHand.Add(vm.cashValues[i], rnd.Next(1, 10));

            }

            bool menuActive = true;
            while (menuActive) {
                Console.Clear();
                WelcomeMenu();
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": 
                        vm.Deposit(DepositMenu(moneyAtHand));
                        break;
                    case "2": 
                        Console.WriteLine($"You have {vm.MoneyPool}kr to shop for.");
                        Console.ReadKey();
                        break;
                    case "3": 
                        ShopMenu(vm, ref productList);
                        break;
                    case "4":
                        MyItems();
                        break;
                    case "5": 
                        WithdrawMenu(vm.Withdraw(), ref moneyAtHand);
                        break;
                    case "6": 
                        menuActive = false;
                        break;
                    default: 
                        break;
                }
                
            }
            
        }


        private static void WelcomeMenu()
        {
            Console.WriteLine("welcome to this vending machine.");
            Console.WriteLine("what do you want to do?");
            Console.WriteLine("***********************************");
            Console.WriteLine("1: Deposit money");
            Console.WriteLine("2: Check balance");
            Console.WriteLine("3: Shop");
            Console.WriteLine("4: Owned products");
            Console.WriteLine("5: Withdraw money");
            Console.WriteLine("6: Leave program");
            Console.WriteLine("***********************************");
            Console.Write("=> ");
        }

        private static int DepositMenu(Dictionary<int,int> moneyList)
        {
            bool worked = true;
            int depositSum = 0;
            while (worked)
            {
                Console.Clear();
                Console.WriteLine($"You have this set of money at you desposal.");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("{0,-6}:{1,6}","Amount", "Value");
                foreach (var value in moneyList)
                {
                    Console.WriteLine($"{value.Value,5} :{value.Key,6}kr {((value.Key >= 20) ? "bills" : "coins")}");

                }

                Console.WriteLine("Pick a value to deposit? Enter to return.");
                Console.Write("=> ");

                worked = int.TryParse(Console.ReadLine(), out int depositValue);
                if (moneyList.ContainsKey(depositValue) == true)
                {
                    
                    if (moneyList[depositValue] > 0) 
                    {
                        depositSum += depositValue;
                        moneyList[depositValue] -= 1;
                    }
                    else 
                    { 
                        Console.WriteLine($"You are out of {depositValue}kr {((depositValue >= 20) ? "bills" : "coins")}");
                        Console.ReadKey();
                    }
                }

            }
            return depositSum;
        }

        private static void ShopMenu(VendingMachine vm, ref List<Product> productList)
        {
            bool stillShopping = true;
            while (stillShopping)
            {
                Console.Clear();
                Console.WriteLine("The products offered today are:");
                Console.WriteLine("***********************************");
                Console.WriteLine("{0,2} : {1,11} : {2,6}", "Id", "Name", "Amount");
                Console.WriteLine("-------------------------");
                foreach (var product in productList)
                {
                    Console.WriteLine($"{product.Id, 2} : {product.Name,11} : {product.Amount,6}");
                }
                Console.WriteLine("***********************************");
                Console.WriteLine("Which product are you interested in? ");
                Console.Write("=> ");
                bool aNumber = int.TryParse(Console.ReadLine(), out int value);
                if (productList.Exists(x => x.Id == value))
                {
                    var product = productList.Find(product => product.Id == value);

                    PurchaseProduct.GiveInfo(product);
                    Console.Write("Purchase? (y/n)");
                    string purchase = Console.ReadLine();
                    if (purchase.ToLower() == "y") vm.PurchaseProduct(ref product);
                    
                }
                else if 
                    (aNumber == false) break;
                else 
                {
                    Console.WriteLine("Please choose a product in the list");
                    Console.ReadKey();
                }
                
                
            }

        }

        private static void MyItems()
        {
            bool stillConsuming = true;
            while (stillConsuming)
            {
                Console.Clear();
                Console.WriteLine("The products you have bought are:");
                Console.WriteLine("----------------------------------");
                Console.WriteLine(String.Format("{0,-2} : {1,-12} : {2,6}", "Id", "Name", "Amount"));
                foreach (var product in ProductList.GetOwnedProducts().Where(x => x.Amount > 0))
                {
                    Console.WriteLine($"{product.Id,-2} : {product.Name,-12} : {product.Amount,6}");

                }
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Which product do you want to inspect?");
                Console.Write("=> ");
                bool aNumber = int.TryParse(Console.ReadLine(), out int value);
                if (ProductList.GetOwnedProducts().Exists(x => x.Id == value))
                {
                    
                    var product = ProductList.GetOwnedProducts().Find(product => product.Id == value);
                    ProductList.PresentOwnedProduct(product);
                    Console.WriteLine($"Do you want to {(product.Type == "drink" ? "drink" : "eat")} the {product.Name}. (y/n)");
                    Console.Write("=> ");
                    string consume = Console.ReadLine();
                    if (consume.ToLower() == "y") product.Consume();
                }
                else if (aNumber == false) break;
                else Console.WriteLine("Please choose a product in the list");

                Console.ReadKey();
            }
        }

        private static void WithdrawMenu(Dictionary<int, int> withdrawnMoney, ref Dictionary<int, int> moneyList)
        {
            
                foreach (KeyValuePair<int, int> kvp in moneyList)
                {
                    moneyList[kvp.Key] += withdrawnMoney[kvp.Key];
                    withdrawnMoney[kvp.Key] = 0;
                }
            
        }

    }
}
