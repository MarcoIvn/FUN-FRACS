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
                Destroy(hit.transform.gameObject);
                asteroidsDestroyed++;
                /*
                if (asteroidsDestroyed >= asteroidsToDestroy) // Nuevo: si se han destruido suficientes asteroides, imprimir "You Win" en la consola
                {
                    Debug.Log("You Win");
                }
                else  // Nuevo: si se han destruido suficientes asteroides, imprimir "You Win" en la consola
                {
                    Debug.Log("You lose");
                }
                */
                audioExplosion.Play(); //explosion
                audioExplosion.volume = 1f;
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
