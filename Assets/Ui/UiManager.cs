using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour 
{
    public Image            ForeGroundPanel;
    public RectTransform    ProficencyWindow;
    public RectTransform    GreatnessNodeList;
    public AlertMessage     AlertMessage;
    public Button           ButtonGreatness;
    public Button           ButtonProficency;
    public Text             ButtonProficencyText { get; set; }

    public RectTransform    MoreInfosPanel;

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
        if (Application.isPlaying)
        {
            ButtonProficency.onClick.AddListener(() => { OpenProficencyWindow(); });
            ButtonProficencyText = ButtonProficency.GetComponentInChildren<Text>();

            ButtonGreatness.onClick.AddListener(() => { EnableGreatnessNodeList(); });
        }
        GreatnessNodeList.gameObject.SetActive(false);
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

    public void EnableGreatnessNodeList()
    {
        GreatnessNodeList.gameObject.SetActive(true);
        NodeCostList.Instance.FindCheapestGreatnessNodes();
        ButtonGreatness.GetComponentInChildren<Text>().text = "Close";
        ButtonGreatness.onClick.RemoveAllListeners();
        ButtonGreatness.onClick.AddListener(() => { DisableGreatnessNodeList(); });
    }

    public void DisableGreatnessNodeList()
    {
        ButtonGreatness.GetComponentInChildren<Text>().text = "Cheapest Greatness Nodes";
        ButtonGreatness.onClick.RemoveAllListeners();
        ButtonGreatness.onClick.AddListener(() => { EnableGreatnessNodeList(); });
        GreatnessNodeList.gameObject.SetActive(false);
    }

    public void SwitchMoreInfos()
    {
        MoreInfosPanel.gameObject.SetActive(!MoreInfosPanel.gameObject.activeSelf);
        if(WorldScript.Instance.HighlightPath != null)
        {
            PathStatsPanel.Instance.SetPanel(WorldScript.Instance.HighlightPath);
        }
    }

    public void ShowAlertMessage(string message)
    {
        EnableForeGround();
        AlertMessage.gameObject.SetActive(true);
        AlertMessage.Message.text = message;
    }
}
