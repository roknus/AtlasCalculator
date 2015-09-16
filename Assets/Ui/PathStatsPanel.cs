using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PathStatsPanel : MonoBehaviour
{

    public Text Might;
    public Text Stamina;
    public Text Strength;
    public Text Valor;	
	public Text Luck;
    public Text Spirit;
    public Text Prestige;

	public static PathStatsPanel Instance { get; private set; }

	void Awake () 
    {		
		if (Instance != null) 
		{
			Destroy (gameObject);
			return;
		}
		else
		{
			Instance = this;
		}
	}

    void Start()
    {
        UiManager.Instance.SwitchMoreInfos();
    }

    public void SetPanel(NodePath path)
    {
        Might.text = path.Might.ToString();
        Stamina.text = path.Stamina.ToString();
        Strength.text = path.Strength.ToString();
        Valor.text = path.Valor.ToString();
        Luck.text = path.Luck.ToString();
        Spirit.text = path.Spirit.ToString();
        Prestige.text = path.Prestige.ToString();
    }

    public void Clean()
    {
        Might.text      = "0";
        Stamina.text    = "0";
        Strength.text   = "0";
        Valor.text      = "0";
        Luck.text       = "0";
        Spirit.text     = "0";
        Prestige.text   = "0";
    }
}
