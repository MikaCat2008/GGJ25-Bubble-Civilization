using System;
using BubbleApi;
using UnityEngine;


public class Test : MonoBehaviour
{
    public Storage storage;
    public SystemsContainer systems;
    public BuildingUpdater buildingUpdater;

    void Init()
    {
        this.storage = new Storage();
        this.systems = new SystemsContainer(this.storage);
        this.buildingUpdater = new BuildingUpdater(this.systems);
    }

    Bubble CreateBubble()
    {
        ResourcesContainer resources = new ResourcesContainer(
            500, 500, 500
        );
        BuildingsContainer buildings = new BuildingsContainer();
        
        Bubble bubble = new Bubble(resources, buildings);

        this.storage.bubbles.Add(0, bubble);

        Building house = this.systems.house.Build(0, bubble);

        this.systems.house.SetCapacity(house, 10);
        this.systems.house.Settle(house, bubble);
        this.systems.house.Settle(house, bubble);
        this.systems.house.Settle(house, bubble);
        this.systems.house.Settle(house, bubble);
        this.systems.house.Settle(house, bubble);

        return bubble;
    }

    void PrintResources(string text, Bubble bubble)
    {
        Debug.Log(text +
            " Ресурси: " +
            $"Їжа({bubble.resources.food}), " + 
            $"Кисень({bubble.resources.oxygen}), " + 
            $"Паливо({bubble.resources.fuel}), " + 
            $"Енергія({bubble.resources.energy}), " + 
            $"Населення({bubble.resources.freePopulation}/{bubble.resources.population}), " + 
            $"Будівельні матеріали({bubble.resources.materials})"
        );
    }

    void PrintBuildings(string text, Bubble bubble)
    {
        string[] buildings = new string[bubble.buildings.container.Count];

        int i = 0;

        foreach (Building building in bubble.buildings.container.Values)
        {
            BuildingSystem system = this.systems.GetBuildingSystem(building.GetBuildingType());
            buildings[i] = system.BuildingToString(building);

            i += 1;
        }

        Debug.Log(text + " Будівлі: " + String.Join(", ", buildings));
    }

    void Start()
    {
        return;

        this.Init();
        //this.Test_Gameplay();

        Bubble bubble = this.CreateBubble();

        // створення всіх видів будівель

        Building house1 = this.systems.house.Build(1, bubble);
        Building house2 = this.systems.house.Build(2, bubble);
        Building fuelMine = this.systems.mine.Build(3, bubble);
        Building materialsMine = this.systems.mine.Build(4, bubble);
        Building powerStation = this.systems.powerStation.Build(5, bubble);
        Building greenHouseFood = this.systems.greenHouse.Build(6, bubble);
        Building greenHouseOxygen = this.systems.greenHouse.Build(7, bubble);
        Building greenHouseMaterials = this.systems.greenHouse.Build(8, bubble);
        Building shipDockExploration = this.systems.shipDock.Build(9, bubble);
        Building shipDockTransfer = this.systems.shipDock.Build(10, bubble);
        Building airPurificationStation = this.systems.airPurificationStation.Build(11, bubble);

        // максимальна кількість жителів і працівників

        this.systems.house.SetCapacity(house1, 10);
        this.systems.house.SetCapacity(house2, 10);

        this.systems.mine.SetCapacity(fuelMine, 10);
        this.systems.mine.SetCapacity(materialsMine, 10);
        this.systems.powerStation.SetCapacity(powerStation, 10);
        this.systems.greenHouse.SetCapacity(greenHouseFood, 10);
        this.systems.greenHouse.SetCapacity(greenHouseOxygen, 10);
        this.systems.greenHouse.SetCapacity(greenHouseMaterials, 10);
        this.systems.shipDock.SetCapacity(shipDockExploration, 10);
        this.systems.shipDock.SetCapacity(shipDockTransfer, 10);
        this.systems.airPurificationStation.SetCapacity(airPurificationStation, 10);

        // режими

        this.systems.mine.SetMiningMode(fuelMine, MiningMode.Fuel);
        this.systems.mine.SetMiningMode(materialsMine, MiningMode.Materials);
        this.systems.greenHouse.SetGeneratingMode(greenHouseFood, GeneratingMode.Food);
        this.systems.greenHouse.SetGeneratingMode(greenHouseOxygen, GeneratingMode.Oxygen);
        this.systems.greenHouse.SetGeneratingMode(greenHouseMaterials, GeneratingMode.Materials);
        this.systems.shipDock.SetDockMode(shipDockExploration, DockMode.Exploration);
        this.systems.shipDock.SetDockMode(shipDockTransfer, DockMode.Transfer);

        // заселення жителів і назначення працівників

        this.systems.house.Settle(house1, bubble);
        this.systems.house.Settle(house1, bubble);
        this.systems.house.Settle(house1, bubble);
        this.systems.house.Settle(house2, bubble);
        this.systems.house.Settle(house2, bubble);
        this.systems.house.Settle(house2, bubble);

        this.systems.mine.Hire(fuelMine, bubble);
        this.systems.mine.Hire(materialsMine, bubble);
        this.systems.powerStation.Hire(powerStation, bubble);
        this.systems.powerStation.Hire(powerStation, bubble);
        this.systems.greenHouse.Hire(greenHouseFood, bubble);
        this.systems.greenHouse.Hire(greenHouseOxygen, bubble);
        this.systems.greenHouse.Hire(greenHouseMaterials, bubble);
        this.systems.shipDock.Hire(shipDockExploration, bubble);
        this.systems.shipDock.Hire(shipDockTransfer, bubble);
        this.systems.airPurificationStation.Hire(airPurificationStation, bubble);
    }
}
