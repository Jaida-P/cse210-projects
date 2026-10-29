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
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int Age { get; set; }
    public string Title { get; set; } = "";
    public Race Race { get; set; }
    public CharacterStats Stats { get; set; }
    public int Experience { get; set; }

    public string GetFullName() { return FirstName + " " + LastName; }
    public void UpdateCharacter() { }
}

// CharacterStats
public class CharacterStats
{
    public int STR { get; set; }
    public int STA { get; set; }
    public int INT { get; set; }
    public int DEX { get; set; }
    public int DEF { get; set; }
    public int WIS { get; set; }
    public int LUC { get; set; }
    public int AvailablePoints { get; set; }

    public void AllocatePoints() { }
    public int GetStat(string stat) { return 0; }
    public void SetStat(string stat, int value) { }
    public int GetTotalStats() { return 0; }
}

// Race
public class Race
{
    public string Name { get; set; } = "";
    public CharacterStats BaseStats { get; set; }

    public CharacterStats GetBaseStats() { return BaseStats; }
    public void ApplyBaseStats(Character character) { }
}

// RaceManager
public class RaceManager
{
    public List<Race> Races { get; set; } = new List<Race>();

    public void LoadRaces() { }
    public Race GetRace(string name) { return null; }
    public void DisplayRaces() { }
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