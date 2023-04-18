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

    public const string baseURL = "http://20.83.162.38/";
    private const string LOGOUT_URL = baseURL + "api/dologout/"; // Replace with your actual login URL
    private const string LVLCOMP_URL = baseURL + "api/lvlcomplete/"; // Replace with your actual login URL

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

        using (UnityWebRequest www = UnityWebRequest.Post(LOGOUT_URL, form))
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

        using (UnityWebRequest www = UnityWebRequest.Post(LVLCOMP_URL, form))
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
