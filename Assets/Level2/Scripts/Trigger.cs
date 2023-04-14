using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private bool activated = false;

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
                }
                else  // "You lose" en la consola
                {
                    Debug.Log("Asteroids destruidos: " + gunScript.asteroidsDestroyed);
                    Debug.Log("You lose");
                }
            }
        }
    }
    
}
