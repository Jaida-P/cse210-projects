using System;

namespace Sandwhich
{
        class Program
    {
        static void Main(string[] args)
        {
            Console.Write("What is your first name? ");
            string FN = Console.ReadLine();

            Console.Write("What is your last name? ");
            string LN = Console.ReadLine();
            //Print
            Console.WriteLine($"Your name is {LN}, {FN} {LN}.");


            /* int number = 6;                         Declare variable always!! (This info is from the Video Demo for Prep 1)
                number = 8;
                number = number + 3;
            */
        }
    }
}