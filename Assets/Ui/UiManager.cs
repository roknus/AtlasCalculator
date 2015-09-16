using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UiManager : MonoBehaviour 
{
    public RectTransform    MainMenu;
    public RectTransform    Calculateur;
    public Image            ForeGroundPanel;

    public RectTransform    ProficencyWindow;
    public RectTransform    GreatnessNodeList;
    public AlertMessage     AlertMessage;
    public Button           ButtonGreatness;
    public Button           ButtonProficency;
    public Text             ButtonProficencyText { get; set; }
    public Button           SimulationButton;

    public RectTransform    RightClickPanel;
    public Button           FindShortestPathButton;
    public Button           FindCheapestPathButton;

    public Toggle           IgnorePinkNodes;

    public RectTransform    StatsInfoPanel;
    public Button           MoreInfosButton;

    public RectTransform    SymbolsPanel;
    public Button           ShowSymbolsButton;

    public RectTransform    SimulationPanel;
    public Button           ShowSimulationButton;

    public Button           SaveSimulationButton;
    public Button           LoadSimulationButton;

    public Button           SaveButton;

    public RectTransform    FPSCounter;

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
            DontDestroyOnLoad(gameObject);
        }
	}

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.F))
        {
            SwitchFPSCounter();
        }
    }

    public void InitCalculatorUI()
    {
        SaveButton.onClick.AddListener(() => Save());
        if (!WorldScript.Instance.EDITOR_MODE && (User.Instance == null || !User.Instance.Connected))
        {
            SaveButton.gameObject.SetActive(false);
            UiManager.Instance.SaveSimulationButton.gameObject.SetActive(false);
            UiManager.Instance.LoadSimulationButton.gameObject.SetActive(false);
        }                                           

        ButtonProficency.onClick.AddListener(() => { OpenProficencyWindow(); });
        ButtonProficencyText = ButtonProficency.GetComponentInChildren<Text>();

        ButtonGreatness.onClick.AddListener(() => { EnableGreatnessNodeList(); });

        MoreInfosButton.onClick.AddListener(() => SwitchMoreInfos());

        ShowSymbolsButton.onClick.AddListener(() => SwitchShowSymbols());

        ShowSimulationButton.onClick.AddListener(() => SwitchSimulation());

        SimulationButton.onClick.AddListener(() => { SimulationScript.Instance.SwitchSimulation(); });

        IgnorePinkNodes.onValueChanged.AddListener((b) => WorldScript.Instance.SwitchIgnorePinkNodes(b));
        
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
        StatsInfoPanel.gameObject.SetActive(!StatsInfoPanel.gameObject.activeSelf);
        /*
        if(WorldScript.Instance.HighlightPath != null)
        {
            PathStatsPanel.Instance.SetPanel(WorldScript.Instance.HighlightPath);
        }
         * */
    }

    public void SwitchShowSymbols()
    {
        SymbolsPanel.gameObject.SetActive(!SymbolsPanel.gameObject.activeSelf);
    }

    public void SwitchSimulation()
    {
        SimulationPanel.gameObject.SetActive(!SimulationPanel.gameObject.activeSelf);
    }

    public void ShowAlertMessage(string message)
    {
        EnableForeGround();
        AlertMessage.gameObject.SetActive(true);
        AlertMessage.Message.text = message;
    }

    public void ShowRightClickPanel(Vector3 _pos, NodeBase _n)
    {
        RightClickPanel.position = _pos;
        RightClickPanel.gameObject.SetActive(true);
        FindShortestPathButton.onClick.RemoveAllListeners();
        FindShortestPathButton.onClick.AddListener(delegate()
        {
            _n.FindShortestPath();
            RightClickPanel.gameObject.SetActive(false);
        });
        FindCheapestPathButton.onClick.RemoveAllListeners();
        FindCheapestPathButton.onClick.AddListener(delegate()
        {
            _n.FindAndHighlightCheapestPath();
            RightClickPanel.gameObject.SetActive(false);
        });
    }

    public void ShowCalculatorUI()
    {
        MainMenu.gameObject.SetActive(false);
        Calculateur.gameObject.SetActive(true);
    }

    public void Save()
    {
        SaveButton.GetComponentInChildren<Text>().text = "Saving...";
        SaveButton.enabled = false;
        WorldScript.Instance.SaveAtlas();
    }

    public void ResetSaveButton()
    {
        SaveButton.GetComponentInChildren<Text>().text = "Save Atlas";
        SaveButton.enabled = true;
    }

    public void SwitchFPSCounter()
    {
        FPSCounter.gameObject.SetActive(!FPSCounter.gameObject.activeSelf);
    }
}
