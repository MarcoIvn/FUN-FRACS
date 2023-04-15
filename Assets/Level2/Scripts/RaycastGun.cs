using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastGun : MonoBehaviour
{
    public Camera playerCamera;
    public Transform laserOrigin;
    public float gunRange = 80f;
    public float fireRate = 0.4f;
    public float laserDuration = 3f;
    public int asteroidsToDestroy; // Nuevo: cantidad de asteroides a destruir para ganar
    public AudioSource audioLaser;
    public AudioSource audioExplosion;

    LineRenderer laserLine;
    float fireTimer;
    public int asteroidsDestroyed = 0; // Nuevo: contador de asteroides destruidos

    //Win-Lose
    private bool activated = false;

    // Operaciones 
    public int num1;
    public int num2;
    public string operation;

    void Start()
    {
        // Generate random numbers
        num1 = Random.Range(1, 11);
        num2 = Random.Range(1, 11);

        // Generate random operation
        int opIndex = Random.Range(0, 4);
        switch (opIndex)
        {
            case 0:
                operation = "+";
                asteroidsToDestroy = num1 + num2;
                break;
            case 1:
                operation = "-";
                asteroidsToDestroy = num1 - num2;
                break;
            case 2:
                operation = "*";
                asteroidsToDestroy = num1 * num2;
                break;
            case 3:
                operation = "/";
                num1 *= num2; // multiply num1 to get a divisible number
                asteroidsToDestroy = num1 / num2;
                break;
        }

        // Print the math problem
        // Debug.Log(num1 + " " + operation + " " + num2 + " = ? " + asteroidsToDestroy);
    }

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }
    // Poner un nuevo asset, para que al entrar a el haga un check point y diga: has ganado, has perdido.
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        {
            audioLaser.Play(); //laser
            audioLaser.volume = 0.2f;
            fireTimer = 0;
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOring = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(rayOring, playerCamera.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                if (hit.transform.gameObject.CompareTag("Asteroid")) // Destruir asteroides
                {
                    Destroy(hit.transform.gameObject);
                    asteroidsDestroyed++;
                    audioExplosion.Play(); //explosion
                    audioExplosion.volume = 1f;
                }
                
            }
            else
            {
                laserLine.SetPosition(1, rayOring + (playerCamera.transform.forward * gunRange));
            }
            StartCoroutine(ShoorLaser());
        }
    }

    IEnumerator ShoorLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}

