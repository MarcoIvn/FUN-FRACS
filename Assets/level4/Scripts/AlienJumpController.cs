using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienJumpController : MonoBehaviour
{
    public float velocidadRotacion = 45f;
    public float velocidadSalto = 10f;
    private bool isJumping = false;
    private Rigidbody rb;
    public GameObject runWay;
    private int platformIndex = -1;
    private bool isLeft = false, isRight = false, firstPlat = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        platformIndex = -1;
        isLeft = false;
        isRight = false;
        firstPlat = true;
    }

    void Update()
    {
        // Gira el objeto si se presiona la flecha izquierda
        if (!isJumping)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (isLeft)
                    platformIndex++;
                else if(isRight)
                    platformIndex = platformIndex + 2;
                else if (firstPlat)
                {
                    platformIndex++;
                    firstPlat = false;
                }
                
                isLeft = true;
                isRight = false;
                isJumping = true;
                transform.Rotate(Vector3.up, -velocidadRotacion);
                //rb.AddForce(Vector3.up * velocidadSalto, ForceMode.Impulse);
                rb.AddForce(transform.forward * velocidadSalto + Vector3.up * velocidadSalto, ForceMode.Impulse);
                transform.position = runWay.transform.GetChild(platformIndex).gameObject.transform.position;
            }

            // Gira el objeto si se presiona la flecha derecha
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (isLeft)
                    platformIndex = platformIndex + 2;
                else if(isRight)
                    platformIndex++;
                else if (firstPlat)
                {
                    platformIndex = platformIndex + 2;
                    firstPlat = false;
                }
                isRight = true;
                isLeft = false;
                isJumping = true;
                transform.Rotate(Vector3.up, velocidadRotacion);
                //rb.AddForce(Vector3.up * velocidadSalto, ForceMode.Impulse);
                rb.AddForce(transform.forward * velocidadSalto + Vector3.up * velocidadSalto, ForceMode.Impulse);
                transform.position = runWay.transform.GetChild(platformIndex).gameObject.transform.position;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Gira el objeto a su orientación inicial si colisiona con la plataforma
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.rotation = Quaternion.identity;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 180, transform.eulerAngles.z);
            isJumping = false;
        }
    }
}
