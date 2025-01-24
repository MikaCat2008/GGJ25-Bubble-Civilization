using System;
using BubbleApi;
using UnityEngine;


public class Test : MonoBehaviour
{
    void PrintResources(string text, Bubble bubble)
    {
        Debug.Log(text +
            " Ресурси: " +
            $"Їжа({bubble.resources.food}), " + 
            $"Кисень({bubble.resources.oxygen}/100), " + 
            $"Паливо({bubble.resources.fuel}), " + 
            $"Енергія({bubble.resources.energy}), " + 
            $"Населення({bubble.resources.freePopulation}/{bubble.resources.population}), " + 
            $"Забруднення({bubble.resources.pollution}/1000), " +
            $"Будівельні матеріали({bubble.resources.materials})"
        );
    }

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

    void Start()
    {
        return;

        Bubble bubble = GlobalStorage.storage.bubbles[0];

        // створення всіх видів будівель

        this.PrintBuildings("", bubble);
        this.PrintResources("", bubble);

        Building house1 = GlobalStorage.systems.house.Build(1, bubble);
        Building house2 = GlobalStorage.systems.house.Build(2, bubble);
        Building fuelMine = GlobalStorage.systems.mine.Build(3, bubble);
        Building materialsMine = GlobalStorage.systems.mine.Build(4, bubble);
        Building powerStation = GlobalStorage.systems.powerStation.Build(5, bubble);
        Building greenHouseFood = GlobalStorage.systems.greenHouse.Build(6, bubble);
        Building greenHouseOxygen = GlobalStorage.systems.greenHouse.Build(7, bubble);
        Building greenHouseMaterials = GlobalStorage.systems.greenHouse.Build(8, bubble);
        Building shipDockExploration = GlobalStorage.systems.shipDock.Build(9, bubble);
        Building shipDockTransfer = GlobalStorage.systems.shipDock.Build(10, bubble);
        Building airPurificationStation = GlobalStorage.systems.airPurificationStation.Build(11, bubble);

        // максимальна кількість жителів і працівників

        GlobalStorage.systems.house.SetCapacity(house1, 10);
        GlobalStorage.systems.house.SetCapacity(house2, 10);

        GlobalStorage.systems.mine.SetCapacity(fuelMine, 10);
        GlobalStorage.systems.mine.SetCapacity(materialsMine, 10);
        GlobalStorage.systems.powerStation.SetCapacity(powerStation, 10);
        GlobalStorage.systems.greenHouse.SetCapacity(greenHouseFood, 10);
        GlobalStorage.systems.greenHouse.SetCapacity(greenHouseOxygen, 10);
        GlobalStorage.systems.greenHouse.SetCapacity(greenHouseMaterials, 10);
        GlobalStorage.systems.shipDock.SetCapacity(shipDockExploration, 10);
        GlobalStorage.systems.shipDock.SetCapacity(shipDockTransfer, 10);
        GlobalStorage.systems.airPurificationStation.SetCapacity(airPurificationStation, 10);

        // режими

        GlobalStorage.systems.mine.SetMiningMode(fuelMine, MiningMode.Fuel);
        GlobalStorage.systems.mine.SetMiningMode(materialsMine, MiningMode.Materials);
        GlobalStorage.systems.greenHouse.SetGeneratingMode(greenHouseFood, GeneratingMode.Food);
        GlobalStorage.systems.greenHouse.SetGeneratingMode(greenHouseOxygen, GeneratingMode.Oxygen);
        GlobalStorage.systems.greenHouse.SetGeneratingMode(greenHouseMaterials, GeneratingMode.Materials);
        GlobalStorage.systems.shipDock.SetDockMode(shipDockExploration, DockMode.Exploration);
        GlobalStorage.systems.shipDock.SetDockMode(shipDockTransfer, DockMode.Transfer);

        // заселення жителів і назначення працівників
        
        this.PrintBuildings("", bubble);
        this.PrintResources("", bubble);

        GlobalStorage.systems.house.Settle(house1, bubble);
        GlobalStorage.systems.house.Settle(house1, bubble);
        GlobalStorage.systems.house.Settle(house1, bubble);
        GlobalStorage.systems.house.Settle(house2, bubble);
        GlobalStorage.systems.house.Settle(house2, bubble);
        GlobalStorage.systems.house.Settle(house2, bubble);

        GlobalStorage.systems.mine.Hire(fuelMine, bubble);
        GlobalStorage.systems.mine.Hire(materialsMine, bubble);
        GlobalStorage.systems.powerStation.Hire(powerStation, bubble);
        GlobalStorage.systems.powerStation.Hire(powerStation, bubble);
        GlobalStorage.systems.greenHouse.Hire(greenHouseFood, bubble);
        GlobalStorage.systems.greenHouse.Hire(greenHouseOxygen, bubble);
        GlobalStorage.systems.greenHouse.Hire(greenHouseMaterials, bubble);
        GlobalStorage.systems.shipDock.Hire(shipDockExploration, bubble);
        GlobalStorage.systems.shipDock.Hire(shipDockTransfer, bubble);
        GlobalStorage.systems.airPurificationStation.Hire(airPurificationStation, bubble);

        this.PrintBuildings("", bubble);
        this.PrintResources("", bubble);
    }
}
