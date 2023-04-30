using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEarthLevel : MonoBehaviour
{
    [SerializeField]
    public AudioSource audioCorrect;
    public AudioSource audioIncorrect;
    private Transform spawnPoint;
    public Camera cam;
    /*[SerializeField]
    private Portal target;
    public Transform SpawnPoint { get { return spawnPoint; } }*/
    public static int asteroidCount = 0, bluePlanetCount = 0, pinkPlanetCount = 0, yellowStarCount = 0, blueAlienCount = 0, purpleCoinCount = 0;
    public static int errorCount = 0;
    public static int correctObjetcs = 0;


    // Start is called before the first frame update
    void Start()
    {
        correctObjetcs = 0;
        asteroidCount = 0; bluePlanetCount = 0; pinkPlanetCount = 0; yellowStarCount = 0; blueAlienCount = 0; purpleCoinCount = 0;
        errorCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(ControladorTiempo.tiempoActivado == true)
        {
            if (other.CompareTag("Player"))
            {
                /*cam.transform.position = spawnPoint.position;
                other.transform.parent.position = spawnPoint.position;*/
            }
            else if (other.CompareTag(EarthLevel.currObj))
            {
                audioCorrect.Play();
                EarthLevel.currObjAmount--;
                correctObjetcs++;
                Destroy(other.gameObject);
            }
            else
            {
                audioIncorrect.Play();
                errorCount++;
                Destroy(other.gameObject);
            }
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            Debug.Log("Found Player");
        
    }*/
}
