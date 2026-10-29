using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        bool run = true;

        while (run)
        {
            Console.WriteLine("\nEternal Quest");
            Console.WriteLine("1. Show Goals");
            Console.WriteLine("2. Create Goal");
            Console.WriteLine("3. Record Goal");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Save");
            Console.WriteLine("6. Load");
            Console.WriteLine("7. Quit");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            if (choice == "1")
                manager.ShowGoals();
            else if (choice == "2")
                manager.CreateGoal();
            else if (choice == "3")
                manager.RecordGoal();
            else if (choice == "4")
                manager.ShowScore();
            else if (choice == "5")
                manager.Save();
            else if (choice == "6")
                manager.Load();
            else if (choice == "7")
                run = false;
        }
    }
}

class GoalManager
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;

    public void ShowGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + goals[i].GetDetails());
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
        {
            goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type == "2")
        {
            goals.Add(new EternalGoal(name, description, points));
        }
        else if (type == "3")
        {
            Console.Write("How many times? ");
            int target = int.Parse(Console.ReadLine());

            Console.Write("Bonus points: ");
            int bonus = int.Parse(Console.ReadLine());

            goals.Add(new ChecklistGoal(
                name, description, points, target, bonus));
        }
    }

    public void RecordGoal()
    {
        ShowGoals();

        if (goals.Count == 0)
            return;

        Console.Write("Which goal? ");
        int number = int.Parse(Console.ReadLine());

        if (number > 0 && number <= goals.Count)
        {
            int earned = goals[number - 1].RecordEvent();
            score += earned;
            Console.WriteLine("Points earned: " + earned);
        }
    }

    public void ShowScore()
    {
        Console.WriteLine("Your score is " + score);

        // Extra feature: player levels up every 1000 points.
        Console.WriteLine("Level: " + (score / 1000 + 1));
    }

    public void Save()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            writer.WriteLine(score);

            foreach (Goal goal in goals)
            {
                writer.WriteLine(goal.SaveGoal());
            }
        }

        Console.WriteLine("Saved!");
    }

    public void Load()
    {
        if (!File.Exists("goals.txt"))
        {
            Console.WriteLine("No save file found.");
            return;
        }

        string[] lines = File.ReadAllLines("goals.txt");
        score = int.Parse(lines[0]);
        goals.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');

            if (parts[0] == "Simple")
            {
                SimpleGoal goal = new SimpleGoal(
                    parts[1], parts[2], int.Parse(parts[3]));

                if (bool.Parse(parts[4]))
                    goal.RecordEvent();

                goals.Add(goal);
            }
            else if (parts[0] == "Eternal")
            {
                goals.Add(new EternalGoal(
                    parts[1], parts[2], int.Parse(parts[3])));
            }
            else if (parts[0] == "Checklist")
            {
                goals.Add(new ChecklistGoal(
                    parts[1], parts[2], int.Parse(parts[3]),
                    int.Parse(parts[4]), int.Parse(parts[5]),
                    int.Parse(parts[6])));
            }
        }

        Console.WriteLine("Loaded!");
    }
}

abstract class Goal
{
    private string name;
    private string description;
    private int points;
    protected bool complete;

    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public int Points { get { return points; } }

    public Goal(string n, string d, int p)
    {
        name = n;
        description = d;
        points = p;
        complete = false;
    }

    public abstract int RecordEvent();
    public abstract string GetDetails();
    public abstract string SaveGoal();
}

class SimpleGoal : Goal
{
    public SimpleGoal(string n, string d, int p)
        : base(n, d, p)
    {
    }

    public override int RecordEvent()
    {
        if (complete)
            return 0;

        complete = true;
        return Points;
    }

    public override string GetDetails()
    {
        string box = complete ? "[X]" : "[ ]";
        return box + " " + Name + " - " + Description;
    }

    public override string SaveGoal()
    {
        return "Simple|" + Name + "|" + Description
            + "|" + Points + "|" + complete;
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string n, string d, int p)
        : base(n, d, p)
    {
    }

    public override int RecordEvent()
    {
        return Points;
    }

    public override string GetDetails()
    {
        return Name + " - " + Description + " (Eternal)";
    }

    public override string SaveGoal()
    {
        return "Eternal|" + Name + "|" + Description + "|" + Points;
    }
}

class ChecklistGoal : Goal
{
    private int target;
    private int bonus;
    private int count;

    public ChecklistGoal(string n, string d, int p, int t, int b, int c = 0)
        : base(n, d, p)
    {
        target = t;
        bonus = b;
        count = c;

        if (count >= target)
            complete = true;
    }

    public override int RecordEvent()
    {
        if (count >= target)
            return 0;

        count++;
        int earned = Points;

        if (count == target)
        {
            earned += bonus;
            complete = true;
        }

        return earned;
    }

    public override string GetDetails()
    {
        string box = complete ? "[X]" : "[ ]";
        return box + " " + Name + " - " + count + "/" + target;
    }

    public override string SaveGoal()
    {
        return "Checklist|" + Name + "|" + Description
            + "|" + Points + "|" + target + "|" + bonus + "|" + count;
    }
}