using System;

namespace Prep3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate a random magic number
            Random randomGenerator = new Random();
            int magicNumber = randomGenerator.Next(1, 101);

            // Ask the user for their first guess
            Console.Write("What is your guess? ");
            int guess = int.Parse(Console.ReadLine());
            int NumGuess = 0;
            NumGuess++;


            // Keep guessing until the answer is correct
            while (guess != magicNumber)
            {
                if (guess<magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else
                {
                    Console.WriteLine("Lower");
                }

                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                NumGuess++;
            }

            // The user guessed correctly
            Console.WriteLine("You guessed it!");
            
            // How many times user guessed
            Console.WriteLine($"You made {NumGuess} guesses.");
        }
    }
}
