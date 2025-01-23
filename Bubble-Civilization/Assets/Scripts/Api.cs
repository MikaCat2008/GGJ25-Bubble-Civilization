using BubbleApi;
using UnityEngine;


public class Api : MonoBehaviour
{
    public void FixedUpdate()
    {
        if (GlobalStorage.storage.timer.speed != 0)
            return;

        foreach (Bubble bubble in GlobalStorage.storage.bubbles.Values)
            GlobalStorage.buildingUpdater.Update(bubble);

        GlobalStorage.storage.timer.Tick();
    }
}
