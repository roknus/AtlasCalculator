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
	}

    public void ResetList()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetList(List<CTalentNode> _list)
    {
        ResetList();
        foreach (CTalentNode t in _list)
        {
            Transform o = Instantiate(shortcut);
            o.SetParent(transform);
            o.GetComponentInChildren<Tooltip>().m_Tooltip = CTalentNode.SymbolName[t.Talent];
            o.GetComponentsInChildren<Image>()[1].sprite = CTalentNode.SymbolIcon[t.Talent];
            Transform tCopy = t.transform;
            o.GetComponent<Button>().onClick.AddListener(() => CameraController.Instance.FocusNode(tCopy));            
        }
    }
}
