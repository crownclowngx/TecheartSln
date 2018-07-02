using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            while (true)
            {

                Console.Write (random.Next(0, 20) +" ");
 
            }
            
        }
    }
}
