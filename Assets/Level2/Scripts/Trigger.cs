using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
    private bool activated = false;
    public SendToServer script;
    public GameObject win;
    public GameObject lose;

    public RaycastGun gunScript; // agregar una referencia a RaycastGun

    // Start is called before the first frame update
    void Start()
    {
        // asignar el valor de la instancia de RaycastGun a gunScript
        gunScript = FindObjectOfType<RaycastGun>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "source")
        {
            Debug.Log("Entraste a la nave nodrisa");
            if (!activated)
            {
                activated = true;
                if (gunScript.asteroidsDestroyed == gunScript.asteroidsToDestroy) // Nuevo: si se han destruido suficientes asteroides, imprimir "You Win" en la consola
                {
                    Debug.Log("Asteroids destruidos: " + gunScript.asteroidsDestroyed);
                    Debug.Log("You Win");
                    Time.timeScale = 0f;
                    win.SetActive(true);
                    //Invoke("CargarSiguienteEscena", 5.0f);

                    GameObject po = GameObject.Find("PlayerX");
                    PlayerData pd = po.GetComponent<PlayerData>();
                    pd.player.nivel = 2;
                    pd.player.dificultad = 1;
                    pd.player.calificacion = 100;
                    script.LevelComplete();
                }
                else  // "You lose" en la consola
                {
                    Debug.Log("Asteroids destruidos: " + gunScript.asteroidsDestroyed);
                    Debug.Log("You lose");
                    Time.timeScale = 0f;
                    lose.SetActive(true);
                    //Invoke("CargarMismaEscena", 5.0f);

                    GameObject po = GameObject.Find("PlayerX");
                    PlayerData pd = po.GetComponent<PlayerData>();
                    pd.player.nivel = 1;
                    pd.player.dificultad = 3;
                    pd.player.calificacion = gunScript.asteroidsDestroyed * 100 / gunScript.asteroidsToDestroy;
                    script.LevelComplete();
                    Debug.Log("Calificacion: " + pd.player.calificacion);
                }
            }
        }
    }

    private void CargarSiguienteEscena()
    {
        SceneManager.LoadScene("VenusMedium");
    }

    private void CargarMismaEscena()
    {
        SceneManager.LoadScene("VenusEasy");
    }
}
