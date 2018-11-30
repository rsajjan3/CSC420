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
            int[] indicies = new int[25];
            MyHashTable tbl = new MyHashTable(50);
            Random ran = new Random();
            for (int x = 0; x < 40; x++)
            {
                tbl = new MyHashTable(50);
                for (int i = 0; i < 25; i++)
                {
                    indicies[i] = ran.Next(0, 200);
                    tbl.add(indicies[i], ran.Next(10000, 100000));
                }
                Console.WriteLine("Run ({0}/{1}). Average Add Probes: {2}", (x + 1), 40, tbl.AverageAddProbes);
            }
            Console.WriteLine();


            for (int x = 0; x < 40; x++) //Only tests the last hash table created above.
            {
                int idx = indicies[ran.Next(0, indicies.Length)];
                tbl.find(idx);
                Console.WriteLine("Run ({0}/{1}). Average Find Probes: {2}", (x + 1), 40, tbl.AverageFindProbes);
            }

            Console.WriteLine(tbl.find(indicies[0]));
            Console.ReadLine();
        }
    }
}
