using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SymbolSubListScript : MonoBehaviour 
{
    public Transform shortcut;

    public static SymbolSubListScript Instance { get; private set; }

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
        }
        gameObject.SetActive(false);
	}

    public void ResetList()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetList(List<SymbolNode> _list)
    {
        ResetList();
		for(int i = 0; i < _list.Count; i++)
        {
			SymbolNode t = _list[i];
            Transform o = Instantiate(shortcut);
            o.SetParent(transform);
            o.GetComponentInChildren<Tooltip>().m_Tooltip = SymbolNode.SymbolName[t.Talent];
			o.GetComponentsInChildren<Image>()[1].sprite = SymbolNode.SymbolIcon[t.Talent];
            o.GetComponentInChildren<Text>().text = SymbolNode.RomanNumbers[i];
            Transform tCopy = t.transform;
            o.GetComponent<Button>().onClick.AddListener(() => CameraController.Instance.FocusNode(tCopy));            
        }
    }
}
