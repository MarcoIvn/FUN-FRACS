using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] Transform levelContainer;
    List<GameObject> levels = new List<GameObject>();
    public GameObject canvasObject;
    private GameObject currentLevel => levels[currentIndex];
    private GameObject planetMenu, mainMenu;
    private int currentIndex = 0;
    private bool laClicked, raClicked;
    public TMP_Text planetText;
    public GameObject generalView;
    public Button leftArrow, rightArrow;
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    private void Awake()
    {
        //Reference Planet Menu
        mainMenu = canvasObject.transform.Find("Main Menu").gameObject;
        planetMenu = canvasObject.transform.Find("Planet Menu").gameObject;
        //Add function when right of left arrow is clicked
        leftArrow.onClick.AddListener(taskOnClickla);
        rightArrow.onClick.AddListener(taskOnClickra);
        //Add all children of the level container to the list of elements
        for (int i = 0; i < levelContainer.childCount; i++)
            levels.Add(levelContainer.GetChild(i).gameObject);
        //Disable all elements
        foreach (var level in levels)
            level.SetActive(false);
        //Enable first level of element
        if(levels.Count > 0)
        {
            currentIndex = 0;
            currentLevel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) //Change this in the future
        { 
            generalView.SetActive(false);
            planetMenu.SetActive(true);
            mainMenu.SetActive(false);
            changePlanetText(currentLevel);
        }
        if (levels.Count == 0)
            return;
        if (Input.GetKeyDown(KeyCode.RightArrow) || raClicked == true)
        {
            SelectNextLevel();
            planetMenu.SetActive(true);
            changePlanetText(currentLevel);
            raClicked = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || laClicked == true)
        {
            SelectPreviousLevel();
            planetMenu.SetActive(true);
            changePlanetText(currentLevel);
            laClicked = false;
        }  
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Level " + currentLevel.name);
        }
    }

    private void SelectNextLevel()
    {
        currentLevel.SetActive(false);
        currentIndex = Mathf.Min(levels.Count - 1, currentIndex + 1);
        currentLevel.SetActive(true);
    }

    private void SelectPreviousLevel()
    {
        currentLevel.SetActive(false);
        currentIndex = Mathf.Max(0, currentIndex - 1);
        currentLevel.SetActive(true);
    }

    private void changePlanetText(GameObject currentLevel) /////
    {
        if (currentLevel.name == "Earth1")
            planetText.text = "Earth";
        else if (currentLevel.name == "Mercury2")
            planetText.text = "Mercury";
        else if (currentLevel.name == "Mars3")
            planetText.text = "Mars";
        else if (currentLevel.name == "Venus4")
            planetText.text = "Venus";
        else if (currentLevel.name == "Uran5")
            planetText.text = "Uran";
        else if (currentLevel.name == "Jupiter6")
            planetText.text = "Jupiter";
        else if (currentLevel.name == "Neptune7")
            planetText.text = "Neptune";
        else if (currentLevel.name == "Saturn8")
            planetText.text = "Saturn";
    }
    private void LoadLevel(GameObject currentLevel)
    {
        //Disable the level selector to avoid further interaction
        this.enabled = false;
        // Load scene

        //
    }

    private void taskOnClickla()
    {
        laClicked = true;
    }

    private void taskOnClickra()
    {
        raClicked = true;
    }
}
