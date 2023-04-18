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

    public void submitUser()
    {
        string g = group.text;
        string n = numberList.text;
        Debug.Log("Enter Pressed");
        StartCoroutine(SendLoginData(g, n));
    
    }
    private const string LOGIN_URL = "http://127.0.0.1:8000/api/dologin/"; // Replace with your actual login URL

    [System.Serializable]
    public class JugadorData{
        public string grupo;
        public string numLista;

    }   
    [System.Serializable]
    public class PlayerForm{
        public string json;
    }
    // public IEnumerator SendLoginData(string grupo, string numLista)
    public IEnumerator SendLoginData(string grupo, string numLista)
    {
        JugadorData jugadorData = new JugadorData();
        jugadorData.grupo = grupo;
        jugadorData.numLista = numLista;

        string player = JsonUtility.ToJson(jugadorData);

        PlayerForm formita_json = new PlayerForm();
        formita_json.json = player;

        WWWForm form = new WWWForm();
        form.AddField("player", formita_json.ToString());

        using (UnityWebRequest request = UnityWebRequest.Post(LOGIN_URL, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Login request failed: " + request.error);
                yield break;
            }

            Debug.Log("Login request succeeded: " + request.downloadHandler.text);

            // Process login response here...
        }
    }
}
