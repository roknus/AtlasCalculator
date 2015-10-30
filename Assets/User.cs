using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class User : MonoBehaviour
{
    public static string ServerHostname = "www.prestige-guilde.xyz";

    public InputField Username;
    public InputField Password;

    public RectTransform Foreground;
    public AlertMessage AlertMessage;

    public bool bUserGraphLoaded;
    public string XMLUserGraph { get; set; }

    public bool Connected;

    private bool bTryingToConnect;

    public static User Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (Username)
        {
            Username.text = "";
            Password.text = "";
        }

        bUserGraphLoaded = false;
        bTryingToConnect = false;
        Connected = false;
    }

	IEnumerator Start()
	{		
		bTryingToConnect = true;
		WWW www = new WWW("http://" + ServerHostname + "/atlas/isConnected.php");
		
		yield return www;
		
		bTryingToConnect = false;
		
		if (www.text == "true") {
			Connected = true;
			LoadAtlas ();
		}
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
            else
            {
                Foreground.GetComponent<Image>().enabled = true;
                AlertMessage.Message.text = "Wrong username or password";
                AlertMessage.gameObject.SetActive(true);
            }
        }
    }

    public IEnumerator Reconnect()
    {
		if (Username.text == "" || Password.text == "")
        {
            UiManager.Instance.ShowAlertMessage("Error, need to reconnect");
			Debug.Log("Error, need to reconnect");
			Connected = false;
			yield break;
        }

        if (!bTryingToConnect)
        {
            bTryingToConnect = true;
            WWW www = new WWW("http://" + ServerHostname + "/atlas/isConnected.php");

            yield return www;

            bTryingToConnect = false;

            if (www.text == "true")
            {
                // OK !
                yield break;
            }
            else
            {
                // Reconnect
                if (!bTryingToConnect)
                {
                    bTryingToConnect = true;
                    WWWForm form = new WWWForm();
                    form.AddField("username", Username.text);
                    form.AddField("password", Password.text);

                    WWW www2 = new WWW("http://" + ServerHostname + "/atlas/connect.php", form);

                    yield return www2;

                    bTryingToConnect = false;

					if(www2.text == "fail")
					{
						UiManager.Instance.ShowAlertMessage("Error, need to reconnect");
						Debug.Log("Error, need to reconnect");
						Connected = false;
					}
                }
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

        UiManager.Instance.DisableForeGround();

        // check for errors
        if (www.error != null)
        {
            UiManager.Instance.ShowAlertMessage("An error occured");
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
                UiManager.Instance.ShowAlertMessage("Failed to load user graph");
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


