using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercuryLevel : MonoBehaviour
{
    public enum DifficultyLevel { Easy, Medium, Hard }

    public DifficultyLevel difficultyLevel;
    public GameObject bubbleFrac;
    public Game
    // Start is called before the first frame update
    void Start()
    {
        List<Fraction> possibleResults = GetRandomFractions("*");
        Debug.Log(possibleResults[0].ToString() + "   " + possibleResults[1].ToString() + "  " + possibleResults[2].ToString() + "  " + possibleResults[3].ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private Fraction GetRandomFraction()
    {
        int numerator = Random.Range(1, 10);
        int denominator = Random.Range(2, 10);
        while (denominator == numerator)
            denominator = Random.Range(2, 10);
        return new Fraction(numerator, denominator);
    }

    private List<Fraction> GetRandomFractions(string op)
    {
        List<Fraction> fractions = new List<Fraction>();
        Fraction frac1 = GetRandomFraction();
        Fraction frac2 = GetRandomFraction();
        Fraction frac3 = GetRandomFraction();
        Fraction fracResult = new Fraction(1,1);
        if (op == "+")
        {
            fracResult = frac1 + frac2;
        }
        else if (op == "-")
        {
            fracResult = frac1 - frac2;
        }
        else if (op == "*")
        {
            fracResult = frac1 * frac2;
        }
        else if (op == "/")
        {
            fracResult = frac1 / frac2;
        }
        fracResult.Simplify();
        fractions.Add(frac1);
        fractions.Add(frac2);
        fractions.Add(frac3);
        ShuffleList(fractions);
        fractions.Add(fracResult);
        return fractions;
    }


    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
public struct Fraction
{
    public int numerator;
    public int denominator;

    public Fraction(int n, int d)
    {
        numerator = n;
        denominator = d;
    }

    public override string ToString()
    {
        return numerator.ToString() + "/" + denominator.ToString();
    }

    public float ToFloat()
    {
        return (float)numerator / (float)denominator;
    }

    public bool Equals(Fraction other)
    {
        return this.ToFloat() == other.ToFloat();
    }

    // Sobrecarga del operador +
    public static Fraction operator +(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.numerator * f2.denominator + f2.numerator * f1.denominator;
        int newDenominator = f1.denominator * f2.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    // Sobrecarga del operador -
    public static Fraction operator -(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.numerator * f2.denominator - f2.numerator * f1.denominator;
        int newDenominator = f1.denominator * f2.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    // Sobrecarga del operador *
    public static Fraction operator *(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.numerator * f2.numerator;
        int newDenominator = f1.denominator * f2.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    // Sobrecarga del operador /
    public static Fraction operator /(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.numerator * f2.denominator;
        int newDenominator = f1.denominator * f2.numerator;
        return new Fraction(newNumerator, newDenominator);
    }

    //Simplificar fracciones
    public void Simplify()
    {
        int mcd = MCD(numerator, denominator);
        numerator /= mcd;
        denominator /= mcd;
    }

    private static int MCD(int a, int b)
    {
        // Algoritmo de Euclides para encontrar el MCD
        while (b != 0)
        {
            int t = b;
            b = a % b;
            a = t;
        }
        return a;
    }


}