using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketBalancer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sample strings to try:
            // {([])[()]}
            // (([][){})
            // {[[[]]]}
            // {[[({})]]}
            MyStack steck;
            String[] test_cases = new String[] { "{([])[()]}", "(([][){})", "{[[[]]]}", "{[[({})]]}", "{{{{{{{{{", "{{{{}}" };

            for (int i = 0; i < test_cases.Length; i++)
            {
                Console.WriteLine("Test Case: " + test_cases[i]);
                steck = new MyStack(test_cases[i].Length);
                steck.mass_push(test_cases[i]);
                PrintContents(steck);
                PrintBalance(steck);
            }

            while (true)
            {
                Console.Write("Enter in your test string(no spaces): ");
                String user_input = Console.ReadLine();

                steck = new MyStack(user_input.Length);
                steck.mass_push(user_input);

                PrintContents(steck);

                PrintBalance(steck);

                Console.ReadLine();
            }
        }
        static void PrintContents(MyStack input)
        {
            Console.WriteLine("STACK CONTENTS: ");
            Console.ForegroundColor = ConsoleColor.Green;
            input.print_stack();
            Console.ResetColor();
            Console.WriteLine("END STACK CONTENTS\r\n");
        }
        static void PrintBalance(MyStack input)
        {
            bool balance = input.balanced();
            if (balance) Console.WriteLine("String is balanced\r\n");
            else Console.WriteLine("String is not balanced\r\n");
        }
    }
}
