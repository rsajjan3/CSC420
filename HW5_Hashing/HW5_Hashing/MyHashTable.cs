using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5_Hashing
{
    class Entry
    {
        private int _key;
        private object _value;
        public Entry(int key, object value)
        {
            _key = key;
            _value = value;
        }
        public int Key
        {
            get
            {
                return _key;
            }
        }
        public object Value
        {
            get
            {
                return _value;
            }
        }
    }
    class MyHashTable
    {
        private Entry[] _table;
        private int _count, _addProbes, _findProbes, _finds, _divisor;
        public MyHashTable(int size)
        {
            _count = 0;
            _addProbes = 0;
            _findProbes = 0;
            _finds = 0;
            _table = new Entry[size];
            _divisor = findPrime(size);

            for(int i = 0; i < size; i++)
            {
                _table[i] = null;
            }
        }
        public int Count
        {
            get
            {
                return _count;
            }
        }
        public int AverageAddProbes
        {
            get
            {
                return _addProbes / _count;
            }
        }
        public int AverageFindProbes
        {
            get
            {
                return _findProbes / _finds;
            }
        }
        public void add(int key, object item)
        {
            if (_count != _table.Length)
            {
                int hashKey = key % _divisor;
                if (_table[hashKey] != null) //Collison, need to do linear-probing
                {
                    hashKey = linearProbe(hashKey);
                    if (hashKey != -1)
                    {
                        if (_table[hashKey] != null &&_table[hashKey].Key == key) throw new ArgumentException("An item with the same key has already been added");
                        else _table[hashKey] = new Entry(key, item);
                    }
                    else throw new IndexOutOfRangeException("Hashtable is full");
                }
                else _table[hashKey] = new Entry(key, item);
                _count++;
            }
            else throw new IndexOutOfRangeException("Hashtable is full");
        }
        public void add(String key, object item)
        {
            int ascii = getASCIIValue(key);
            add(ascii, item);
        }
        public object find(int key)
        {
            _finds++;
            int hashKey = key % _divisor;
            if (_table[hashKey] != null)
            {
                if (_table[hashKey].Key != key)
                {
                    hashKey = linearProbe(hashKey, true, key);
                    if (hashKey != -1) return _table[hashKey].Value;
                    else throw new KeyNotFoundException(Convert.ToString(key) + " does not exist");
                }
                else return _table[hashKey].Value;
            }
            else throw new KeyNotFoundException(Convert.ToString(key) + " does not exist");
        }
        public object find(String key)
        {
            int ascii = getASCIIValue(key);
            return find(ascii);
        }
        private int linearProbe(int startPoint, bool find = false, int key = -1)
        {
            int i = startPoint+1;
            for(int iterations = 1; iterations <= _table.Length; iterations++)
            {
                if (i == _table.Length) i = 0; //Reached the end, loop to the start
                if ((!find && _table[i] == null) || (_table[i] != null && _table[i].Key == key))
                {
                    if (find) _findProbes += iterations;
                    else _addProbes += iterations;
                    return i;
                }
                i++;
            }
            return -1; //Reached the end and didn't find an open index.
        }
        private static int findPrime(int iterations)
        {
            for (int i = iterations; i > 0; i--) //Instead of counting up to iterations, start at iterations and return when first prime found
            {
                bool isPrime = true;
                for (int x = 2; x < Math.Sqrt(i); x++)
                {
                    if (i % x == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) return i;
            }
            return -1; //No primes found
        }
        private static int getASCIIValue(String key)
        {
            int ascii = 0;
            foreach (char c in key)
            {
                ascii += Convert.ToInt32(c);
            }
            return ascii;
        }
    }
}
