using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public AudioSource audioLaser;
    public float raycastDistance = 5000f; // la distancia del raycast
    public float objectDistance = 130f; // la distancia a la que se puede arrastrar el planeta
    public LayerMask layerMask; // la capa de objetos con la que colisionar� el raycast

    private bool isFiring = false; // indica si el raycast est� activo
    private GameObject currentObject = null; // el planeta que se est� arrastrando actualmente

    void Update()
    {
        // Comprueba si el bot�n izquierdo del rat�n est� pulsado
        if (Input.GetMouseButton(0))
        {
            isFiring = true; // activa el raycast

        }
        else
        {
            isFiring = false; // desactiva el raycast

            if (currentObject != null)
            {
                // Detiene el movimiento del objeto que estaba siendo arrastrado
                Rigidbody objectRb = currentObject.GetComponent<Rigidbody>();
                if (objectRb != null)
                {
                    objectRb.velocity = Vector3.zero;
                    objectRb.angularVelocity = Vector3.zero;
                }
            }
            currentObject = null; // limpia la variable del objeto arrastrado
        }
        if (Input.GetMouseButtonDown(0))
        {
            audioLaser.Play(); //laser
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            audioLaser.Stop(); //laser

        }

    }

    void FixedUpdate()
    {
        // Comprueba si el raycast est� activo
        if (isFiring)
        {
            RaycastHit hit;
            // Genera el raycast desde la posici�n de la nave en la direcci�n en la que est� mirando
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, layerMask))
            {

                
                Debug.DrawLine(transform.position, hit.point, Color.red); // Dibuja el raycast
                                                                          // Comprueba la etiqueta del objeto colisionado
                //if (hit.collider.gameObject.CompareTag("Planet") || hit.collider.CompareTag("Asteroid") || hit.collider.CompareTag("Star") || hit.collider.CompareTag("Coin") || hit.collider.CompareTag("Rocket") || hit.collider.CompareTag("Ufo") || hit.collider.CompareTag("Alien"))
                //{
                    currentObject = hit.collider.gameObject;
                    // Calcula la direcci�n y la velocidad a la que se mover� el objeto
                    Vector3 objectDirection = hit.point - transform.position;
                    Vector3 objectVelocity = transform.forward * objectDirection.magnitude * 2f;
                    // Limita la velocidad para evitar que el objeto se mueva demasiado r�pido
                    if (objectVelocity.magnitude > 20f)
                    {
                        objectVelocity = objectVelocity.normalized * 20f;
                    }
                    // Aplica la velocidad al planeta
                    Rigidbody planetRb = currentObject.GetComponent<Rigidbody>();
                    if (planetRb != null)
                    {
                        planetRb.velocity = objectVelocity;
                    }
                    // Actualiza la posici�n del objeto para que siempre est� a una distancia constante de la nave
                    Vector3 objectPosition = transform.position + transform.forward * objectDistance;
                    currentObject.transform.position = objectPosition;
                //}

            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + transform.forward * raycastDistance, Color.green); // Dibuja el raycast
                                                                                                                           // Si el raycast no colisiona con nada, detiene el movimiento del objeto arrastrado
                if (currentObject != null)
                {
                    Rigidbody objectRb = currentObject.GetComponent<Rigidbody>();
                    if (objectRb != null)
                    {
                        objectRb.velocity = Vector3.zero;
                        objectRb.angularVelocity = Vector3.zero;
                    }
                }
                currentObject = null; // limpia la variable del objeto arrastrado
            }
        }
    }
}
