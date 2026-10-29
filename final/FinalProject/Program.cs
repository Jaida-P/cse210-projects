using System;
using System.Collections.Generic;

// Program
public class Program
{
    public static void Main(string[] args) { }
    public void ShowMenu() { }
    public void ExitProgram() { }
}

// CharacterCreator
public class CharacterCreator
{
    public Character CurrentCharacter;

    public void CreateCharacter()
    {
        CurrentCharacter = new Character();

        Console.Write("First name: ");
        CurrentCharacter.FirstName = Console.ReadLine();

        Console.Write("Last name: ");
        CurrentCharacter.LastName = Console.ReadLine();

        Console.Write("Age: ");
        CurrentCharacter.Age = int.Parse(Console.ReadLine());

        Console.Write("Title: ");
        CurrentCharacter.Title = Console.ReadLine();

        Console.Write("Race: ");
        string race = Console.ReadLine();

        CurrentCharacter.Race = race;
    }
}

// Character
public class Character
{
    public string FirstName = "";
    public string LastName = "";
    public int Age;
    public string Title = "";
    public string Race = "";
    public int Experience;
    public int Level = 1;
    public string Rank = "F-";
    public double Health;
    public double Mana;

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}

// CharacterStats
// 2. CharacterCreator
public class CharacterCreator
{
    public Character CurrentCharacter { get; set; }

    public void CreateCharacter()
    {
        CurrentCharacter = new Character();

        GetCharacterInfo();
        SelectRace();

        Console.WriteLine("\nCharacter created successfully!");
    }

    public void GetCharacterInfo()
    {
        Console.Write("Enter first name: ");
        CurrentCharacter.FirstName = Console.ReadLine() ?? "";

        Console.Write("Enter last name: ");
        CurrentCharacter.LastName = Console.ReadLine() ?? "";

        Console.Write("Enter age: ");
        int.TryParse(Console.ReadLine(), out int age);
        CurrentCharacter.Age = age;

        Console.Write("Enter title: ");
        CurrentCharacter.Title = Console.ReadLine() ?? "";
    }

    public void SelectRace()
    {
        RaceManager raceManager = new RaceManager();

        raceManager.LoadRaces();
        raceManager.DisplayRaces();

        Console.Write("Choose a race: ");
        string raceName = Console.ReadLine() ?? "";

        Race selectedRace = raceManager.GetRace(raceName);

        if (selectedRace != null)
        {
            CurrentCharacter.Race = selectedRace;
            selectedRace.ApplyBaseStats(CurrentCharacter);
        }
        else
        {
            Console.WriteLine("Race not found. Please try again.");
            SelectRace();
        }
    }
}

// Race
public class Race
{
    public string Name;
    public int STR;
    public int STA;
    public int INT;
    public int DEX;
    public int DEF;
    public int WIS;
    public int LUC;

    public Race(string name, int str, int sta, int intel,
                int dex, int def, int wis, int luc)
    {
        Name = name;
        STR = str;
        STA = sta;
        INT = intel;
        DEX = dex;
        DEF = def;
        WIS = wis;
        LUC = luc;
    }

    public void DisplayRace()
    {
        Console.WriteLine("Race: " + Name);
        Console.WriteLine("STR: " + STR);
        Console.WriteLine("STA: " + STA);
        Console.WriteLine("INT: " + INT);
        Console.WriteLine("DEX: " + DEX);
        Console.WriteLine("DEF: " + DEF);
        Console.WriteLine("WIS: " + WIS);
        Console.WriteLine("LUC: " + LUC);
    }
}

// ExperienceManager
public class ExperienceManager
{
    public int Experience { get; set; }
    public int Level { get; set; }

    public int CalculateLevel() { return 1; }
    public void AddExperience(int amount) { }
    public int GetExperienceToNextLevel() { return 0; }
}

// RankManager
public class RankManager
{
    public SortedDictionary<int, string> RankThresholds { get; set; }
        = new SortedDictionary<int, string>();

    public string CurrentRank { get; set; } = "F-";

    public string CalculateRank(int level) { return "F-"; }
    public void GetRankThresholds() { }
}

// CharacterFileManager
public class CharacterFileManager
{
    public string SaveDirectory { get; set; } = "Characters";

    public void SaveCharacter(Character character) { }
    public Character LoadCharacter(string filepath) { return null; }
    public List<string> GetSavedCharacters() { return new List<string>(); }
}

// CharacterSheet
public class CharacterSheet
{
    public Character Character { get; set; }

    public void DisplaySheet() { }
    public string FormatStats() { return ""; }
    public string FormatSummary() { return ""; }
}