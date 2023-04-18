using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendToServer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendToServerF()
    {
        GameObject px = GameObject.Find("PlayerX");
        PlayerData pd = px.GetComponent<PlayerData>();
        string message = JsonUtility.ToJson(pd.player);
        Debug.Log(message);
        StartCoroutine(SendLogoutData(message));

    }
    public void LevelComplete()
    {
        GameObject px = GameObject.Find("PlayerX");
        PlayerData pd = px.GetComponent<PlayerData>();
        string message = JsonUtility.ToJson(pd.player);
        Debug.Log(message);
        StartCoroutine(SendLevelComplete(message));

    }

    IEnumerator SendLogoutData(string data)
    {
        WWWForm form = new WWWForm();
        form.AddField("player", data);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8000/api/dologout/", form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string txt = www.downloadHandler.text;
                Debug.Log(txt);
                Application.Quit();
            }
        }
    }

    IEnumerator SendLevelComplete(string data)
    {
        WWWForm form = new WWWForm();
        form.AddField("player", data);

        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8000/api/lvlcomplete/", form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string txt = www.downloadHandler.text;
                Debug.Log(txt);
                Application.Quit();
            }
        }
    }
}
