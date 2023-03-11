using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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

    IEnumerator upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("listNumber", "2");
        form.AddField("group", "1");

        using (UnityWebRequest www = UnityWebRequest.Post("http;//40.76.234.232:8000/validacion-usuario/", form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string txt = www.downloadHandler.text;
                
            }
        }
    }

}
