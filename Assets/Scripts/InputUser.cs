using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUser : MonoBehaviour
{
    public string numberList, group;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void inputNL(string s)
    {
        numberList = s;
        Debug.Log(numberList);
    }

    public void inputGroup(string s)
    {
        group = s;
        Debug.Log(group);
    }
    

    public void submitUser()
    {
        //Do all the django stuff here
        Debug.Log("Enter Pressed");
    }
}
