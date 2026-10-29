using System;
using System.Collections.Generic;
//Work with files and folders
using System.IO;
//Gives tools to search, filter, and organize collections
using System.Linq;
//Converts character data into JSON format when saving and convert it back when loading
using System.Text.Json;

public class Program
{
    static void Main(string[] args)
    {
        CharacterCreator creator = new CharacterCreator();
        while (true)
        {
            Console.WriteLine("\n--- Routh Character Creator ---");
            Console.WriteLine("1. Create Character");
            Console.WriteLine("2. Load Character");
            Console.WriteLine("3. Exit");
            Console.Write("Choose: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1") creator.CreateCharacter();
            else if (choice == "2") creator.LoadCharacter();
            else if (choice == "3") break;
            else Console.WriteLine("Invalid choice.");
        }
    }
}

public class CharacterCreator
{
    public Character CurrentCharacter = new Character();

    public void CreateCharacter()
    {
        CurrentCharacter = new Character();

        Console.Write("First name: ");
        CurrentCharacter.FirstName = Console.ReadLine() ?? "";
        Console.Write("Last name: ");
        CurrentCharacter.LastName = Console.ReadLine() ?? "";
        CurrentCharacter.Age = ReadNumber("Age: ", 0, 999);
        Console.Write("Title: ");
        CurrentCharacter.Title = Console.ReadLine() ?? "";

        SelectRace();
        CurrentCharacter.UpdateStats();

        Console.WriteLine("\nCharacter created!");
        new CharacterSheet(CurrentCharacter).DisplaySheet();

        Console.Write("Save character? (y/n): ");
        if ((Console.ReadLine() ?? "").ToLower() == "y")
            new CharacterFileManager().SaveCharacter(CurrentCharacter);
    }

    private void SelectRace()
    {
        RaceManager raceManager = new RaceManager();
        raceManager.DisplayRaces();

        Console.Write("Choose a race: ");
        string raceName = Console.ReadLine() ?? "";
        Race selectedRace = raceManager.GetRace(raceName);

        if (selectedRace == null)
        {
            Console.WriteLine("Race not found. Try again.");
            SelectRace();
            return;
        }

        CurrentCharacter.Race = selectedRace.Name;
        selectedRace.ApplyBaseStats(CurrentCharacter);
    }

    private int ReadNumber(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int number) &&
                number >= min && number <= max)
                return number;
            Console.WriteLine($"Enter a number from {min} to {max}.");
        }
    }

    public void LoadCharacter()
    {
        CharacterFileManager files = new CharacterFileManager();
        List<string> saved = files.GetSavedCharacters();

        if (saved.Count == 0)
        {
            Console.WriteLine("No saved characters found.");
            return;
        }

        for (int i = 0; i < saved.Count; i++)
            Console.WriteLine($"{i + 1}. {Path.GetFileName(saved[i])}");

        Console.Write("Choose file number: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) ||
            choice < 1 || choice > saved.Count)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        CurrentCharacter = files.LoadCharacter(saved[choice - 1]);
        if (CurrentCharacter != null)
        {
            CurrentCharacter.UpdateStats();
            new CharacterSheet(CurrentCharacter).DisplaySheet();
        }
    }
}

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
    public int STR, STA, INT, DEX, DEF, WIS, LUC;
    public int AvailablePoints;

    public string GetFullName() => FirstName + " " + LastName;

    public void UpdateStats()
    {
        if (Experience == 0 && Age > 0)
            Experience = Math.Min(Age, 12) * 365 * 6;

        int level = 1;
        while (Math.Pow(level, 3) * 1000 <= Experience)
            level++;
        Level = level > 1 ? level - 1 : 1;

        Rank = new RankManager().CalculateRank(Level);
        Health = 30 + Math.Pow(STA, 1.75) * 2;
        Mana = 10 + Math.Pow(WIS, 1.75) * 2;
    }
}

public class Race
{
    public string Name;
    public int STR, STA, INT, DEX, DEF, WIS, LUC;

    public Race(string name, int str, int sta, int intel, int dex, int def, int wis, int luc)
    {
        Name = name;
        STR = str; STA = sta; INT = intel; DEX = dex;
        DEF = def; WIS = wis; LUC = luc;
    }

    public void ApplyBaseStats(Character character)
    {
        character.STR = STR; character.STA = STA; character.INT = INT;
        character.DEX = DEX; character.DEF = DEF; character.WIS = WIS; character.LUC = LUC;
        // Each race starts with stats drawn from a total budget of 70 points.
        character.AvailablePoints = 70 - STR - STA - INT - DEX - DEF - WIS - LUC;
    }
}

public class RaceManager
{
    private List<Race> Races = new List<Race>
    {
        new Race("Human", 10, 10, 10, 10, 10, 10, 10),
        new Race("Elf", 6, 8, 16, 12, 8, 14, 6),
        new Race("Dark Elf", 9, 10, 13, 13, 10, 12, 4),
        new Race("Dwarf", 13, 13, 10, 6, 13, 10, 5),
        new Race("Beastfolk", 12, 11, 8, 12, 10, 9, 8),
        new Race("Demon", 13, 10, 10, 12, 11, 10, 4),
        new Race("Diefthamos", 13, 12, 9, 11, 13, 6, 13)
    };

    public void LoadRaces() { }

    public void DisplayRaces()
    {
        Console.WriteLine("\nAvailable races:");
        foreach (Race race in Races)
            Console.WriteLine("- " + race.Name);
    }

    public Race GetRace(string name)
    {
        return Races.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}

public class ExperienceManager
{
    public int Experience { get; set; }
    public int Level { get; set; } = 1;

    public int CalculateLevel()
    {
        int level = 1;
        while (Math.Pow(level, 3) * 1000 <= Experience) level++;
        return level > 1 ? level - 1 : 1;
    }

    public void AddExperience(int amount)
    {
        if (amount > 0) Experience += amount;
        Level = CalculateLevel();
    }

    public int GetExperienceToNextLevel()
    {
        return (int)(Math.Pow(Level + 1, 3) * 1000) - Experience;
    }
}

public class RankManager
{
    public SortedDictionary<int, string> RankThresholds = new SortedDictionary<int, string>
    {
        {1,"F-"},{3,"F"},{5,"F+"},{10,"E-"},{15,"E"},{20,"E+"},
        {30,"D-"},{40,"D"},{50,"D+"},{65,"C-"},{80,"C"},{100,"C+"},
        {125,"B-"},{150,"B"},{200,"B+"},{250,"A-"},{300,"A"},{400,"A+"},
        {800,"S-"},{1000,"S"},{1500,"S+"}
    };

    public string CurrentRank = "F-";

    public string CalculateRank(int level)
    {
        foreach (var rank in RankThresholds)
            if (level >= rank.Key) CurrentRank = rank.Value;
        return CurrentRank;
    }

    public void GetRankThresholds()
    {
        foreach (var rank in RankThresholds)
            Console.WriteLine($"Level {rank.Key}: {rank.Value}");
    }
}

public class CharacterFileManager
{
    public string SaveDirectory = "Characters";

    public void SaveCharacter(Character character)
    {
        Directory.CreateDirectory(SaveDirectory);
        string fileName = character.FirstName + "_" + character.LastName;
        foreach (char c in Path.GetInvalidFileNameChars())
            fileName = fileName.Replace(c, '_');

        string path = Path.Combine(SaveDirectory, fileName + ".json");
        try
        {
            File.WriteAllText(path, JsonSerializer.Serialize(character,
                new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("Saved to " + path);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Could not save character: " + ex.Message);
        }
    }

    public Character LoadCharacter(string filepath)
    {
        try
        {
            return JsonSerializer.Deserialize<Character>(File.ReadAllText(filepath));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Could not load character: " + ex.Message);
            return null;
        }
    }

    public List<string> GetSavedCharacters()
    {
        Directory.CreateDirectory(SaveDirectory);
        return Directory.GetFiles(SaveDirectory, "*.json").ToList();
    }
}

public class CharacterSheet
{
    public Character Character;

    public CharacterSheet(Character character) { Character = character; }

    public void DisplaySheet() => Console.WriteLine(FormatSummary());

    public string FormatStats()
    {
        return $"STR: {Character.STR}\nSTA: {Character.STA}\nINT: {Character.INT}\n" +
               $"DEX: {Character.DEX}\nDEF: {Character.DEF}\nWIS: {Character.WIS}\nLUC: {Character.LUC}";
    }

    public string FormatSummary()
    {
        Character.UpdateStats();
        //Lets te string span multiple lines without needed all the \n
        return $@"
--- Character Sheet ---
Name: {Character.GetFullName()}
Race: {Character.Race}
Age: {Character.Age}
Title: {Character.Title}
Level: {Character.Level}
Rank: {Character.Rank}
Experience: {Character.Experience}
Health: {Character.Health:0.0}
Mana: {Character.Mana:0.0}

{FormatStats()}
Unspent points: {Character.AvailablePoints}";
    }
}
