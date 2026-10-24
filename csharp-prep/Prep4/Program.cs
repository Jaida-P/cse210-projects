using System;

        {
            Console.WriteLine("List some things you are grateful for.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine();

            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.Write("Choose an activity: ");

            string choice = Console.ReadLine();

            Console.Write("How long would you like to do it? ");
            int duration = int.Parse(Console.ReadLine());

            Activity activity;

            if (choice == "1")
            {
                activity = new BreathingActivity(duration);
            }
            else if (choice == "2")
            {
                activity = new ReflectionActivity(duration);
            }
            else
            {
                activity = new ListingActivity(duration);
            }

            Console.WriteLine();
            activity.Start();

            Console.WriteLine($"Duration: {duration} seconds");

            activity.End();

            Console.WriteLine();
            Console.WriteLine("Thanks for using the program!");
        }
    }
}
