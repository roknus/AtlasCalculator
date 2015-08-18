using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour 
{
    public Image            ForeGroundPanel;
    public RectTransform    ProficencyWindow;

    public RectTransform    SaveButton;

    public static UiManager Instance { get; private set; }

	void Start () 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        if(!WorldScript.Instance.EDITOR_MODE && (User.Instance == null || !User.Instance.Connected))
        {
            SaveButton.gameObject.SetActive(false);
        }
	}
	
	void Update () 
    {
	
	}

    public void EnableForeGround()  { ForeGroundPanel.enabled = true; }
    public void DisableForeGround() 
    {
        ForeGroundPanel.enabled = false;
        foreach (Transform child in ForeGroundPanel.transform)
            child.gameObject.SetActive(false);
    }

    public void OpenProficencyWindow()
    {
        EnableForeGround();
        ProficencyWindow.gameObject.SetActive(true);
    }
}
