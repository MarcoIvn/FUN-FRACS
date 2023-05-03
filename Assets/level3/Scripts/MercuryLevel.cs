using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class MercuryLevel : MonoBehaviour
{
    public enum DifficultyLevel { Easy, Medium, Hard }

    public DifficultyLevel difficulty;
    public GameObject bubbleFrac;
    public GameObject frac1, frac2, frac3, frac4,frac5;
    private Vector3 initPosf1, initPosf2, initPosf3, initPosf4, initPosf5;
    public GameObject OperatorUI;
    private string currOperator;
    private Fraction currResult;
    private List<GameObject> fracs = new List<GameObject>();
    private List<string> operators = new List<string>();
    private List<Fraction> possibleResults = new List<Fraction>();
    private int operationsComplete = 0;
    private int operations = 4;
    private int operatorIndex = 0;
    private int calification = 0;
    private int maxCalification = 4;
    //Mouth
    public static int errorCount = 0;
    public GameObject frac1UI, frac2UI, frac3UI;
    public static bool firstObjectDestroyed;
    public GameObject checkUI;
    public static Fraction currFrac1, currFrac2, result;
    public static bool operationComplete = false;
    public GameObject MonsterTongue;
    private bool canFeed = true;

    public SendToServer script;
    public GameObject LosePanel;
    public GameObject WinPanel;
    public static int notaFinal = 0;
    public static bool gamecomplete = false;
    public AudioSource crunch, correct, incorrect;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        PausaB.juegoPausado = false;
        gamecomplete = false;
        notaFinal = 0;
        
        LosePanel.SetActive(false);
        WinPanel.SetActive(false);
        errorCount = 0;
        initPosf1 = frac1.transform.parent.gameObject.transform.position;
        initPosf2 = frac2.transform.parent.gameObject.transform.position;
        initPosf3 = frac3.transform.parent.gameObject.transform.position;
        initPosf4 = frac4.transform.parent.gameObject.transform.position;
        initPosf5 = frac5.transform.parent.gameObject.transform.position;
        canFeed = true;
        checkUI.SetActive(false);
        frac1UI.transform.GetChild(0).gameObject.SetActive(false);
        frac1UI.transform.GetChild(1).gameObject.SetActive(false);
        frac2UI.transform.GetChild(0).gameObject.SetActive(false);
        frac2UI.transform.GetChild(1).gameObject.SetActive(false);
        frac3UI.transform.GetChild(0).gameObject.SetActive(false);
        frac3UI.transform.GetChild(1).gameObject.SetActive(false);
        frac3UI.transform.GetChild(2).gameObject.SetActive(false);
        frac3UI.transform.GetChild(3).gameObject.SetActive(false);
        firstObjectDestroyed = false;
        currFrac1 = new Fraction();
        currFrac2 = new Fraction();
        result = new Fraction();

        currResult = new Fraction();
        fracs.Add(frac1);
        fracs.Add(frac2);
        fracs.Add(frac3);
        fracs.Add(frac4);
        if (difficulty == DifficultyLevel.Hard)
        {
            maxCalification = 5;
            operations = 5;
            fracs.Add(frac5);
            operators.Add("div");
            operators.Add("-");
            operators.Add("+");
            operators.Add("x");
            operators.Add("-");
        }
        else if (difficulty == DifficultyLevel.Easy)
        {
            operators.Add("x");
            operators.Add("+");
            operators.Add("x");
            operators.Add("+");
        }
        else
        {
            operators.Add("+");
            operators.Add("-");
            operators.Add("x");
            operators.Add("div");
        }
        fracs.Add(bubbleFrac);
        operationsComplete = 0;
        operatorIndex = 0;
        /*foreach (Transform child in bubbleFrac.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in frac1.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in frac2.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in frac3.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in frac4.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in frac5.transform)
        {
            child.gameObject.SetActive(false);
        }*/
        Invoke("generateSublevel", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (PausaB.juegoPausado == true)
        {
            CursorController.setDefaultCursor();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

    }

    private void generateSublevel()
    {
        canFeed= true;
        currFrac1 = new Fraction(0, 0);
        currFrac2 = new Fraction(0, 0);
        result = new Fraction(0, 0);
        frac1UI.transform.GetChild(0).gameObject.SetActive(false);
        frac1UI.transform.GetChild(1).gameObject.SetActive(false);
        frac2UI.transform.GetChild(0).gameObject.SetActive(false);
        frac2UI.transform.GetChild(1).gameObject.SetActive(false);
        frac3UI.transform.GetChild(0).gameObject.SetActive(false);
        frac3UI.transform.GetChild(1).gameObject.SetActive(false);
        frac3UI.transform.GetChild(2).gameObject.SetActive(false);
        frac3UI.transform.GetChild(3).gameObject.SetActive(false);
        checkUI.gameObject.SetActive(false);
        frac1UI.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("0");
        frac1UI.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("0");
        frac2UI.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("0");
        frac2UI.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("0");
        //Reset positions
        if (difficulty == DifficultyLevel.Hard)
            frac5.transform.parent.gameObject.SetActive(true);
        frac1.transform.parent.transform.position = initPosf1;
        frac2.transform.parent.transform.position = initPosf2;
        frac3.transform.parent.transform.position = initPosf3;
        frac4.transform.parent.transform.position = initPosf4;
        if (difficulty == DifficultyLevel.Hard)
            frac5.transform.parent.transform.position = initPosf5;
        //Reset fracs
        frac1.transform.parent.gameObject.SetActive(true);
        frac2.transform.parent.gameObject.SetActive(true);
        frac3.transform.parent.gameObject.SetActive(true);
        frac4.transform.parent.gameObject.SetActive(true);
        if (operationsComplete < operations)
        {
            currOperator = operators[operatorIndex];
            Debug.Log(operatorIndex);
            possibleResults = GetRandomFractions(currOperator);
            OperatorUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(currOperator);
            currResult = possibleResults[possibleResults.Count - 1];
            int index = 0;
            foreach (GameObject frac in fracs)
            {
                if (possibleResults[index].numerator.ToString().Length > 1)
                {
                    frac.transform.GetChild(0).gameObject.SetActive(false);
                    frac.transform.GetChild(1).gameObject.SetActive(true);
                    frac.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].numerator.ToString()[0].ToString());
                    frac.transform.GetChild(1).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].numerator.ToString()[1].ToString());
                }
                else
                {
                    frac.transform.GetChild(0).gameObject.SetActive(true);
                    frac.transform.GetChild(1).gameObject.SetActive(false);
                    frac.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].numerator.ToString());
                }
                if (possibleResults[index].denominator.ToString().Length > 1)
                {
                    frac.transform.GetChild(2).gameObject.SetActive(false);
                    frac.transform.GetChild(3).gameObject.SetActive(true);
                    frac.transform.GetChild(3).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].denominator.ToString()[0].ToString());
                    frac.transform.GetChild(3).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].denominator.ToString()[1].ToString());
                }
                else
                {
                    frac.transform.GetChild(2).gameObject.SetActive(true);
                    frac.transform.GetChild(3).gameObject.SetActive(false);
                    frac.transform.GetChild(2).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].denominator.ToString());
                }
                index++;
            }
            
            operationsComplete++;
            operatorIndex++;
            //Mouth.operationComplete = false;
        }
        else
        {
            calification = (maxCalification - errorCount);
            notaFinal = calification;
            PausaB.juegoPausado = true;
            Time.timeScale = 0f;
            if ((notaFinal * 100) / maxCalification >= 60) { WinPanel.SetActive(true); }
            else { LosePanel.SetActive(true); }
            calification = (maxCalification  - errorCount);
            notaFinal = calification;
            Debug.Log("calificacion final: " + (notaFinal * 100) / maxCalification);
            if (difficulty == DifficultyLevel.Easy)
            {
                GameObject po = GameObject.Find("PlayerX");
                PlayerData pd = po.GetComponent<PlayerData>();
                pd.player.nivel = 3;
                pd.player.dificultad = 1;
                pd.player.calificacion = ((notaFinal * 100) / maxCalification);

                script.LevelComplete();
            }
            if (difficulty == DifficultyLevel.Medium)
            {
                GameObject po = GameObject.Find("PlayerX");
                PlayerData pd = po.GetComponent<PlayerData>();
                pd.player.nivel = 3;
                pd.player.dificultad = 2;
                pd.player.calificacion = ((notaFinal * 100) / maxCalification);
                script.LevelComplete();
            }
            if (difficulty == DifficultyLevel.Hard)
            {
                GameObject po = GameObject.Find("PlayerX");
                PlayerData pd = po.GetComponent<PlayerData>();
                pd.player.nivel = 3;
                pd.player.dificultad = 3;
                pd.player.calificacion = ((notaFinal * 100) / maxCalification);
                script.LevelComplete();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fracs") && canFeed)
        {
            crunch.Play();
            MonsterTongue.SetActive(true);
            other.gameObject.SetActive(false);
            string name = other.name;
            if (!firstObjectDestroyed)
            {
                frac1UI.transform.GetChild(0).gameObject.SetActive(true);
                frac1UI.transform.GetChild(1).gameObject.SetActive(true);
                frac1UI.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].numerator.ToString());
                frac1UI.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].denominator.ToString());
                currFrac1.numerator = possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].numerator;
                currFrac1.denominator = possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].denominator;
                firstObjectDestroyed = true;
            }
            else
            {
                frac2UI.transform.GetChild(0).gameObject.SetActive(true);
                frac2UI.transform.GetChild(1).gameObject.SetActive(true);
                frac2UI.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].numerator.ToString());
                frac2UI.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].denominator.ToString());
                currFrac2.numerator = possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].numerator;
                currFrac2.denominator = possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].denominator;
                firstObjectDestroyed = false;
                if (currOperator == "+")
                {
                    result = currFrac1 + currFrac2;
                }
                else if (currOperator == "-")
                {
                    result = currFrac1 - currFrac2;
                }
                else if (currOperator == "x")
                {
                    result = currFrac1 * currFrac2;
                }
                else if (currOperator == "div")
                {
                    result = currFrac1 / currFrac2;
                }
                if ((result).Equals(currResult))
                {
                    //Debug.Log("Correct" + result.ToString());
                    correct.Play();
                    checkUI.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("GreenCheck");
                }
                else
                {
                    //Debug.Log("Incorrect" + result.ToString());
                    incorrect.Play();
                    checkUI.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("RedCross");
                    errorCount++;
                }
                result.Simplify();
                if (result.numerator.ToString().Length > 1)
                {
                    frac3UI.transform.GetChild(0).gameObject.SetActive(false);
                    frac3UI.transform.GetChild(1).gameObject.SetActive(true);
                    frac3UI.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(result.numerator.ToString()[0].ToString());
                    frac3UI.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(result.numerator.ToString()[1].ToString());
                }
                else
                {
                    frac3UI.transform.GetChild(0).gameObject.SetActive(true);
                    frac3UI.transform.GetChild(1).gameObject.SetActive(false);
                    frac3UI.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(result.numerator.ToString());
                }
                if (result.denominator.ToString().Length > 1)
                {
                    frac3UI.transform.GetChild(2).gameObject.SetActive(false);
                    frac3UI.transform.GetChild(3).gameObject.SetActive(true);
                    frac3UI.transform.GetChild(3).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(result.denominator.ToString()[0].ToString());
                    frac3UI.transform.GetChild(3).transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(result.denominator.ToString()[1].ToString());
                }
                else
                {
                    frac3UI.transform.GetChild(2).gameObject.SetActive(true);
                    frac3UI.transform.GetChild(3).gameObject.SetActive(false);
                    frac3UI.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(result.denominator.ToString());
                }
                checkUI.gameObject.SetActive(true);
                currFrac1 = new Fraction(0, 0);
                currFrac2 = new Fraction(0, 0);
                result = new Fraction(0, 0);
                Invoke("generateSublevel", 5f);
                canFeed = false;
            }
            Invoke("disappearTongue", 0.3f);
        }
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
        Fraction frac4 = GetRandomFraction();
        Fraction fracResult = new Fraction(1, 1);
        if (op == "+")
        {
            fracResult = frac1 + frac2;
        }
        else if (op == "-")
        {
            fracResult = frac1 - frac2;
        }
        else if (op == "x")
        {
            fracResult = frac1 * frac2;
        }
        else if (op == "div")
        {
            fracResult = frac1 / frac2;
        }
        fracResult.Simplify();
        fractions.Add(frac1);
        fractions.Add(frac2);
        fractions.Add(frac3);
        fractions.Add(frac4);
        if (difficulty == DifficultyLevel.Hard)
        {
            Fraction frac = GetRandomFraction();
            fractions.Add(frac);
            frac5.transform.parent.gameObject.SetActive(true);
        }
        ShuffleList(fractions);
        fractions.Add(fracResult);
        Debug.Log("Right Answers: " + frac1.ToString() + op + frac2.ToString());
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

    private void disappearTongue()
    {
        MonsterTongue.SetActive(false);
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
        if(numerator == 0 || denominator == 0)
        {
            numerator = 0;
            denominator = 0;
        }
        else
        {
            int mcd = MCD(numerator, denominator);
            numerator /= mcd;
            denominator /= mcd;
        }
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
