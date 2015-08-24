using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class User : MonoBehaviour
{
    public static string ServerHostname = "www.prestige-guilde.xyz";

    public static User Instance { get; private set; }

    public InputField Username;
    public InputField Password;

    public bool bUserGraphLoaded;
    public string XMLUserGraph { get; set; }

    public bool Connected;

    private bool bTryingToConnect;

    void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        bUserGraphLoaded = false;
        bTryingToConnect = false;
        Connected = false;
    }

    IEnumerator TryConnect()
    {
        if (!bTryingToConnect)
        {
            bTryingToConnect = true;
            WWWForm form = new WWWForm();
            form.AddField("username", Username.text);
            form.AddField("password", Password.text);

            WWW www = new WWW("http://" + ServerHostname + "/atlas/connect.php", form);

            yield return www;

            bTryingToConnect = false;

            if (www.text == "success")
            {
                Connected = true;
                LoadAtlas();
            }
        }
    }

    public void StartLoadUserXML()
    { 
        StartCoroutine("LoadUserXML", "http://" + ServerHostname + "/atlas/AtlasCalculator/get_user_graph.php");
    }

    public void StartLoadDefaultUserXML()
    {
        StartCoroutine("LoadUserXML", "http://" + ServerHostname + "/atlas/AtlasCalculator/default.xml");
    }

    IEnumerator LoadUserXML(string url)
    {
        WWW www = new WWW(url);

        yield return www;

        // check for errors
        if (www.error != null)
        {
            Debug.Log("WWW error : " + www.error);
        }
        else
        {
            XMLUserGraph = www.text;
            if (!(XMLUserGraph == "fail"))
            {
                bUserGraphLoaded = true;
                WorldScript.Instance.LoadUserGraph();	
                Debug.Log("Loaded user graph");
            }
            else
            {
                Debug.Log("Failed to load user graph");
            }
        }
    }

    public void Connect()
    {
        StartCoroutine("TryConnect");
    }

    public void InviteMode()
    {
        LoadAtlas();
    }

    public void LoadAtlas()
    {
        if(Application.isWebPlayer)
        {
            // Avoid piracy
            if (Application.absoluteURL.IndexOf(ServerHostname) == -1)
            {
                Debug.Log("Pirated");
                return;
            }
        }

        Application.LoadLevel("AtlasCalculator");
    }

    public void CreateNewAccount()
    {
        Application.ExternalEval("window.open('http://" + ServerHostname + "/atlas/register.php','_blank')");
    }
}


