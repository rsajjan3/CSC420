﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Runway sim = new Runway();
            sim.RunSimulation(2);

            Console.ReadLine();
        }
    }
}
