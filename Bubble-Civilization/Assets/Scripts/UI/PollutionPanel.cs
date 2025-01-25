using BubbleApi;
using UnityEngine;


public class PollutionPanel : MonoBehaviour
{
    void Start()
    {
        ScalePanel scalePanel = GetComponent<ScalePanel>();

        GlobalStorage.resourcesUpdater.OnPollutionChanged += (int value) =>
        {
            scalePanel.SetValue((float)value / 1000f);
        };
    }
}
