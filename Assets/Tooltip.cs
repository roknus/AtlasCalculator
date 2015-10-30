using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string m_Tooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipPanel.Instance.gameObject.SetActive(true);
        TooltipPanel.Instance.Enable(m_Tooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipPanel.Instance.Disable();
    }
}
