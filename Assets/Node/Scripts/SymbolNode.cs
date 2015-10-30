using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Serializer;

public enum Symbol
{
    BonusDash = 0,
    SlowingAttacks = 1,
    SummonOculat = 2,
    ImpulseIntensity = 3,
    MaximumRecoil = 4,
    UltimateStrength = 5,
    Mobilization = 6,
    WarriorCreed = 7,
    ProtectorCreed = 8,
    PainShock = 9,
    ParalyzingShock = 10,
    WaveDamage = 11,
    WaveSlow = 12,
    LuckyShot = 13,
    DoubleDischarge = 14,
    ElementSurprise = 15,
    Inertia = 16,
    Breakout = 17,
    CleverManeuver = 18,
    DesperateResistance = 19,
    Balance = 20,
    Ruthlessness = 21,
    PainfulSpasms = 22,
    LaceratedWound = 23,
    ForcedRespite = 24,
    DefenseReflex = 25,
    AdrenalineSurge = 26,
    ImpulseBarrier = 27,
    Impact = 28,
    MarkDeath = 29,
    GloriousTriumph = 30,
    LifeEnergy = 31,
    ConsumablesEfficiency = 32,
    RechargingConsumables = 33,
    TacticalDefense = 34,
    RapidAttack = 35,
    CompressiveTraume = 36,
    DeepDissection = 37
}

public class SymbolNode : NodeWithPrestige 
{
    public static bool isInit = false;
    public static Dictionary<Symbol, string>    SymbolName              = new Dictionary<Symbol, string>();
    public static Dictionary<Symbol, Sprite>    SymbolIcon              = new Dictionary<Symbol, Sprite>();
    public static Dictionary<Symbol, string>    SymbolDesctription      = new Dictionary<Symbol, string>();
    public static Dictionary<int, string>       RomanNumbers            = new Dictionary<int, string>();

    public static void Init()
    {
        if (isInit) return;

        SymbolName.Add(Symbol.BonusDash, "Bonus Dash");
        SymbolIcon.Add(Symbol.BonusDash, Resources.Load<Sprite>("Symbols/Daring_Youth_Icon"));
        SymbolDesctription.Add(Symbol.BonusDash, "Number of Dash Charges is increased by 1 for each level of this symbol.");

        SymbolName.Add(Symbol.SlowingAttacks, "Slowing Attacks");
        SymbolIcon.Add(Symbol.SlowingAttacks, Resources.Load<Sprite>("Symbols/Slowing_attacks_Icon"));
        SymbolDesctription.Add(Symbol.SlowingAttacks, "The character's attacks slow enemies down by 10% for each level of this symbol. Lasts 3 seconds.");

        SymbolName.Add(Symbol.SummonOculat, "Summon Oculat");
        SymbolIcon.Add(Symbol.SummonOculat, Resources.Load<Sprite>("Symbols/Summon_Oculat_Icon"));
        SymbolDesctription.Add(Symbol.SummonOculat, "Using attacking abilities in combat has a certain chance to summon an Oculat that will be fighting on the character's side for 18/25 seconds.");

        SymbolName.Add(Symbol.ImpulseIntensity, "Impulse Intensity");
        SymbolIcon.Add(Symbol.ImpulseIntensity, Resources.Load<Sprite>("Symbols/Node_BalanceSp"));
        SymbolDesctription.Add(Symbol.ImpulseIntensity, "Impulse damage is increased by 10% for each level of this symbol.");

        SymbolName.Add(Symbol.MaximumRecoil, "Maximum Recoil");
        SymbolIcon.Add(Symbol.MaximumRecoil, Resources.Load<Sprite>("Symbols/Node_BalanceL"));
        SymbolDesctription.Add(Symbol.MaximumRecoil, "Critical damage is increased by 10% for each level of this symbol.");

        SymbolName.Add(Symbol.UltimateStrength, "Ultimate Strength");
        SymbolIcon.Add(Symbol.UltimateStrength, Resources.Load<Sprite>("Symbols/Node_BalanceSt"));
        SymbolDesctription.Add(Symbol.UltimateStrength, "The effect of Strength on your base damage is increased by 10% for each level of this symbol.");

        SymbolName.Add(Symbol.Mobilization, "Mobilization");
        SymbolIcon.Add(Symbol.Mobilization, Resources.Load<Sprite>("Symbols/Node_BalanceV"));
        SymbolDesctription.Add(Symbol.Mobilization, "Bonus damage is increased by 10% for each level of this symbol. ");

        SymbolName.Add(Symbol.WarriorCreed, "Warrior's Creed");
        SymbolIcon.Add(Symbol.WarriorCreed, Resources.Load<Sprite>("Symbols/Warrior's_Creed_Icon"));
        SymbolDesctription.Add(Symbol.WarriorCreed, "Incoming damage and damage dealt are increased by 5%.");

        SymbolName.Add(Symbol.ProtectorCreed, "Protector's Creed");
        SymbolIcon.Add(Symbol.ProtectorCreed, Resources.Load<Sprite>("Symbols/Protector's_Creed_Icon"));
        SymbolDesctription.Add(Symbol.ProtectorCreed, "Incoming damage and damage dealt are both reduced by 5%.");

        SymbolName.Add(Symbol.PainShock, "Pain Shock");
        SymbolIcon.Add(Symbol.PainShock, Resources.Load<Sprite>("Symbols/Pain_Shock_Icon"));
        SymbolDesctription.Add(Symbol.PainShock, "Using offensive abilities allows you to use shock. Pain Shock stuns the enemy for 3 seconds. This ability takes 25 seconds to recharge. When used together with Paralyzing Shock, both effects are applied simultaneously, and cooldown is reduced by 5 seconds.");

        SymbolName.Add(Symbol.ParalyzingShock, "Paralyzing Shock");
        SymbolIcon.Add(Symbol.ParalyzingShock, Resources.Load<Sprite>("Symbols/Pain_Shock_Icon"));
        SymbolDesctription.Add(Symbol.ParalyzingShock, "Using offensive abilities allows you to use shock. Paralyzing Shock stuns the enemy for 3 seconds. This ability takes 25 seconds to recharge. When used together with Pain Shock, both effects are applied simultaneously, and cooldown is reduced by 5 seconds.");

        SymbolName.Add(Symbol.WaveDamage, "Wave: Damage");
        SymbolIcon.Add(Symbol.WaveDamage, Resources.Load<Sprite>("Symbols/Ripple_Effect_Icon"));
        SymbolDesctription.Add(Symbol.WaveDamage, "Using offensive abilities allows you to use wave. Wave deals damage to all enemies around the character in a 8 yard radius. Cooldown is 25 seconds. When the attack is used together with Wave: Slow, both effects are applied simultaneously, and cooldown is reduced by 5 seconds.");

        SymbolName.Add(Symbol.WaveSlow, "Wave: Slow");
        SymbolIcon.Add(Symbol.WaveSlow, Resources.Load<Sprite>("Symbols/Ripple_Effect_Icon"));
        SymbolDesctription.Add(Symbol.WaveSlow, "Using offensive abilities allows you to use wave. Wave slows all enemies within an 8-yard radius by 50% for 4 seconds. Cooldown is 25 seconds. When the attack is used together with Wave: Damage, both effects are applied simultaneously, and cooldown is reduced by 5 seconds. ");

        SymbolName.Add(Symbol.LuckyShot, "Lucky Shot");
        SymbolIcon.Add(Symbol.LuckyShot, Resources.Load<Sprite>("Symbols/Lucky_Shot_Icon"));
        SymbolDesctription.Add(Symbol.LuckyShot, "Every 2 seconds, the character's critical hit chance is increased by 1% for each level of this symbol. When critical damage is dealt, the effect is reset and begins accumulating again.");

        SymbolName.Add(Symbol.DoubleDischarge, "Double Discharge");
        SymbolIcon.Add(Symbol.DoubleDischarge, Resources.Load<Sprite>("Symbols/Double_Discharge_Icon"));
        SymbolDesctription.Add(Symbol.DoubleDischarge, "When Impulse Charge is restored, there is a chance that it can be activated again 2 times in a row. The chance is increased by 12% for each level of this symbol.");

        SymbolName.Add(Symbol.ElementSurprise, "Element of Surprise");
        SymbolIcon.Add(Symbol.ElementSurprise, Resources.Load<Sprite>("Symbols/Fighting_Spirit_Icon"));
        SymbolDesctription.Add(Symbol.ElementSurprise, "For the first 8 seconds of the fight, damage dealt is increased by 12% for each level of this symbol.");

        SymbolName.Add(Symbol.Inertia, "Inertia");
        SymbolIcon.Add(Symbol.Inertia, Resources.Load<Sprite>("Symbols/Slalom_Icon"));
        SymbolDesctription.Add(Symbol.Inertia, "For 3 seconds after dash is used, the incoming damage is reduced by 12% for each level of this symbol.");

        SymbolName.Add(Symbol.RapidAttack, "Rapid Attack");
        SymbolIcon.Add(Symbol.RapidAttack, Resources.Load<Sprite>("Symbols/Rapid_Attack_Icon"));
        SymbolDesctription.Add(Symbol.RapidAttack, "For the first 3 seconds of the fight, damage dealt is increased by 12% for each level of this symbol.");

        SymbolName.Add(Symbol.Breakout, "Breakout");
        SymbolIcon.Add(Symbol.Breakout, Resources.Load<Sprite>("Symbols/Steamroller_Icon"));
        SymbolDesctription.Add(Symbol.Breakout, "Using Dash stuns nearby enemies for 1.5 seconds. The effect can be activated no more than once every 21/14 seconds.");

        SymbolName.Add(Symbol.CleverManeuver, "Clever Maneuver");
        SymbolIcon.Add(Symbol.CleverManeuver, Resources.Load<Sprite>("Symbols/Clever_Maneuver_Icon"));
        SymbolDesctription.Add(Symbol.CleverManeuver, "Using Dash creates a protective barrier that absorbs damage equal to 5% of the character's maximum health for 3 seconds.");

        SymbolName.Add(Symbol.DesperateResistance, "Desperate Resistance");
        SymbolIcon.Add(Symbol.DesperateResistance, Resources.Load<Sprite>("Symbols/Desperate_Resistance_Icon"));
        SymbolDesctription.Add(Symbol.DesperateResistance, "Slowing effects used against the character are reduced by 15% for each level of this symbol.");

        SymbolName.Add(Symbol.Balance, "Balance");
        SymbolIcon.Add(Symbol.Balance, Resources.Load<Sprite>("Symbols/Balance_Icon"));
        SymbolDesctription.Add(Symbol.Balance, "The character takes 6% less damage for each level of this symbol when they are fighting against 3 or more enemies. ");

        SymbolName.Add(Symbol.Ruthlessness, "Ruthlessness");
        SymbolIcon.Add(Symbol.Ruthlessness, Resources.Load<Sprite>("Symbols/Pyrophobia_Icon"));
        SymbolDesctription.Add(Symbol.Ruthlessness, "Opponents under stun, fear, blind, or immobilize takes 6% more damage from the character for each level of this symbol. Opponents under slowing effects take 2% more damage from the character for each level of this symbol.");

        SymbolName.Add(Symbol.PainfulSpasms, "Painful Spasms");
        SymbolIcon.Add(Symbol.PainfulSpasms, Resources.Load<Sprite>("Symbols/Painful_Spasms_Icon"));
        SymbolDesctription.Add(Symbol.PainfulSpasms, "Impulse damage applies an effect to the enemy that inflicts damage over time. Total damage is equal to the character's Spirit. ");

        SymbolName.Add(Symbol.LaceratedWound, "Lacerated Wound");
        SymbolIcon.Add(Symbol.LaceratedWound, Resources.Load<Sprite>("Symbols/Lacerated_Wound_Icon"));
        SymbolDesctription.Add(Symbol.LaceratedWound, "Critical damage applies an effect to the enemy that inflicts damage over time. Total damage is equal to the character's Luck. ");

        SymbolName.Add(Symbol.ForcedRespite, "Forced Respite");
        SymbolIcon.Add(Symbol.ForcedRespite, Resources.Load<Sprite>("Symbols/Forced_Respite_Icon"));
        SymbolDesctription.Add(Symbol.ForcedRespite, "Having a slow effect applied to you has a 20% chance to restore Dash charge. ");

        SymbolName.Add(Symbol.DefenseReflex, "Defense Reflex");
        SymbolIcon.Add(Symbol.DefenseReflex, Resources.Load<Sprite>("Symbols/Defense_Reflex_Icon"));
        SymbolDesctription.Add(Symbol.DefenseReflex, "The character takes 10% less damage for each level of this symbol when under stun, fear, blind, or immobilize effects. The character takes 3% less damage for each level of this symbol when under slowing effects. ");

        SymbolName.Add(Symbol.AdrenalineSurge, "Adrenaline Surge");
        SymbolIcon.Add(Symbol.AdrenalineSurge, Resources.Load<Sprite>("Symbols/Steadying_Effect_Icon"));
        SymbolDesctription.Add(Symbol.AdrenalineSurge, "When taking damage, the character's speed can be increased by 15% for each level of this symbol. The effect last 5 seconds, and its activation chance depends on the character's current health. ");

        SymbolName.Add(Symbol.ImpulseBarrier, "Impulse Barrier");
        SymbolIcon.Add(Symbol.ImpulseBarrier, Resources.Load<Sprite>("Symbols/Electric_Field_Icon"));
        SymbolDesctription.Add(Symbol.ImpulseBarrier, "When Impulse Charge is activated, the character gains a shield that absorbs damage for 3 seconds. Each level of this symbol increases the amount of damage absorbed by 3% of the character's maximum health. ");

        SymbolName.Add(Symbol.Impact, "Impact");
        SymbolIcon.Add(Symbol.Impact, Resources.Load<Sprite>("Symbols/Knockout_Punches_Icon"));
        SymbolDesctription.Add(Symbol.Impact, "Critical hits stun enemies for 1.5 seconds. The attack can be used against the same target only once every 7 seconds. ");

        SymbolName.Add(Symbol.MarkDeath, "Mark of Death");
        SymbolIcon.Add(Symbol.MarkDeath, Resources.Load<Sprite>("Symbols/Mark_of_Death_Icon"));
        SymbolDesctription.Add(Symbol.MarkDeath, "Your attacks may leave a mark on the opponent that, once removed, will inflict 20/30% of damage inflicted to them while they had the mark. ");

        SymbolName.Add(Symbol.GloriousTriumph, "Glorious Triumph");
        SymbolIcon.Add(Symbol.GloriousTriumph, Resources.Load<Sprite>("Symbols/Glorious_Triumph_Icon"));
        SymbolDesctription.Add(Symbol.GloriousTriumph, "Using a finishing strike increases damage dealt by 10% and reduces incoming damage by 10% for 10 seconds. ");

        SymbolName.Add(Symbol.LifeEnergy, "Life Energy");
        SymbolIcon.Add(Symbol.LifeEnergy, Resources.Load<Sprite>("Symbols/Life_Energy_Icon"));
        SymbolDesctription.Add(Symbol.LifeEnergy, "Using Healing Orbs increases running speed by 30% and reduces incoming damage by 30% for 3 seconds. ");

        SymbolName.Add(Symbol.ConsumablesEfficiency, "Consumables Efficiency");
        SymbolIcon.Add(Symbol.ConsumablesEfficiency, Resources.Load<Sprite>("Symbols/Consumables_Efficiency_Icon"));
        SymbolDesctription.Add(Symbol.ConsumablesEfficiency, "Duration of the positive effects of consumables is increased by 12/24%. Amount of health restored by consumables is increased by 12/24%. Damage dealt is increased by 12/24%. ");

        SymbolName.Add(Symbol.RechargingConsumables, "Red Alert");
        SymbolIcon.Add(Symbol.RechargingConsumables, Resources.Load<Sprite>("Symbols/Recharging_Consumables_Icon"));
        SymbolDesctription.Add(Symbol.RechargingConsumables, "Combat consumables are restored 15/30% faster. ");

        SymbolName.Add(Symbol.TacticalDefense, "Tactical Defense");
        SymbolIcon.Add(Symbol.TacticalDefense, Resources.Load<Sprite>("Symbols/Tactical_Defense_Icon"));
        SymbolDesctription.Add(Symbol.TacticalDefense, "Melee and Ranged defense is increased by 5% for each level of this symbol. ");

        SymbolName.Add(Symbol.CompressiveTraume, "Compressive Traume");
        SymbolIcon.Add(Symbol.CompressiveTraume, Resources.Load<Sprite>("Symbols/Compressive_Trauma_Icon"));
        SymbolDesctription.Add(Symbol.CompressiveTraume, "When you inflict maximum bonus damage to the enemy, they have an effect applied the them that inflicts damage over time. Total damage inflicted equals 140% of the character's Strength.");

        SymbolName.Add(Symbol.DeepDissection, "Deep Dissection");
        SymbolIcon.Add(Symbol.DeepDissection, Resources.Load<Sprite>("Symbols/Deep_Dissection_Icon"));
        SymbolDesctription.Add(Symbol.DeepDissection, "When you inflict maximum bonus damage to the enemy, they have an effect applied the them that inflicts damage over time. Total damage inflicted equals 100% of the character's Valor.");

        RomanNumbers.Add(0, "0");
        RomanNumbers.Add(1, "I");
        RomanNumbers.Add(2, "II");
        RomanNumbers.Add(3, "III");
        RomanNumbers.Add(4, "IV");
        RomanNumbers.Add(5, "V");
        RomanNumbers.Add(6, "VI");
        RomanNumbers.Add(7, "VII");
        RomanNumbers.Add(8, "VIII");
        RomanNumbers.Add(9, "IX");
        RomanNumbers.Add(10, "X");

        isInit = true;
    }

	public string m_TalentName;
    public Symbol Talent;

	public override string GetName() { return "Talent : " + m_TalentName; }

	public override XMLNode GetSerialize ()	{ return new XMLTalentNode(this); }
	
	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);
		
		XMLTalentNode talentNode = node as XMLTalentNode;
		
		m_TalentName = talentNode.m_TalentName;

        Talent = talentNode.m_Talent;

        //SymbolShortcut.Instance.AddSymbol(this);
	}
}
