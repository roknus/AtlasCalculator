using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SymbolShortcut : MonoBehaviour 
{
    public Transform Button;
    public Dictionary<Symbol, List<SymbolNode>> TalentList;

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

        TalentList = new Dictionary<Symbol, List<SymbolNode>>();

        foreach (KeyValuePair<Symbol, Sprite> s in SymbolNode.SymbolIcon)
        {
            Symbol symb = s.Key;
            Transform t = Instantiate(Button);
            t.GetComponentsInChildren<Image>()[1].sprite = s.Value;
            t.GetComponentInChildren<Tooltip>().m_Tooltip = SymbolNode.SymbolName[symb];
            t.GetComponentInChildren<Text>().text = "";
            // Need to make a copy dunno why...
            t.GetComponent<Button>().onClick.AddListener(() => ShowSymbols(symb));
            t.SetParent(transform);
            TalentList.Add(symb, new List<SymbolNode>());
        }

        foreach (Transform t in WorldScript.Instance.m_nodes.Values)
        {
            NodeBase n = t.GetComponent<NodeBase>();
            if (n is SymbolNode)
            {
                AddSymbol((SymbolNode)n);
            }
        }
    }

    public void AddSymbol(SymbolNode _talent)
    {
        TalentList[_talent.Talent].Add(_talent);
    }

    public void ShowSymbols(Symbol s)
    {
        SymbolSubListScript.Instance.gameObject.SetActive(true);
        SymbolSubListScript.Instance.SetList(TalentList[s]);
    }
}
