using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEarthLevel : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    public Camera cam;
    /*[SerializeField]
    private Portal target;
    public Transform SpawnPoint { get { return spawnPoint; } }*/
    public static int asteroidCount = 0, bluePlanetCount = 0, pinkPlanetCount = 0, yellowStarCount = 0, blueAlienCount = 0, purpleCoinCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /*cam.transform.position = spawnPoint.position;
            other.transform.parent.position = spawnPoint.position;*/
        }else if (other.CompareTag(EarthLevel.currObj))
        {
            EarthLevel.currObjAmount--;
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("ERROR");
            Destroy(other.gameObject);
        }

        /*else if (other.CompareTag("Asteroid"))
        {
            if(EarthLevel.currObj == "Asteroid_Gray") {
                EarthLevel.currObjAmount--;
                Destroy(other.gameObject);
                Debug.Log("Asteroid Found");
            }
            else
            {
                Debug.Log("ERROR");
                Destroy(other.gameObject);
            }
            
        }else if (other.CompareTag("Planet"))
        {
            if(other.name == "BluePlanet(Clone)")
            {
                if(EarthLevel.currObj == "Planet_Blue")
                {
                    EarthLevel.currObjAmount--;
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.Log("ERROR");
                    Destroy(other.gameObject);
                }

            }    
            else if(other.name == "PinkPlanet(Clone)" )
                if(EarthLevel.currObj == "Planet_Pink")
                {
                    EarthLevel.currObjAmount--;
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.Log("ERROR");
                    Destroy(other.gameObject);
                }
            Debug.Log("Planet Found");
        }
        else if (other.CompareTag("Star") && EarthLevel.currObj == "Star_Yellow")
        {
            if()
            EarthLevel.currObjAmount--;
            Destroy(other.gameObject);
            Debug.Log("Star Found");
        }
        else if (other.CompareTag("Alien") && EarthLevel.currObj == "Alien_Blue")
        {
            EarthLevel.currObjAmount--;
            Destroy(other.gameObject);
            Debug.Log("Alien Found");
        }
        else if (other.CompareTag("Coin") && EarthLevel.currObj == "Coin_Purple")
        {
            EarthLevel.currObjAmount--;
            Destroy(other.gameObject);
            Debug.Log("Coin Found");
        }*/
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            Debug.Log("Found Player");
        
    }*/
}
