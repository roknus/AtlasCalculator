using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MaximizeProficencyWindow : MonoBehaviour
{
    public InputField RedInputField;
    public InputField GreenInputField;
    public InputField BlueInputField;
	public InputField SpareInputField;

    public static MaximizeProficencyWindow Instance { get; private set; }
	
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	public void MaximizeProficency()
	{
        string r        = RedInputField.text;
        string g        = GreenInputField.text;
        string b        = BlueInputField.text;
        string spare    = SpareInputField.text;

        if (r == "") r = "0";
        if (g == "") g = "0";
        if (b == "") b = "0";
        if (spare == "") spare = "0";

		WorldScript.Instance.Maximize(int.Parse(r), int.Parse(g), int.Parse(b), int.Parse(spare));
		UiManager.Instance.DisableForeGround();
	}
}
