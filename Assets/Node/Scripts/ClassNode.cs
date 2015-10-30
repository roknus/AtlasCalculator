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

    public string m_ClassName;
    public Classes m_Class;

    private static string IconPath = "NodesIcon/Class";

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
        XMLClassNode classNode = node as XMLClassNode;
        ClassName = classNode.m_ClassName;
        m_Class = (Classes)classNode.m_Class;

        switch (m_Class)
        {
            case Classes.Alchemist:
                Locked = Resources.Load<Sprite>(IconPath + "/Alchemist" + "/Alchemist_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Alchemist" + "/Alchemist_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Alchemist" + "/Alchemist_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Alchemist" + "/Alchemist_Calculated");
                break;
            case Classes.Lightbinder:
                Locked = Resources.Load<Sprite>(IconPath + "/Lightbinder" + "/Lightbinder_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Lightbinder" + "/Lightbinder_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Lightbinder" + "/Lightbinder_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Lightbinder" + "/Lightbinder_Calculated");
                break;
            case Classes.Knight:
                Locked = Resources.Load<Sprite>(IconPath + "/Knight" + "/Knight_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Knight" + "/Knight_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Knight" + "/Knight_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Knight" + "/Knight_Calculated");
                break;
            case Classes.Paladin:
                Locked = Resources.Load<Sprite>(IconPath + "/Paladin" + "/Paladin_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Paladin" + "/Paladin_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Paladin" + "/Paladin_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Paladin" + "/Paladin_Calculated");
                break;
            case Classes.Archer:
                Locked = Resources.Load<Sprite>(IconPath + "/Archer" + "/Archer_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Archer" + "/Archer_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Archer" + "/Archer_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Archer" + "/Archer_Calculated");
                break;
            case Classes.Berserker:
                Locked = Resources.Load<Sprite>(IconPath + "/Berserker" + "/Berserker_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Berserker" + "/Berserker_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Berserker" + "/Berserker_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Berserker" + "/Berserker_Calculated");
                break;
            case Classes.Cryomancer:
                Locked = Resources.Load<Sprite>(IconPath + "/Cryomancer" + "/Cryomancer_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Cryomancer" + "/Cryomancer_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Cryomancer" + "/Cryomancer_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Cryomancer" + "/Cryomancer_Calculated");
                break;
            case Classes.Gunner:
                Locked = Resources.Load<Sprite>(IconPath + "/Gunner" + "/Gunner_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Gunner" + "/Gunner_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Gunner" + "/Gunner_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Gunner" + "/Gunner_Calculated");
                break;
            case Classes.Kinetic:
                Locked = Resources.Load<Sprite>(IconPath + "/Kinetic" + "/Kinetic_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Kinetic" + "/Kinetic_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Kinetic" + "/Kinetic_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Kinetic" + "/Kinetic_Calculated");
                break;
            case Classes.Necromancer:
                Locked = Resources.Load<Sprite>(IconPath + "/Necromancer" + "/Necromancer_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Necromancer" + "/Necromancer_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Necromancer" + "/Necromancer_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Necromancer" + "/Necromancer_Calculated");
                break;
            case Classes.Slayer:
                Locked = Resources.Load<Sprite>(IconPath + "/Slayer" + "/Slayer_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Slayer" + "/Slayer_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Slayer" + "/Slayer_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Slayer" + "/Slayer_Calculated");
                break;
            case Classes.Witch:
                Locked = Resources.Load<Sprite>(IconPath + "/Witch" + "/Witch_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Witch" + "/Witch_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Witch" + "/Witch_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Witch" + "/Witch_Calculated");
                break;
            case Classes.Monk:
                Locked = Resources.Load<Sprite>(IconPath + "/Monk" + "/Monk_Locked");
                Unlocked = Resources.Load<Sprite>(IconPath + "/Monk" + "/Monk_Unlocked");
                Simulated = Resources.Load<Sprite>(IconPath + "/Monk" + "/Monk_Simulated");
                Calculated = Resources.Load<Sprite>(IconPath + "/Monk" + "/Monk_Calculated");
                break;
        }
        m_SpriteRenderer.sprite = Locked;
        base.Deserialize(node);
	}
}
