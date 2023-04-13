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
        StartCoroutine(SendPlayerData(message));
    }

    IEnumerator SendPlayerData(string data)
    {
        WWWForm form = new WWWForm();
        form.AddField("player", data);
        using (UnityWebRequest www = UnityWebRequest.Post("http://40.76.234.232:8000/logout", form))
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
                Application.Quit();
            }
        }
    }
}
