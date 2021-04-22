using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Candy : Product
    {
        
        public override void Consume()
        {
            if (Amount > 0)
            {
                Console.WriteLine($"You ate the {Name}. Yummy");
                Amount -= 1;
            }
            else
            {
                Console.WriteLine($"You are out of {Name}. Go and buy more.");
            }
        }

        public override string DisplayVolume()
        {
            return $"{Volume}g";
        }
    }
}
