using System;

class Program
{
    static void Main(string[] args)
    {
        //Create assignment object with student name and topic
        Assignment assignment = new Assignment(
            "Samuel Bennett", "Multiplication");
        Console.WriteLine(assignment.GetSummary());
        MathAssignment math = new MathAssignment("Robert Rodriguez", "Fractions", "7.3", "8-19");
        //Display the math assignment summary
        Console.WriteLine(math.GetSummary());
        Console.WriteLine(math.GetHomeworkList());
        WritingAssignment writing = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");
        Console.WriteLine(writing.GetSummary());
        Console.WriteLine(writing.GetWritingInformation());
    }
}

public class Assignment
{
    //Store student's name and assignment topic.
    private string _studentName;
    private string _topic;
    public Assignment(string studentName, string topic)
    {
        _studentName = studentName;
        _topic = topic;
    }
    //Return student's name and topic as summary
    public string GetSummary()
    {
        return _studentName + " - " + _topic;
    }
    public string GetStudentName()
    {
        return _studentName;
    }
}
public class MathAssignment : Assignment
{
    private string _textbookSection;
    private string _problems;
    //Create math assignment and pass name and topic to base class
    public MathAssignment(string studentName, string topic,
                          string textbookSection, string problems)
        : base(studentName, topic)
    {
        _textbookSection = textbookSection;
        _problems = problems;
    }

    public string GetHomeworkList()
    {
        return "Section " + _textbookSection +
               " Problems " + _problems;
    }
}
public class WritingAssignment : Assignment
{
    private string _title;
    public WritingAssignment(string studentName, string topic, string title)
    :base(studentName, topic)
    {
        _title = title;
    }
    public string GetWritingInformation()
    {
        return _title + " by " + GetStudentName();
    }
}
