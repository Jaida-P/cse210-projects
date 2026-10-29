using System;

public class Fraction
{
    private int numerator;
    private int denominiator;

    public Fraction()
    {
        numerator= 1;
        denominiator= 1;
    }
    //Construtor 1 para
    public Fraction(int WholeNumber)
    {
        numerator= WholeNumber;
        denominiator= 1;
    }
    //Constructor 2 para
    public Fraction (int numer, int den)
    {
        numerator= numer;
        denominiator= den;
    }
    public int GetTop()
    {
        return denominiator;
    }
    public void SetDen(int den)
    {
        denominiator=den;
    }
    //Returns fraction as the as a string
    public string GetFractionString()
    {
        return numerator+"/"+denominiator;
    }
    public double GetDecimalValue()
    {
        return (double)numerator/denominiator;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Fraction fract1=  new Fraction();
        Fraction fract2= new Fraction(5);
        Fraction fract3= new Fraction(3,4);

        Console.WriteLine(fract1.GetFractionString());
        Console.WriteLine(fract2.GetFractionString());
        Console.WriteLine(fract3.GetFractionString());

        Console.Write
    }
}