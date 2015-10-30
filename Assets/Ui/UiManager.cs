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
    public AlertMessage     AlertMessage;
    public RectTransform    LoadingPanel;

    public RectTransform    GreatnessNodeList;

    public Button           ButtonGreatness;

    public Button           ButtonProficency;
    public Text             ButtonProficencyText { get; set; }

    public RectTransform    RightClickPanel;
    public Button           FindShortestPathButton;
    public Button           FindCheapestPathButton;

    public Toggle           IgnorePinkNodes;

    public PathCostPanel    CostInfoPanel_Simulated;
    public PathStatsPanel   StatsInfoPanel_Simulated;
	public Button           ResetPathInfo_Simulated;
	public Button			StrikePath_Simulated;
    public PathCostPanel    CostInfoPanel_Calculated;
    public PathStatsPanel   StatsInfoPanel_Calculated;
	public Button           ResetPathInfo_Calculated;
	public Button			StrikePath_Calculated;
    public Button           MoreInfosButton;

    public RectTransform    SymbolsPanel;
    public Button           ShowSymbolsButton;

    public RectTransform    SimulationPanel;
    public Button           ShowSimulationButton;
    public Button           SimulationButton;
    public Button           SaveSimulationButton;
    public Button           LoadSimulationButton;

    public Button           SaveButton;

    public Button           TopViewButton;
    public Button           IsoViewButton;

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
        GreatnessNodeList.gameObject.SetActive(false);

        MoreInfosButton.onClick.AddListener(() => SwitchMoreInfos());
        ResetPathInfo_Simulated.onClick.AddListener(() => SimulationScript.Instance.ResetPath());
        ResetPathInfo_Calculated.onClick.AddListener(() => WorldScript.Instance.ResetPath());

		StrikePath_Calculated.onClick.AddListener ( () =>  WorldScript.Instance.StrikeCalculatedpath());
		StrikePath_Simulated.onClick.AddListener ( () => SimulationScript.Instance.StrikeSimulatedPath());

        ShowSymbolsButton.onClick.AddListener(() => SwitchShowSymbols());

        ShowSimulationButton.onClick.AddListener(() => SwitchSimulation());

        SimulationButton.onClick.AddListener(() => SimulationScript.Instance.SwitchSimulation());

        IgnorePinkNodes.onValueChanged.AddListener((b) => WorldScript.Instance.SwitchIgnorePinkNodes(b));

        IsoViewButton.onClick.AddListener(() => CameraController.Instance.SwitchCamera());
        TopViewButton.onClick.AddListener(() => CameraController.Instance.SwitchCamera());
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

    public void ShowAlertMessage(string message)
    {
        EnableForeGround();
        AlertMessage.gameObject.SetActive(true);
        AlertMessage.Message.text = message;
    }

    public void ShowLoading()
    {
        EnableForeGround();
        LoadingPanel.gameObject.SetActive(true);
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
        StatsInfoPanel_Simulated.gameObject.SetActive(!StatsInfoPanel_Simulated.gameObject.activeSelf);
        StatsInfoPanel_Calculated.gameObject.SetActive(!StatsInfoPanel_Calculated.gameObject.activeSelf);
    }

    public void SwitchShowSymbols()
    {
        SymbolsPanel.gameObject.SetActive(!SymbolsPanel.gameObject.activeSelf);
    }

    public void SwitchSimulation()
    {
        SimulationPanel.gameObject.SetActive(!SimulationPanel.gameObject.activeSelf);
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
