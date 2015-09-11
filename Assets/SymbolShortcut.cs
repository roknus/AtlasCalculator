using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SymbolShortcut : MonoBehaviour 
{
    public Transform Button;
    public Dictionary<Symbol, List<CTalentNode>> TalentList;

    public static SymbolShortcut Instance { get; private set; }

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

        TalentList = new Dictionary<Symbol, List<CTalentNode>>();

        foreach (KeyValuePair<Symbol, Sprite> s in CTalentNode.SymbolIcon)
        {
            Transform t = Instantiate(Button);
            t.GetComponentsInChildren<Image>()[1].sprite = s.Value;
            t.GetComponentInChildren<Tooltip>().m_Tooltip = CTalentNode.SymbolName[s.Key];
            // Need to make a copy dunno why...
            Symbol symb = s.Key;
            t.GetComponent<Button>().onClick.AddListener(() => ShowSymbols(symb));
            t.SetParent(transform);
            TalentList.Add(s.Key, new List<CTalentNode>());
        }

        foreach (Transform t in WorldScript.Instance.m_nodes.Values)
        {
            NodeBase n = t.GetComponent<NodeBase>();
            if (n is CTalentNode)
            {
                AddSymbol((CTalentNode)n);
            }
        }
    }

    public void AddSymbol(CTalentNode _talent)
    {
        TalentList[_talent.Talent].Add(_talent);
    }

    public void ShowSymbols(Symbol s)
    {
        SymbolSubListScript.Instance.SetList(TalentList[s]);
    }
}
