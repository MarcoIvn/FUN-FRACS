using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goback : MonoBehaviour
{
    
    // Start is called before the first frame update
    public Button home;
    public void CambiarEscena() {
        
        SceneManager.LoadScene("LevelSelector"); }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
