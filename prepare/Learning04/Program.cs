
using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        bool run = true;

        // Extra feature: The program counts how many activities
        // are completed during this session.
        int completed = 0;

        while (run)
        {
            Console.WriteLine("\n--- Mindfulness Program ---");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            Activity activity = null;

            if (choice == "1")
            {
                activity = new BreathingActivity();
            }
            else if (choice == "2")
            {
                activity = new ReflectionActivity();
            }
            else if (choice == "3")
            {
                activity = new ListingActivity();
            }
            else if (choice == "4")
            {
                run = false;
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Please enter 1, 2, 3, or 4.");
            }

            if (activity != null)
            {
                activity.Run();
                completed++;
            }
        }

        Console.WriteLine($"Activities completed this session: {completed}");
    }
}

class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;
    }

    public int Duration
    {
        get { return _duration; }
    }

    public void StartActivity()
    {
        Console.WriteLine($"\nWelcome to the {_name}.");
        Console.WriteLine(_description);

        Console.Write("How long, in seconds, would you like for your session? ");

        while (!int.TryParse(Console.ReadLine(), out _duration)
               || _duration <= 0)
        {
            Console.Write("Enter a number greater than zero: ");
        }

        Console.WriteLine("Get ready...");
        Pause(3);
    }

    public void EndActivity()
    {
        Console.WriteLine("\nGood job!");
        Console.WriteLine($"You completed the {_name} for {Duration} seconds.");
        Pause(3);
    }

    public void Pause(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(1000);
        }

        Console.WriteLine();
    }

    public void Spinner(int seconds)
    {
        string[] symbols = { "|", "/", "-", "\\" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(symbols[i % symbols.Length]);
            Thread.Sleep(250);
            Console.Write("\b \b");
            i++;
        }
    }

    public virtual void Run()
    {
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing Activity",
            "This activity will help you relax by breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void Run()
    {
        StartActivity();

        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        bool breatheIn = true;

        while (DateTime.Now < endTime)
        {
            if (breatheIn)
            {
                Console.WriteLine("Breathe in...");
            }
            else
            {
                Console.WriteLine("Breathe out...");
            }

            int remaining = (int)Math.Ceiling(
                (endTime - DateTime.Now).TotalSeconds);

            Pause(Math.Min(4, remaining));
            breatheIn = !breatheIn;
        }

        EndActivity();
    }
}

class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something selfless."
    };

    private List<string> _questions = new List<string>()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience?",
        "What did you learn about yourself?",
        "How can you remember this experience in the future?"
    };

    public ReflectionActivity()
        : base("Reflection Activity",
            "This activity will help you reflect on times when you showed strength and resilience.")
    {
    }

    public override void Run()
    {
        StartActivity();

        Random random = new Random();
        string prompt = _prompts[random.Next(_prompts.Count)];

        Console.WriteLine("\nConsider the following prompt:");
        Console.WriteLine(prompt);
        Console.WriteLine("Think about it:");
        Spinner(3);

        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            string question = _questions[random.Next(_questions.Count)];

            Console.WriteLine("\n" + question);
            Spinner(5);
        }

        EndActivity();
    }
}

class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>()
    {
        "Who are people that you appreciate?",
        "What are some personal strengths of yours?",
        "Who have you helped this week?",
        "When have you felt grateful this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
        : base("Listing Activity",
            "This activity will help you recognize the good things in your life.")
    {
    }

    public override void Run()
    {
        StartActivity();

        Random random = new Random();
        string prompt = _prompts[random.Next(_prompts.Count)];

        Console.WriteLine("\nList as many responses as you can for:");
        Console.WriteLine(prompt);
        Console.WriteLine("Get ready...");
        Pause(5);

        List<string> answers = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        Console.WriteLine("Enter one answer per line.");
        Console.WriteLine("Press Enter on a blank line to finish early.");

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string answer = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(answer))
            {
                break;
            }

            answers.Add(answer);
        }

        Console.WriteLine($"\nYou listed {answers.Count} items.");
        EndActivity();
    }
}
