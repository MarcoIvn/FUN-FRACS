using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    [SerializeField] private ControladorTiempo controlarTimpo;


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("source"))
        {
            controlarTimpo.DesactivarTemporizador();
        }
    }
}
