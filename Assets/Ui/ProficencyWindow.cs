using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProficencyWindow : MonoBehaviour 
{	
	public InputField       ProficnecyInputField;

	void Start () {
	
	}

	void Update () {
	
	}
	
	public void CalculateProficency()
	{
		string p = ProficnecyInputField.text;
		WorldScript.Instance.Optimize(int.Parse(p));
		UiManager.Instance.DisableForeGround();
	}
}
