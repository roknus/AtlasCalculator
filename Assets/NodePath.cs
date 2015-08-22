using UnityEngine;
using System.Collections.Generic;

public class NodePath
{
    public HashSet<NodeBase> Path { get; set; }
    public int Count { get { return Path.Count; } }

    public Cost Cost;
    public int Red { get { return Cost.R; } }
    public int Green { get { return Cost.G; } }
    public int Blue { get { return Cost.B; } }
    public int TotSparks { get { return Cost.Tot; } }

	public int PurpleSparks { get { return Cost.Pink; } }

    public int Proficency;
    public int Greatness;

    public int Might;
    public int Stamina;
    public int Strength;
    public int Valor;
    public int Luck;
    public int Spirit;

    public NodePath()
    {
        Path = new HashSet<NodeBase>();
        Cost = new Cost(0, 0, 0);
        Proficency = 0;
        Greatness = 0;
        Might = 0; 
        Stamina = 0; 
        Strength = 0;
        Valor = 0;
        Luck = 0;
        Spirit = 0;
    }
    public NodePath(NodePath _other)
    {
        Path = new HashSet<NodeBase>(_other.Path);
        Cost = new Cost(_other.Cost);
        Proficency = _other.Proficency;
        Greatness = _other.Greatness;
        Might = _other.Might;
        Stamina = _other.Stamina;
        Strength = _other.Strength;
        Valor = _other.Valor;
        Luck = _other.Luck;
        Spirit = _other.Spirit;
    }

    public void Add(NodeBase _n) 
    {
		if (_n != null)
        {
            Path.Add(_n);
            if (!_n.bUnlocked)
            {
                Cost += _n.GetCost();
                Proficency += _n.GetProficency();
                Greatness  += _n.GetGreatness();
                Might += _n.GetMight();
                Stamina += _n.GetStamina();
                Strength += _n.GetStrength();
                Valor += _n.GetValor();
                Luck += _n.GetLuck();
                Spirit += _n.GetSpirit();
            }
		}
    }

    public void Remove(NodeBase _n)
    {
		if (_n != null )
        {
            Path.Remove(_n);

            Cost -= _n.GetCost();
            Proficency -= _n.GetProficency();
            Greatness -= _n.GetGreatness();
            Might -= _n.GetMight();
            Stamina -= _n.GetStamina();
            Strength -= _n.GetStrength();
            Valor -= _n.GetValor();
            Luck -= _n.GetLuck();
            Spirit -= _n.GetSpirit();
        }
    }

    public bool Contains(NodeBase _n) { return Path.Contains(_n); }
}
