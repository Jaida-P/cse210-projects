using System;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Develop05
{
    abstract class Goal
    {
        private string _name;
        private string _description;
        private int _points;

        public string Name => _name;
        public string Description => _description;
        public int Points => _points;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        public abstract int RecordEvent();
        public abstract bool IsComplete();
        public abstract string GetDetails();

        protected string Encode(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        public abstract string SaveString();

        public static Goal LoadString(string line)
        {
            string[] p = line.Split('|');

            string name = Encoding.UTF8.GetString(
                Convert.FromBase64String(p[1]));
            string description = Encoding.UTF8.GetString(
                Convert.FromBase64String(p[2]));

            int points = int.Parse(p[3]);

            if (p[0] == "S")
                return new SimpleGoal(name, description, points, p[4] == "1");

            if (p[0] == "E")
                return new EternalGoal(name, description, points);

            if (p[0] == "C")
                return new ChecklistGoal(name, description, points,
                    int.Parse(p[4]), int.Parse(p[5]), int.Parse(p[6]));

            throw new Exception("Unknown goal type.");
        }
    }

    class SimpleGoal : Goal
    {
        private bool _complete;

        public SimpleGoal(string name, string description, int points,
            bool complete = false)
            : base(name, description, points)
        {
            _complete = complete;
        }

        public override int RecordEvent()
        {
            if (_complete)
                return 0;

            _complete = true;
            return Points;
        }

        public override bool IsComplete() => _complete;

        public override string GetDetails()
        {
            return $"[{(_complete ? "X" : " ")}] {Name} - {Description}";
        }

        public override string SaveString()
        {
            return $"S|{Encode(Name)}|{Encode(Description)}|{Points}|{(_complete ? 1 : 0)}";
        }
    }

    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override int RecordEvent() => Points;

        public override bool IsComplete() => false;

        public override string GetDetails()
        {
            return $"[ ] {Name} - {Description} (Eternal)";
        }

        public override string SaveString()
        {
            return $"E|{Encode(Name)}|{Encode(Description)}|{Points}";
        }
    }

    class ChecklistGoal : Goal
    {
        private int _target;
        private int _completed;
        private int _bonus;

        public ChecklistGoal(string name, string description, int points,
            int target, int completed = 0, int bonus = 0)
            : base(name, description, points)
        {
            _target = target;
            _completed = completed;
            _bonus = bonus;
        }

        public override int RecordEvent()
        {
            if (IsComplete())
                return 0;

            _completed++;
            int earned = Points;

            if (_completed == _target)
                earned += _bonus;

            return earned;
        }

        public override bool IsComplete() => _completed >= _target;

        public override string GetDetails()
        {
            return $"[{(IsComplete() ? "X" : " ")}] {Name} - {Description} " +
                   $"(Completed {_completed}/{_target} times)";
        }

        public override string SaveString()
        {
            return $"C|{Encode(Name)}|{Encode(Description)}|{Points}|{_target}|{_completed}|{_bonus}";
        }
    }

    class Program
    {
        static List<Goal> goals = new List<Goal>();
        static int score = 0;
        static string filePath = "goals.txt";

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== ETERNAL QUEST ===");
                Console.WriteLine($"Your score: {score}");
                Console.WriteLine("1. Create a new goal");
                Console.WriteLine("2. Record a goal event");
                Console.WriteLine("3. Show goals");
                Console.WriteLine("4. Save goals");
                Console.WriteLine("5. Load goals");
                Console.WriteLine("6. Quit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        CreateGoal();
                        break;
                    case "2":
                        RecordGoal();
                        break;
                    case "3":
                        ShowGoals();
                        break;
                    case "4":
                        SaveGoals();
                        break;
                    case "5":
                        LoadGoals();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

            Console.WriteLine("Keep working toward your eternal quest!");
        }

        static void CreateGoal()
        {
            Console.WriteLine("\nGoal types:");
            Console.WriteLine("1. Simple goal");
            Console.WriteLine("2. Eternal goal");
            Console.WriteLine("3. Checklist goal");
            Console.Write("Choose a type: ");
            string type = Console.ReadLine() ?? "";

            Console.Write("Goal name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Goal description: ");
            string description = Console.ReadLine() ?? "";

            int points = ReadInt("Points earned per event: ");

            switch (type)
            {
                case "1":
                    goals.Add(new SimpleGoal(name, description, points));
                    break;

                case "2":
                    goals.Add(new EternalGoal(name, description, points));
                    break;

                case "3":
                    int target = ReadInt("How many times to complete it? ");
                    int bonus = ReadInt("Bonus points for completing it: ");

                    if (target < 1)
                    {
                        Console.WriteLine("Target must be at least 1.");
                        return;
                    }

                    goals.Add(new ChecklistGoal(
                        name, description, points, target, 0, bonus));
                    break;

                default:
                    Console.WriteLine("Invalid goal type.");
                    return;
            }

            Console.WriteLine("Goal created!");
        }

        static void RecordGoal()
        {
            if (goals.Count == 0)
            {
                Console.WriteLine("You have no goals yet.");
                return;
            }

            ShowGoals();
            int index = ReadInt("Which goal did you accomplish? ") - 1;

            if (index < 0 || index >= goals.Count)
            {
                Console.WriteLine("Invalid goal number.");
                return;
            }

            if (goals[index].IsComplete())
            {
                Console.WriteLine("That goal is already complete.");
                return;
            }

            // Polymorphism: each goal type handles its event differently.
            int earned = goals[index].RecordEvent();
            score += earned;

            Console.WriteLine($"You earned {earned} points!");
            Console.WriteLine($"Total score: {score}");
        }

        static void ShowGoals()
        {
            Console.WriteLine("\nYour goals:");

            if (goals.Count == 0)
            {
                Console.WriteLine("No goals created yet.");
                return;
            }

            for (int i = 0; i < goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {goals[i].GetDetails()}");
            }
        }

        static void SaveGoals()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"SCORE|{score}");

                foreach (Goal goal in goals)
                {
                    writer.WriteLine(goal.SaveString());
                }
            }

            Console.WriteLine("Goals and score saved!");
        }

        static void LoadGoals()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No save file found.");
                return;
            }

            List<Goal> loadedGoals = new List<Goal>();
            int loadedScore = 0;

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (line.StartsWith("SCORE|"))
                {
                    loadedScore = int.Parse(line.Split('|')[1]);
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    loadedGoals.Add(Goal.LoadString(line));
                }
            }

            goals = loadedGoals;
            score = loadedScore;

            Console.WriteLine("Goals and score loaded!");
        }

        static int ReadInt(string prompt)
        {
            int number;

            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out number)
                    && number >= 0)
                {
                    return number;
                }

                Console.WriteLine("Enter a valid non-negative number.");
            }
        }
    }
}
