using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaEsceanas : MonoBehaviour
{

    [SerializeField] private GameObject Lose;

    [SerializeField] private GameObject Win;

    public int NumEscenaSig;

    // Start is called before the first frame update
    public void Restart()
    {
        Time.timeScale = 0f;
        Win.SetActive(true);
        Lose.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CursorController.setDefaultCursor();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Siguiente()
    {
        Time.timeScale = 0f;
        Win.SetActive(false);
        Lose.SetActive(true);
        SceneManager.LoadScene(NumEscenaSig);
        CursorController.setDefaultCursor();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
