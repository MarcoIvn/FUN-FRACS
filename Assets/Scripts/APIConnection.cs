using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class APIConnection : MonoBehaviour
{
    public TMP_InputField group, numberList;

    void Start(){}

    // Update is called once per frame
    void Update(){}

    private int int_ify(string str){
        int strToInt = -1;
        if (int.TryParse(str, out strToInt)) {
            // Conversion was successful
            Debug.Log("Conversion successful! idToInt = " + strToInt);
        }
        else {
            // Conversion failed
            Debug.LogError("Conversion failed! Could not convert id to int.");
        }
        return strToInt;
    }

    public void submitUser()
    {
        string g = group.text;
        string n = numberList.text;
        int gr = int_ify(g);
        int nl = int_ify(n);
        StartCoroutine(SendLoginData(gr, nl));
    }
    private const string LOGIN_URL = "http://127.0.0.1:8000/api/dologin/"; // Replace with your actual login URL

    [System.Serializable]
    public class JugadorData{
        public int grupo;
        public int numLista;

    }   
    // public IEnumerator SendLoginData(string grupo, string numLista)
    public IEnumerator SendLoginData(int grupo, int numLista)
    {
        // Hacer clase y asignar variables
        JugadorData jugadorData = new JugadorData();
        jugadorData.grupo = grupo;
        jugadorData.numLista = numLista;
        // Hacerlo Json
        string player = JsonUtility.ToJson(jugadorData);
        Debug.Log("player" + player);


        WWWForm form = new WWWForm();
        form.AddField("player", player);
        // Create a UnityWebRequest using the PUT method
        using (UnityWebRequest www = UnityWebRequest.Post(LOGIN_URL, form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string txt = www.downloadHandler.text;
                Debug.Log(txt);
            }
        }
    }
}
