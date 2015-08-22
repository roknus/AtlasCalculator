using UnityEngine;
using System.Collections;
using Serializer;
public enum Classes
{
    Alchemist = 0,
    Lightbinder = 1,
    Knight = 2,
    Paladin = 3,
    Archer = 4,
    Berserker = 5,
    Cryomancer = 6,
    Gunner = 7,
    Kinetic = 8,
    Necromancer = 9,
    Slayer = 10,
    Witch = 11,
    Monk = 12
}

public class ClassNode : NodeWithCost 
{
    public Sprite Alchemist;
    public Sprite Lightbinder;
    public Sprite Knight;
    public Sprite Paladin;
    public Sprite Archer;
    public Sprite Berserker;
    public Sprite Cryomancer;
    public Sprite Gunner;
    public Sprite Kinetic;
    public Sprite Necromancer;
    public Sprite Slayer;
    public Sprite Witch;
    public Sprite Monk;

    public SpriteRenderer ClassIcon;

    public string m_ClassName;
    public Classes m_Class;

    void Start()
    {
        switch (m_Class)
        {
            case Classes.Alchemist:
                ClassIcon.sprite = Alchemist;
                break;
            case Classes.Lightbinder:
                ClassIcon.sprite = Lightbinder;
                break;
            case Classes.Knight:
                ClassIcon.sprite = Knight;
                break;
            case Classes.Paladin:
                ClassIcon.sprite = Paladin;
                break;
            case Classes.Archer:
                ClassIcon.sprite = Archer;
                break;
            case Classes.Berserker:
                ClassIcon.sprite = Berserker;
                break;
            case Classes.Cryomancer:
                ClassIcon.sprite = Cryomancer;
                break;
            case Classes.Gunner:
                ClassIcon.sprite = Gunner;
                break;
            case Classes.Kinetic:
                ClassIcon.sprite = Kinetic;
                break;
            case Classes.Necromancer:
                ClassIcon.sprite = Necromancer;
                break;
            case Classes.Slayer:
                ClassIcon.sprite = Slayer;
                break;
            case Classes.Witch:
                ClassIcon.sprite = Witch;
                break;
            case Classes.Monk:
                ClassIcon.sprite = Monk;
                break;
        }
    }

	public string ClassName
	{
		get
		{
			if(m_ClassName == "")
				return "N/A";
			else return m_ClassName;
		}
		set { m_ClassName = value; }
	}

	public override string GetName() { return "Class : " + m_ClassName; }

	public override XMLNode GetSerialize ()	{ return new XMLClassNode(this); }

	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);

		XMLClassNode classNode = node as XMLClassNode;

		ClassName = classNode.m_ClassName;
        m_Class = (Classes)classNode.m_Class;
	}
}
