using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> num = new List<int>();

        Console.WriteLine("Enter a list of numbers. Type 0 when finished.");
        int number = 1;
        while (number !=0)
        {
            Console.WriteLine("Enter number: ");
            string user = Console.ReadLine();
            number = int.Parse(user);
            if (number !=0)
            {
                num.Add(number);
            }
        }
        int sum = 0;

        foreach(int n in num)
        {
            sum = sum+n;
        } 

        Console.WriteLine($"Sum equals: {sum}");
        double avg = (double)sum/num.Count;
        Console.WriteLine($"The average is: {avg}");
        int largest =num[0];
        
        foreach(int n in num)
        {
            if (n>largest)
            {
                largest = n;
            }
        }
        Console.WriteLine($"The largest number is: {largest}");
    }
}