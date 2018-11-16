using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5_Hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            Random ran = new Random();
            MyHashTable tbl = new MyHashTable(35);

            for (int i = 0; i < 25; i++)
            {
                tbl.add(ran.Next(0, 400), ran.Next(10000, 100000));
            }

            Console.WriteLine(tbl.AverageAddProbes);
            Console.ReadLine();
        }
    }
}
