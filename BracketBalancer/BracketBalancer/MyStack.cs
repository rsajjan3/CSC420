using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketBalancer
{
    class MyStack
    {
        private char[] elements;
        private int top_index;
        private int max_size;

        public MyStack(int size)
        {
            this.elements = new char[size];
            this.top_index = -1; //Currently no top, so set some arbitrary value
            this.max_size = size;
        }
        public int length
        {
            get
            {
                return top_index + 1;
            }
        }

        public void push (char item)
        {
            if (this.top_index == this.max_size - 1) { throw new StackOverflowException("Stack is full"); }
            else
            {
                this.top_index++; //Increase the counter so there is a new 'top'
                this.elements[this.top_index] = item; //Add item to the 'top' of the stack
            }
        }

        public void mass_push(String items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                this.push(items[i]);
            }
        }
        public void mass_push(char[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                this.push(items[i]);
            }
        }

        public char pop()
        {
            if (this.top_index == -1) { throw new StackOverflowException("Trying to remove from an empty stack"); }
            else
            {
                return this.elements[this.top_index--]; //Return the top item and move the 'top' down one index
            }
        }

        public char peek()
        {
            if (this.top_index == -1) { throw new StackOverflowException("Trying to access an empty stack"); }
            else
            {
                return this.elements[this.top_index]; //Return the top item
            }
        }

        public bool balanced()
        {
            if ((this.length) % 2 != 0) return false; //Has to be an even numbr of elements to be balanced.
            else
            {
                MyStack tmp_stack = new MyStack(this.top_index);
                foreach(char item in this.elements)
                {
                    if (item == '{' || item == '[' || item == '(')
                    {
                        tmp_stack.push(item);
                    }
                    else if (tmp_stack.length > 0)
                    {
                        if (item == '}')
                        {
                            if (tmp_stack.peek() == '{') tmp_stack.pop();
                            else return false; //Brackets aren't matching return false asap
                        }
                        else if (item == ']')
                        {
                            if (tmp_stack.peek() == '[') tmp_stack.pop();
                            else return false;
                        }
                        else if (item == ')')
                        {
                            if (tmp_stack.peek() == '(') tmp_stack.pop();
                            else return false;
                        }
                        else
                        {
                            return false; //Non-bracket input
                        }
                    }
                    else return false;
                }
                if (tmp_stack.length > 0) return false; //If there are still items then it is not balanced
                else return true;
            }
        }

        public void print_stack()
        {
            for(int i = this.top_index; i >= 0; i--)
            {
                Console.WriteLine(this.elements[i]);
            }
        }
    }
}
