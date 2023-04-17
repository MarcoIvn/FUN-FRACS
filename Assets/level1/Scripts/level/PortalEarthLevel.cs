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
        } 
        else if (other.CompareTag("Asteroid"))
        {
            asteroidCount += 1;
            Destroy(other.gameObject);
        }else if (other.CompareTag("Planet"))
        {
            if(other.name == "BluePlanet")
                bluePlanetCount+= 1;
            else if(other.name == "PinkPlanet")
                pinkPlanetCount+= 1;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Star"))
        {
            yellowStarCount+= 1;
            Destroy(other.gameObject);

        }
        else if (other.CompareTag("Alien"))
        {
            blueAlienCount+= 1;
            Destroy(other.gameObject);

        }
        else if (other.CompareTag("Coin"))
        {
            purpleCoinCount+= 1;
            Destroy(other.gameObject);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            Debug.Log("Found Player");
        
    }*/
}
