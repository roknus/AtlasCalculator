using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class AlertMessage : MonoBehaviour 
{
    public Text Message;
    public Button m_Ok;

    public void CloseWindow()
    {
        UiManager.Instance.DisableForeGround();
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(m_Ok.gameObject);
    }
}
