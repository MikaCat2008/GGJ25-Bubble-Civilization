using BubbleApi;
using UnityEngine;


public class Test : MonoBehaviour
{
    public Storage storage;
    public House_BuildingSystem house_system;
    public Mine_BuildingSystem mine_system;

    void Start()
    {
        // це просто зберігати де завгодно

        this.storage = new Storage();
        this.house_system = new House_BuildingSystem();
        this.house_system.storage = this.storage;
        this.mine_system = new Mine_BuildingSystem();
        this.mine_system.storage = this.storage;

        // приклад створення бульбашки з ресурсами всі під 100

        ResourcesContainer resources = new ResourcesContainer(
            100, 100, 100, 100, 100, 100, 100
        );
        BuildingsContainer buildings = new BuildingsContainer();

        Bubble bubble = new Bubble(resources, buildings);

        this.storage.bubbles.Add(bubble);

        // помилка, якщо в систему для шахти закинути щось інше (в цьому випадку неіснуючу будівлю)

        try 
        { 
            Building _building = bubble.buildings.GetBuilding(2);

            Debug.Log(mine_system.BuildingToString(_building));
        }
        catch (BubbleApiException exception)
        {
            Debug.Log($"Exception: ${exception}");
        }

        // зміна типу будівлі на будинок по 2 айді

        bubble.buildings.SetBuildingType(2, BuildingType.House);
        
        Building building = bubble.buildings.GetBuilding(2);

        Debug.Log(house_system.BuildingToString(building));

        // змінює дані
        this.house_system.Update(building);

        Debug.Log(house_system.BuildingToString(building));

        // помилка, якщо в систему для будинку закинути щось інше (в цьому випадку шахту)

        try
        {
            Building _building = bubble.buildings.GetBuilding(2);

            Debug.Log(mine_system.BuildingToString(_building));
        }
        catch (BubbleApiException exception)
        {
            Debug.Log($"Exception: ${exception}");
        }
    }

    void Update()
    {

    }
}
