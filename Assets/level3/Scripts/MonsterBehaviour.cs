using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    public float hoverAmp;
    public float hoverFreq;
    Vector3 initPos;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
        transform.position = new Vector3(initPos.x, Mathf.Sin(hoverFreq * Time.time) * hoverAmp + initPos.y, initPos.z);
    }


}
