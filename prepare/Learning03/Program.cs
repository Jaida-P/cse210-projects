
using System;

public class Fraction
{
    private int numerator;
    private int denominiator;

    public Fraction()
    {
        numerator = 1;
        denominiator = 1;
    }

    // Constructor 1 parameter
    public Fraction(int WholeNumber)
    {
        numerator = WholeNumber;
        denominiator = 1;
    }

    // Constructor 2 parameters
    public Fraction(int numer, int den)
    {
        numerator = numer;
        denominiator = den;
    }

    public int GetNumer()
    {
        return numerator;
    }

    public void SetNumer(int numer)
    {
        numerator = numer;
    }

    public int GetDen()
    {
        return denominiator;
    }

    public void SetDen(int den)
    {
        denominiator = den;
    }

    // Returns the fraction as a string
    public string GetFractionString()
    {
        return numerator + "/" + denominiator;
    }

    // Returns the fraction as a decimal
    public double GetDecimalValue()
    {
        return (double)numerator / denominiator;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Fraction fract1 = new Fraction();
        Fraction fract2 = new Fraction(5);
        Fraction fract3 = new Fraction(3, 4);

        Console.WriteLine(fract1.GetFractionString());
        Console.WriteLine(fract2.GetFractionString());
        Console.WriteLine(fract3.GetFractionString());

        Console.WriteLine(fract3.GetDecimalValue());

        fract3.SetNumer(2);
        fract3.SetDen(5);

        Console.WriteLine(fract3.GetFractionString());
        Console.WriteLine(fract3.GetDecimalValue());

        Console.WriteLine(fract3.GetNumer());
        Console.WriteLine(fract3.GetDen());

        Random random = new Random();

        for (int i = 1; i <= 20; i++)
        {
            int numer = random.Next(1, 11);
            int den = random.Next(1, 11);

            Fraction fraction = new Fraction(numer, den);

            Console.WriteLine(
                $"Fraction {i}: {fraction.GetFractionString()} Number: {fraction.GetDecimalValue()}");
        }
    }
}