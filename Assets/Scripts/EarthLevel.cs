using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthLevel : MonoBehaviour
{
    public GameObject border, section1; //section2;
    float s1Points, s2Points;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        border.GetComponent<Renderer>().enabled = false;
        section1.GetComponent<Renderer>().enabled = false;
        //section2.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
