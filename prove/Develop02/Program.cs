using System;

class Program
{
    static void Main(string[] args)
    {
        //Create objects for program to work
        Journal Notes = new Journal();
        PromptGen Prompt = new PromptGen();
        bool run = true;

        //Keep showing menu until quit
        while (run)
        {
            Console.WriteLine("\n ---Journal---");
            Console.WriteLine("1. Write new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save Journal");
            Console.WriteLine("4. Load Journal");
            Console.WriteLine("5. Quit");

            //Choices
            Console.WriteLine("Choose an option:");
            string choose = Console.ReadLine();

            //Tasks
            if (choose == "1")
            {
                string generate = Prompt.GetPrompt();

                Console.WriteLine(generate);
                Console.Write("Your response: ");
                string response = Console.ReadLine();

                //Store date
                string date = DateTime.Now.ToLongDateString();
                Entry newEntry = new Entry(date, generate, response);
                Notes.AddEntry(newEntry);
            }
            else if (choose == "2")
            {
                Notes.DisplayJournal();
            }
            else if (choose == "3")
            {
                Console.Write("Enter a filename  to save: ");
                string filename = Console.ReadLine();

                Notes.SaveToFile(filename);
            }
            else if (choose == "4")
            {
                Console.Write("Enter a filename to load: ");
                string filename = Console.ReadLine();

                Notes.LoadFromFile(filename);
            }
            else if (choose == "5")
            {
               //Change the conditoon
               run = false;
               Console.WriteLine("See you later!"); 
            }
            else
            {
                Console.WriteLine("Please enter number from 1 to 5.");
            }
        }
    }
}

class Entry
{
    //Attributes
    public string _date;
    public string _prompt;
    public string _response;

    //Constructor
    public Entry(string date, string prompt, string response)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
    }

    //Display entry
    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}");
        Console.WriteLine();
    }
}


class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries yet.");
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        StreamWriter outputFile = new StreamWriter(filename);

        foreach (Entry entry in _entries)
        {
            outputFile.WriteLine(entry._date + "|" + entry._prompt + "|" + entry._response);
        }

        outputFile.Close();
        Console.WriteLine("Journal saved!");
    }

    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            _entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');

                if (parts.Length == 3)
                {
                    Entry entry = new Entry(parts[0], parts[1], parts[2]);
                    _entries.Add(entry);
                }
            }

            Console.WriteLine("Journal loaded!");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

class PromptGen
{
    public List<string> _prompts = new List<string>()
    {
        "What was the best part of my day?",
        "Who did I talk to today?",
        "What made me happy today?",
        "What is something new I learned today?",
        "What would I change about today?",
        "What am I thankful for?",
        "What can I do to be bettr tomorrow?"
    };

    public string GetPrompt()
    {
        Random random = new Random();
        int number = random.Next(_prompts.Count);
        return _prompts[number];
    }
}
