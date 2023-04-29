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
    public static bool tiempoAcabado = false;

    
    private void Start()
    {
        tiempoActivado = false;
        tiempoAcabado = false;
        //ActivarTemporizador();
        LosePanel.SetActive(false);

    }
    

    private void Update()
    {
        if(!tiempoAcabado){
            if (tiempoActivado)
            {
                CambiarContador();
            }
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
            tiempoAcabado = true;
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
