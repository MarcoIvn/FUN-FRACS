using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorTiempo : MonoBehaviour
{
    
    [SerializeField] private float tiempoMaximo;
    [SerializeField] private Slider slider;
    public float tiempoActual;
    public static bool tiempoActivado = false;
    public GameObject LosePanel;

    /*
    private void Start()
    {
        ActivarTemporizador();
    }
    */

    private void Update()
    {
        if (tiempoActivado)
        {
            CambiarContador();
        }
    }

    private void CambiarContador()
    {
        tiempoActual -= Time.deltaTime;
        if (tiempoActual >= 0)
        {
            slider.value = tiempoActual;
        }
        if (tiempoActual <= 0)
        {
            Time.timeScale = 0f;
            CursorController.setDefaultCursor();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            LosePanel.SetActive(true);
            Debug.Log("Derrota por tiempo");
            CambiarTemporizador(false);
        }
    }

    public void CambiarTemporizador(bool estado)
    {
        tiempoActivado = estado;
    }

    
    public void ActivarTemporizador()
    {
        tiempoActual = tiempoMaximo;
        slider.maxValue = tiempoMaximo;
        CambiarTemporizador(true);
    }

    public void DesactivarTemporizador()
    {
        CambiarTemporizador(false);
    }
    
}
