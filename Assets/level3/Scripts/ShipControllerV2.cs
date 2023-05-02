using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControllerV2 : MonoBehaviour
{
    public float velocidad = 10.0f; //Velocidad de movimiento de la nave
    public float rotacion = 80.0f; //Velocidad de rotaci�n de la nave
    public float sensibilidadCamara = 1.0f; //Sensibilidad de rotaci�n de la c�mara

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //Obtiene la entrada del teclado en horizontal
        float vertical = Input.GetAxis("Vertical"); //Obtiene la entrada del teclado en vertical
        float mouseVertical = Input.GetAxis("Mouse Y"); //Obtiene la entrada del rat�n en vertical
        transform.Rotate(Vector3.left, mouseVertical * sensibilidadCamara * Time.deltaTime); //Rota la c�mara hacia arriba o hacia abajo

        transform.Translate(Vector3.right * horizontal * velocidad * Time.deltaTime); //Mueve la nave horizontalmente
        transform.Translate(Vector3.forward * vertical * velocidad * Time.deltaTime); //Mueve la nave hacia adelante y hacia atr�s

        float mouseHorizontal = Input.GetAxis("Mouse X"); //Obtiene la entrada del rat�n en horizontal
        transform.Rotate(Vector3.up, mouseHorizontal * rotacion * Time.deltaTime); //Rota la nave horizontalmente con el rat�n
    }

}
