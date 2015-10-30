using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NodeToolTipScript : MonoBehaviour 
{
	public Sprite RedSpark;
	public Sprite GreenSpark;
	public Sprite BlueSpark;
	public Sprite PurpleSpark;
	public Sprite Diamond;
	public Sprite TransformationSpark;

    public Image NodeIcon;
	public Text Name;
    public Text Prestige;
	public Text Stat1;
	public Text Stat1Value;
	public Text Stat2;
	public Text Stat2Value;
    public Text Cost;
	public Text CostValue;
	public Image CostIcon;

    private static Vector3 mouseOffset = new Vector2(5,5);

	public static NodeToolTipScript Instance { get; private set;}

	void Awake()
	{		
		if (Instance != null) {
			Destroy (gameObject);
			return;
		}
		
		Instance = this;
	}

	void Start () 
	{
		gameObject.SetActive(false);	
	}

    public void SetAtMousePosition()
	{
		RectTransform t = transform as RectTransform;

		// Replace the node tooltip in case it get out of view
		if ((t.sizeDelta.x + Input.mousePosition.x > Screen.width) || (t.sizeDelta.y + Input.mousePosition.y > Screen.height))
		{
			Vector3 newPosition= new Vector3();
			if(t.sizeDelta.x + Input.mousePosition.x > Screen.width)
				newPosition.x = Screen.width - t.sizeDelta.x + mouseOffset.x;
			else
				newPosition.x = Input.mousePosition.x;

			if(t.sizeDelta.y + Input.mousePosition.y > Screen.height)
				newPosition.y = Screen.height - t.sizeDelta.y + mouseOffset.y;
			else
				newPosition.y = Input.mousePosition.y;

			transform.position = newPosition;
		}
		else
		{
			transform.position = Input.mousePosition + mouseOffset;
		}
    }

    public void SetValues(NodeBase _node)
	{
        if (_node is SymbolNode)
        {
            NodeIcon.gameObject.SetActive(true);
            NodeIcon.sprite = SymbolNode.SymbolIcon[((SymbolNode)_node).Talent];
        }
        else
        {
            NodeIcon.gameObject.SetActive(false);
        }

		Name.text = _node.GetName();

		if(_node is NodeWithPrestige)
		{
			Prestige.transform.parent.gameObject.SetActive(true);
			Prestige.text = ((NodeWithPrestige)_node).m_Prestige.ToString();
		}
		else
		{
			Prestige.transform.parent.gameObject.SetActive(false);
		}

		if(_node is NodeWithOneStat)
		{
			Stat1.transform.parent.gameObject.SetActive(true);
			Stat1.text = ((NodeWithOneStat)_node).m_Stat1.ToString();
			Stat1Value.text = ((NodeWithOneStat)_node).value1.ToString();
		}
		else
		{
			Stat1.transform.parent.gameObject.SetActive(false);
		}

		if(_node is NodeWithTwoStat)
		{
			Stat2.transform.parent.gameObject.SetActive(true);
			Stat2.text = ((NodeWithTwoStat)_node).m_Stat2.ToString();
			Stat2Value.text = ((NodeWithTwoStat)_node).value2.ToString();
		}
		else
		{
			Stat2.transform.parent.gameObject.SetActive(false);
		}

		if(_node is NodeWithCost)
		{
			Cost.transform.parent.gameObject.SetActive(true);
			CostValue.text = ((NodeWithCost)_node).m_Cost.ToString();
			switch(((NodeWithCost)_node).m_CostType)
			{
			case CostType.BlueSpark:
				CostIcon.sprite = BlueSpark;
				break;
			case CostType.Diamond:
				CostIcon.sprite = Diamond;
				break;
			case CostType.GreenSpark:
				CostIcon.sprite = GreenSpark;
				break;
			case CostType.PinkSparks:
				CostIcon.sprite = PurpleSpark;
				break;
			case CostType.RedSpark:				
				CostIcon.sprite = RedSpark;
				break;
			case CostType.Transformation:
				CostIcon.sprite = TransformationSpark;
				break;
			}
		}
		else
		{
			Cost.transform.parent.gameObject.SetActive(false);
		}
	}
}
