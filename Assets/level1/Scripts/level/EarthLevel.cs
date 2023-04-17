using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EarthLevel : MonoBehaviour
{
    public enum DifficultyLevel { Easy, Medium, Hard }

    public DifficultyLevel difficultyLevel;
    public int minListSize;
    public int maxListSize;

    private readonly string[] tags = { "Asteroid", "Planet", "Star", "Coin", "Alien" };
    private readonly string[] colors = { "Green", "Orange", "Blue", "Pink", "Yellow", "Purple" };
    private readonly int[] maxCounts = { 6, 5, 4, 4, 4 }; // max count for each tag

    private List<string> objects = new List<string>();
    private string[] spriteArrays = {"","","","","",""};
    public GameObject objectsUI;

    private int checkAmount = 0;
    private int indexGame = 0;

    private void Start()
    {
        int listSize = Random.Range(minListSize, maxListSize + 1);
        

        switch (difficultyLevel)
        {
            case DifficultyLevel.Easy:
                objects.Add("Asteroid_Gray_" + Random.Range(1, maxCounts[GetTagIndex("Asteroid")] + 1));
                objects.Add("Planet_Blue_" + Random.Range(1, maxCounts[GetTagIndex("Planet")] + 1));
                objects.Add("Planet_Pink_" + Random.Range(1, maxCounts[GetTagIndex("Planet")] + 1));
                objects.Add("Star_Yellow_" + Random.Range(1, maxCounts[GetTagIndex("Star")] + 1));
                objects.Add("Coin_Purple_" + Random.Range(1, maxCounts[GetTagIndex("Coin")] + 1));
                objects.Add("Alien_Blue_" + Random.Range(1, maxCounts[GetTagIndex("Alien")] + 1));
                break;

            case DifficultyLevel.Medium:
                objects.Add("Asteroid_Gray_" + Random.Range(1, maxCounts[GetTagIndex("Asteroid")] + 1));
                objects.Add("Planet_Blue_" + Random.Range(1, maxCounts[GetTagIndex("Planet")] + 1));
                objects.Add("Planet_Pink_" + Random.Range(1, maxCounts[GetTagIndex("Planet")] + 1));
                objects.Add("Star_" + colors[Random.Range(4, 6)] + "_" + Random.Range(1, maxCounts[GetTagIndex("Star")] + 1));
                objects.Add("Coin_Purple_" + Random.Range(1, maxCounts[GetTagIndex("Coin")] + 1));
                objects.Add("Alien_" + colors[Random.Range(1, 3)] + "_" + Random.Range(1, maxCounts[GetTagIndex("Alien")] + 1));
                break;

            case DifficultyLevel.Hard:
                for (int i = 0; i < 2; i++)
                {
                    List<string> tempList = new List<string>();
                    tempList.Add("Asteroid_Gray_" + Random.Range(1, maxCounts[GetTagIndex("Asteroid")] + 1));
                    tempList.Add("Planet_Blue_" + Random.Range(1, maxCounts[GetTagIndex("Planet")] + 1));
                    tempList.Add("Planet_Pink_" + Random.Range(1, maxCounts[GetTagIndex("Planet")] + 1));
                    tempList.Add("Star_" + colors[Random.Range(4, 6)] + "_" + Random.Range(1, maxCounts[GetTagIndex("Star")] + 1));
                    tempList.Add("Coin_Purple_" + Random.Range(1, maxCounts[GetTagIndex("Coin")] + 1));
                    tempList.Add("Alien_" + colors[Random.Range(1, 3)] + "_" + Random.Range(1, maxCounts[GetTagIndex("Alien")] + 1));
                    objects.AddRange(tempList);
                }
                break;
        }

        while (objects.Count < listSize)
        {
            string tag = tags[Random.Range(0, tags.Length)];
            int maxCount = maxCounts[GetTagIndex(tag)];

            if (maxCount > 0 && CountObjects(objects, tag) < maxCount)
            {
                string color = (tag == "Asteroid") ? "verde" : colors[Random.Range(0, colors.Length)];
                int count = Random.Range(1, maxCount - CountObjects(objects, tag) + 1);
                objects.Add(tag + "_" + color + "_" + count);
            }
        }

        ShuffleList(objects);
        foreach (string obj in objects)
        {
            Debug.Log(obj);
        }

        List<GameObject> tagObjs = new List<GameObject>();
        int ind = 0;
        foreach (Transform child in objectsUI.transform)
        {
            int count = 0;
            string newSprite = "";
            foreach (char c in objects[ind])
            {
                if (c == '_')
                {
                    count++;
                    if (count == 2)
                    {
                        break;
                    }
                }
                newSprite += c;
            }
            Debug.Log(newSprite);
            spriteArrays[ind] = newSprite;
            child.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(newSprite);
            child.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = objects[ind][objects[ind].Length - 1].ToString();
            ind++;
        }
    }

    private void Update()
    {
        int currObjAmount = objects[indexGame][objects[indexGame].Length - 1];
        Debug.Log(currObjAmount);
        string currObj = spriteArrays[indexGame];
        if (currObj == "Asteroid_Gray")
        {
            checkAmount = PortalEarthLevel.asteroidCount;
            PortalEarthLevel.blueAlienCount= 0;
            PortalEarthLevel.pinkPlanetCount = 0;
            PortalEarthLevel.yellowStarCount= 0;
            PortalEarthLevel.pinkPlanetCount= 0;
            PortalEarthLevel.purpleCoinCount= 0;
        }
        else if (currObj == "Planet_Blue")
        {
            checkAmount = PortalEarthLevel.bluePlanetCount;
            PortalEarthLevel.blueAlienCount = 0;
            PortalEarthLevel.pinkPlanetCount = 0;
            PortalEarthLevel.yellowStarCount = 0;
            PortalEarthLevel.pinkPlanetCount = 0;
            PortalEarthLevel.purpleCoinCount = 0;
        }
 
        else if (currObj == "Planet_Pink")
        {
            checkAmount = PortalEarthLevel.pinkPlanetCount;
            PortalEarthLevel.blueAlienCount = 0;
            PortalEarthLevel.asteroidCount = 0;
            PortalEarthLevel.yellowStarCount = 0;
            PortalEarthLevel.pinkPlanetCount = 0;
            PortalEarthLevel.purpleCoinCount = 0;
        }
   
        else if (currObj == "Star_Yellow")
        {
            checkAmount = PortalEarthLevel.yellowStarCount;
            PortalEarthLevel.blueAlienCount = 0;
            PortalEarthLevel.pinkPlanetCount = 0;
            PortalEarthLevel.asteroidCount = 0;
            PortalEarthLevel.pinkPlanetCount = 0;
            PortalEarthLevel.purpleCoinCount = 0;
        }
        
        else if (currObj == "Coin_purple")
        {
            checkAmount = PortalEarthLevel.purpleCoinCount;
            PortalEarthLevel.blueAlienCount = 0;
            PortalEarthLevel.pinkPlanetCount = 0;
            PortalEarthLevel.yellowStarCount = 0;
            PortalEarthLevel.pinkPlanetCount = 0;
            PortalEarthLevel.asteroidCount = 0;
        }

        else if (currObj == "Alien_Blue")
        {
            checkAmount = PortalEarthLevel.blueAlienCount;
            PortalEarthLevel.asteroidCount = 0;
            PortalEarthLevel.pinkPlanetCount = 0;
            PortalEarthLevel.yellowStarCount = 0;
            PortalEarthLevel.pinkPlanetCount = 0;
            PortalEarthLevel.purpleCoinCount = 0;
        }
        if(currObjAmount != 0)
        {
            currObjAmount -= checkAmount;
            objectsUI.transform.GetChild(indexGame).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currObjAmount.ToString();
            if(checkAmount == 1)
                checkAmount = 0;
        }
        else
        {
            indexGame++;
        }
    }

    private int GetTagIndex(string tag)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (tags[i] == tag)
            {
                return i;
            }
        }

        return -1;
    }

    private int CountObjects(List<string> objects, string tag)
    {
        int count = 0;

        foreach (string obj in objects)
        {
            if (obj.StartsWith(tag))
            {
                count++;
            }
        }

        return count;
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





