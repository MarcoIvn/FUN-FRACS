using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PausaB : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;

    [SerializeField] private GameObject menuPausa;

    public static bool juegoPausado = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado) { Reanudar(); }
            else { Pausa(); }
        }
    }
    public void Pausa() {
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        CursorController.setDefaultCursor();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Reanudar()
    {
        Time.timeScale = 1f;
        juegoPausado = false;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);



    }
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("LevelSelector");



    }


}
