using BubbleApi;
using UnityEngine;


public class Api : MonoBehaviour
{
    public void Start()
    {
        GlobalStorage.Initialize();

        GlobalStorage.systems.house.StartBuilding(
            0, GlobalStorage.storage.bubbles[0]
        );

        GlobalStorage.buildingUpdater.OnBuildingDone += (Building building, Bubble bubble) =>
        {

        };
    }

    public void FixedUpdate()
    {
        if (GlobalStorage.storage.timer.speed == 0)
            return;

        foreach (Bubble bubble in GlobalStorage.storage.bubbles.Values)
            GlobalStorage.buildingUpdater.Update(bubble);

        GlobalStorage.storage.timer.Tick();
    }
}
