using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InputUser : MonoBehaviour
{
    //public string numberList, group;

    public TMP_InputField grupo, numLista;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void submitUser()
    {
        string g = grupo.text;
        string n = numLista.text;
        StartCoroutine(SendLoginData(g, n));
        Debug.Log("Enter Pressed");
    }

    IEnumerator SendLoginData(string gr, string nl)
    {
        WWWForm form = new WWWForm();
        form.AddField("numList", nl);
        form.AddField("grupo", gr);

        using (UnityWebRequest www = UnityWebRequest.Post("http://192.168.8.238:8000/api/dologin", form))
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
                txt = txt.Substring(1);
                txt = txt.Substring(0, txt.Length - 2);
                Player ms = JsonUtility.FromJson<Player>(txt);
                Debug.Log("group: " + ms.grupo);
                Debug.Log("list number: " + ms.numLista);
                int i = -1;
                /*foreach(History h in ms.history)
                {
                    i++;
                    Debug.Log("index: "+ i);
                    Debug.Log("level: " + h.level);
                    Debug.Log("score: " + h.score);
                    if (h.score == "-1")
                        Debug.Log("El estudiante esta en el nivel " + ms.history[i-1].level);
                }*/
                GameObject po = GameObject.Find("PlayerX");
                PlayerData pd = po.GetComponent<PlayerData>();
                pd.player.grupo = ms.grupo;
                pd.player.numLista = ms.numLista;
                SceneManager.LoadScene("Assets/Scenes/LevelSelector.unity");
            }
        }
    }
}
