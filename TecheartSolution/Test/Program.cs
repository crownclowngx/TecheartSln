﻿using System;
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
            {

                Console.WriteLine(Guid.NewGuid().ToString());
                dynamic dy = new { kkk = 1 };
                var s = dy.kkk;
            }
            Console.Read();
        }
    }
}
