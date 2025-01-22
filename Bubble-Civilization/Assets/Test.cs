using BubbleApi;
using UnityEngine;


public class Test : MonoBehaviour
{
    public Storage storage;
    public BuildingSystem building_system;
    public House_BuildingSystem house_system;
    public Mine_BuildingSystem mine_system;

    void Init()
    {
        // це просто зберігати де завгодно

        this.storage = new Storage();
        this.building_system = new BuildingSystem();
        this.building_system.storage = this.storage;
        this.house_system = new House_BuildingSystem();
        this.house_system.storage = this.storage;
        this.mine_system = new Mine_BuildingSystem();
        this.mine_system.storage = this.storage;
    }

    Bubble CreateBubble()
    {
        // приклад створення бульбашки з ресурсами всі під 100

        ResourcesContainer resources = new ResourcesContainer(
            100, 100, 100, 100, 100, 100, 100
        );
        BuildingsContainer buildings = new BuildingsContainer();

        Bubble bubble = new Bubble(resources, buildings);

        this.storage.bubbles.Add(bubble);

        return bubble;
    }

    void Test_Gameplay()
    {
        Bubble bubble = this.CreateBubble();


        /// СЕКТОР З БУДИНКОМ


        // будівля з айді 1 стала будинком
        // витратилось 30 будівельного матеріалу і 20 їжі

        Building house = this.house_system.Build(1, bubble);

        Debug.Log($"Кількість їжі після 1 і 2 заселення: {bubble.resources.food}"); // 90

        // задати максимальне населення 3 людини
        this.house_system.SetCapacity(house, 3);

        // емуляція кнопки "Заселити", потребує 5 їжі, інакше - помилка
        this.house_system.Settle(house, bubble);
        this.house_system.Settle(house, bubble);

        Debug.Log($"Кількість їжі після 1 і 2 заселення: {bubble.resources.food}"); // 90
        Debug.Log(this.house_system.BuildingToString(house)); // Будинок<count=2 capacity=3>

        // зламати будівлю(населення збережеться, проте не можна заселяти нових людей)
        this.building_system.BreakBuilding(house);

        Debug.Log(this.house_system.BuildingToString(house)); // (Зламано) Будинок<count=2 capacity=3>
        
        // this.house_system.Settle(house, bubble); // помилка, будинок зламаний

        // відремонтувати будівлю(потребує 2 їжі, інакше - помилка)
        // якщо не передавати аргумент bubble, то їжа не витратиться
        this.house_system.RepairBuilding(house, bubble);

        Debug.Log($"Кількість їжі після ремонтування: {bubble.resources.food}"); // 88

        this.house_system.Settle(house, bubble);
        // this.house_system.Settle(house, bubble); // помилка, місце закінчилось

        Debug.Log($"Кількість їжі після 3 заселення: {bubble.resources.food}"); // 83

        // зруйнувати будинок
        this.house_system.Destroy(house, bubble);

        Building building = bubble.buildings.GetBuilding(1);
        Debug.Log(this.building_system.BuildingToString(building)); // Пусто


        /// СЕКТОР З ШАХТОЮ
    }

    void Start()
    {
        this.Init();
        this.Test_Gameplay();
    }

    void Update()
    {

    }
}
