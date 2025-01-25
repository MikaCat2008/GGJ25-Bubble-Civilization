using BubbleApi;
using UnityEngine;


public class OxygenPanel : MonoBehaviour
{
    void Start()
    {
        ScalePanel scalePanel = GetComponent<ScalePanel>();

        GlobalStorage.resourcesUpdater.OnOxygenChanged += (int value) =>
        {
            scalePanel.SetValue((float)value / 100f);
        };
    }
}
