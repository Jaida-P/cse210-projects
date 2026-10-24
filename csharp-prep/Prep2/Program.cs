using System;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter grade percentage: ");
            string value= Console.ReadLine();


            int x=int.Parse(value);
            int A=90;
            int B=80;
            int C=70;
            int D=60;

            if(x>=A)                              //&&=and      ||=or    !=not
            {
                Console.WriteLine("You have an A.");
            }
            else if (x>=B)
            {
                Console.WriteLine("You have a B.");
            }
            else if (x>=C)
            {
                Console.WriteLine("You have a C.");
            }
            else if (x>=D)
            {
                Console.WriteLine("You have a D.");
            }
            else
            {
                Console.WriteLine("You have an F.");
            }
        }
    }
}