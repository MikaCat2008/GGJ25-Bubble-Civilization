using BubbleApi;
using System;
using UnityEngine;
using System;

public class BubbleManager : MonoBehaviour
{
    private Bubble currentBubble;
    public event Action OnPopulationDied;
    
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

        SetStartBubble();

    }

    public void SetStartBubble()
    {
        currentBubble = new Bubble();
        currentBubble.SetStartResources();
        currentBubble.OnPopulationDied += OnPopulationDiedHandler;

        currentBubble.ResourcesToDebugString();
    }

    private void Start()
    {
        Bind();
    }

    private void Bind()
    {
        GameplayTimeManager.OnDayPassed += currentBubble.BreathPerDay;
        GameplayTimeManager.OnDayPassed += currentBubble.EatPerDay;
    }
    private void UnBind()
    {
        GameplayTimeManager.OnDayPassed -= currentBubble.BreathPerDay;
        GameplayTimeManager.OnDayPassed -= currentBubble.EatPerDay;
    }

    public Bubble GetCurrentBubble()
    {
        return currentBubble;
    }

    private void OnPopulationDiedHandler()
    {
        UnBind();
        currentBubble.OnPopulationDied -= OnPopulationDiedHandler;
        OnPopulationDied?.Invoke();
    }
}
