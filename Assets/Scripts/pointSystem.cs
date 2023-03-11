using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointSystem : MonoBehaviour
{
    private float r1, r2;
    public GameObject section1, section2, section3, section4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        r1 = GetComponent<SphereCollider>().radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z); ;
        r2 = section1.GetComponent<SphereCollider>().radius * Mathf.Max(section1.transform.lossyScale.x, section1.transform.lossyScale.y, section1.transform.lossyScale.z); ;
        if (Vector3.Distance(transform.position, section1.transform.position) < (r1 + r2))
            Debug.Log("Touching");
    }
}
