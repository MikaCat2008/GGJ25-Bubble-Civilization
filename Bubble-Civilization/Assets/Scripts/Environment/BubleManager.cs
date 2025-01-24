using BubbleApi;
using System;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    private Bubble currentBubble;

    void PrintBuildings(string text, Bubble bubble)
    {
        string[] buildings = new string[bubble.buildings.container.Count];

        int i = 0;

        foreach (Building building in bubble.buildings.container.Values)
        {
            BuildingSystem system = GlobalStorage.systems.GetBuildingSystem(building.GetBuildingType());
            buildings[i] = system.BuildingToString(building);

            i += 1;
        }

        Debug.Log(text + " Будівлі: " + String.Join(", ", buildings));
    }

    void Awake()
    {
        currentBubble = new Bubble();
        currentBubble.SetStartResources();


        currentBubble.ResourcesToDebugString();
    }

    public Bubble GetCurrentBubble()
    {
        return currentBubble;
    }
}
