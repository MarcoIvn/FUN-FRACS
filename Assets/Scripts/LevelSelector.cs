using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] Transform levelContainer;
    List<GameObject> levels = new List<GameObject>();
    public GameObject canvasObject;
    private GameObject currentLevel => levels[currentIndex];
    private GameObject planetMenu, mainMenu;
    private int currentIndex = 0;
    private bool laClicked, raClicked,startClicked, difficultyClicked;
    public TMP_Text planetText;
    public GameObject generalView;
    public Button leftArrow, rightArrow, startButton, easyBtn, mediumBtn, hardBtn;
    private string levelChoice;


    // Start is called before the first frame update
    private void Start()
    {
    }
    private void Awake()
    {
        startClicked = false;
        //No level still choiced
        levelChoice = "";
        //Reference Planet Menu
        mainMenu = canvasObject.transform.Find("Main Menu").gameObject;
        planetMenu = canvasObject.transform.Find("Planet Menu").gameObject;
        //Add function when right of left arrow is clicked
        leftArrow.onClick.AddListener(taskOnClickla);
        rightArrow.onClick.AddListener(taskOnClickra);
        startButton.onClick.AddListener(taskOnClickStart);
        easyBtn.onClick.AddListener(taskOnClickEasy);
        mediumBtn.onClick.AddListener(taskOnClickMedium);
        hardBtn.onClick.AddListener(taskOnClickHard);
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
        if (startClicked == true) //Change this in the future
        { 
            generalView.SetActive(false);
            planetMenu.SetActive(true);
            mainMenu.SetActive(false);
            changePlanetText(currentLevel);
            //startClicked = false;
        }
        if(difficultyClicked == true)
        {
            difficultyClicked = false;
            LoadLevel(currentLevel, levelChoice);
        }

        if (levels.Count == 0)
            return;
        if ((Input.GetKeyDown(KeyCode.RightArrow) || raClicked == true) && startClicked == true)
        {
            SelectNextLevel();
            planetMenu.SetActive(true);
            changePlanetText(currentLevel);
            raClicked = false;
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || laClicked == true) && startClicked == true)
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
            planetText.text = "Tierra";
        else if (currentLevel.name == "Mercury2")
            planetText.text = "Mercurio";
        else if (currentLevel.name == "Mars3")
            planetText.text = "Marte";
        else if (currentLevel.name == "Venus4")
            planetText.text = "Venus";
        else if (currentLevel.name == "Uran5")
            planetText.text = "Urano";
        else if (currentLevel.name == "Jupiter6")
            planetText.text = "Jupiter";
        else if (currentLevel.name == "Neptune7")
            planetText.text = "Neptuno";
        else if (currentLevel.name == "Saturn8")
            planetText.text = "Saturno";
    }
    private void LoadLevel(GameObject currentLevel , string levelChoice)
    {
        string sceneToLoad;
        //Disable the level selector to avoid further interaction
        this.enabled = false;
        // Load scene
        if (currentLevel.name == "Earth1")
        {
            sceneToLoad = "Assets/Scenes/EarthLevels/" + "Earth" + levelChoice +".unity";
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else if (currentLevel.name == "Mercury2")
        {
            sceneToLoad = "Assets/Scenes/MercuryLevels/" + "Mercury" + levelChoice + ".unity";
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
            
        else if (currentLevel.name == "Mars3")
        {
            sceneToLoad = "Assets/Scenes/MarsLevels/" + "Mars" + levelChoice + ".unity";
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else if (currentLevel.name == "Venus4")
        {
            sceneToLoad = "Assets/Scenes/VenusLevels/" + "Venus" + levelChoice + ".unity";
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else if (currentLevel.name == "Uran5")
        {
            sceneToLoad = "Assets/Scenes/UranLevels/" + "Uran" + levelChoice + ".unity";
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else if (currentLevel.name == "Jupiter6")
        {
            sceneToLoad = "Assets/Scenes/JupiterLevels/" + "Jupiter" + levelChoice + ".unity";
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else if (currentLevel.name == "Neptune7")
        {
            sceneToLoad = "Assets/Scenes/NeptuneLevels/" + "Neptune" + levelChoice + ".unity";
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else if (currentLevel.name == "Saturn8")
        {
            sceneToLoad = "Assets/Scenes/SaturnLevels/" + "Saturn" + levelChoice + ".unity";
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
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

    private void taskOnClickStart()
    {
        startClicked = true;
    }

    private void taskOnClickEasy()
    {
        levelChoice = "Easy";
        difficultyClicked = true;
    }

    private void taskOnClickMedium()
    {
        levelChoice = "Medium";
        difficultyClicked = true;
    }

    private void taskOnClickHard()
    {
        levelChoice = "Hard";
        difficultyClicked = true;
    }
}
