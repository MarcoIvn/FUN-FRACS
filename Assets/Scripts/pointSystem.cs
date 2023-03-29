using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointSystem : MonoBehaviour
{
    private float r1, r2, r3;
    public GameObject level,section1, section2;
    private bool inSection1, inSection2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        r1 = GetComponent<SphereCollider>().radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z); ;
        r2 = section1.GetComponent<SphereCollider>().radius * Mathf.Max(section1.transform.lossyScale.x, section1.transform.lossyScale.y, section1.transform.lossyScale.z); 
        r3 = section2.GetComponent<SphereCollider>().radius * Mathf.Max(section2.transform.lossyScale.x, section2.transform.lossyScale.y, section2.transform.lossyScale.z);
        if (Vector3.Distance(transform.position, section1.transform.position) < (r1 + r2))
            inSection1 = true;
        if (Vector3.Distance(transform.position, section2.transform.position) < (r1 + r3))
            inSection2 = true;
        

    }
}
