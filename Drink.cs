using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Drink : Product
    {
        
        public override void Consume()
        {
            if (Amount > 0)
            {
                Console.WriteLine($"You drank the {Name}. Now you feel refreshed");
                Amount -= 1;
            }
            else
            {
                Console.WriteLine($"You are out of {Name}. Go and buy more.");
            }
        }

        public override string DisplayVolume()
        {
            return $"{Volume}ml";
        }
    }
}
