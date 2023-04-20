using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerH : MonoBehaviour
{
    
    private bool activated = false;

    public GameObject win;
    public GameObject lose;

    public RaycastGunH gunScript; // agregar una referencia a RaycastGun

    // Start is called before the first frame update
    void Start()
    {
        // asignar el valor de la instancia de RaycastGun a gunScript
        gunScript = FindObjectOfType<RaycastGunH>();
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
                    //!
                    GameObject po = GameObject.Find("PlayerX");
                    PlayerData pd = po.GetComponent<PlayerData>();
                    pd.player.nivel = 3;
                    pd.player.dificultad = 1;
                    pd.player.calificacion = 100;
                    //!
                    win.SetActive(true);
                    Invoke("CargarNuevaEscena", 5.0f);
                }
                else  // "You lose" en la consola
                {
                    Debug.Log("Asteroids destruidos: " + gunScript.asteroidsDestroyed);
                    Debug.Log("You lose");
                    //!
                    GameObject po = GameObject.Find("PlayerX");
                    PlayerData pd = po.GetComponent<PlayerData>();
                    pd.player.nivel = 3;
                    pd.player.dificultad = 1;
                    pd.player.calificacion = gunScript.asteroidsDestroyed * 100 / gunScript.asteroidsToDestroy;
                    
                    Debug.Log("Calificacion: " + pd.player.calificacion);
                    //!
                    lose.SetActive(true);
                    Invoke("CargarMismaEscena", 5.0f);
                }
            }
        }
    }

    private void CargarNuevaEscena()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    private void CargarMismaEscena()
    {
        SceneManager.LoadScene("VenusHard");
    }
}