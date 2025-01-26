using BubbleApi;
using UnityEngine;


public class OxygenPanel : MonoBehaviour
{
    ScalePanel scalePanel;

    void Start()
    {
        scalePanel = GetComponent<ScalePanel>();

        if (scalePanel != null )
        {
            GlobalStorage.resourcesUpdater.OnOxygenChanged += (int value) =>
            {
                scalePanel.SetValue((float)value / 100f);
            };
        }
        else
        {
            Debug.LogWarning("OxygenPanel, scalePanel is null");
        }

    }
}
