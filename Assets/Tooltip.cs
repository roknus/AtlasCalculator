using UnityEngine;
using System.Collections;

public class Tooltip : MonoBehaviour 
{
    public string m_Tooltip;

    public void ShowTooltip()
    {
        TooltipPanel.Instance.gameObject.SetActive(true);
        TooltipPanel.Instance.Enable(m_Tooltip);
    }

    public void HideTooltip()
    {
        TooltipPanel.Instance.Disable();
        TooltipPanel.Instance.gameObject.SetActive(false);
    }
}
