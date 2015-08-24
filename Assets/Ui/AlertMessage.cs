using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlertMessage : MonoBehaviour 
{
    public Text Message;

    public void CloseWindow()
    {
        UiManager.Instance.DisableForeGround();
        gameObject.SetActive(false);
    }
}
