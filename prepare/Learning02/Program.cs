using System;
using System.Collections.Generic;

public class Resume
{
    public string _name = "";
    public List<Job> _jobs = new List<Job>();

    public void Display()
    {
        Console.WriteLine($"Name: {_name}");

        foreach (Job job in _jobs)
        {
            job.Display();
        }
    }
}

public class Job
{
    public string _company = "";
    public string _jobTitle = "";
    public int _startYear;
    public int _endYear;

    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job();
        job1._company = "Bat cave";
        job1._jobTitle = "The Batman";
        job1._startYear = 1939;
        job1._endYear = 2026;

        Job job2 = new Job();
        job2._company = "Wayne Co. Ent.";
        job2._jobTitle = "Billionare CEO";
        job2._startYear = 1939;
        job2._endYear = 2026;

        Resume myResume = new Resume();
        myResume._name = "Bruce Wayne";

        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        myResume.Display();
    }
}
