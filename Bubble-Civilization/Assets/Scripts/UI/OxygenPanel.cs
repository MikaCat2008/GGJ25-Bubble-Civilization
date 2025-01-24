using BubbleApi;
using UnityEngine;


public class OxygenPanel : MonoBehaviour
{
    public ScalePanel scalePanel;

    void Start()
    {
        this.scalePanel = GetComponent<ScalePanel>();
    }

    void Update()
    {
        float oxygen = (float)GlobalStorage.storage.currentBubble.resources.oxygen;

        this.scalePanel.SetValue(oxygen / 100f);
    }
}
