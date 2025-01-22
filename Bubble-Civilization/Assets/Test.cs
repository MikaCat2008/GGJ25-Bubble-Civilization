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
        // приклад створення бульбашки

        ResourcesContainer resources = new ResourcesContainer(
            200, 200, 200, 100, 100, 200, 0
        );
        BuildingsContainer buildings = new BuildingsContainer();

        Bubble bubble = new Bubble(resources, buildings);

        this.storage.bubbles.Add(bubble);

        return bubble;
    }

    void PrintResources(string text, Bubble bubble)
    {
        Debug.Log(text + " " +
            "Ресурси: " +
            $"Їжа({bubble.resources.food}), " + 
            $"Щастя({bubble.resources.happiness}), " + 
            $"Кисень({bubble.resources.oxygen}), " + 
            $"Паливо({bubble.resources.fuel}), " + 
            $"Енергія({bubble.resources.energy}), " + 
            $"Населення({bubble.resources.population}), " + 
            $"Будівельні матеріали({bubble.resources.materials})"
        );
    }

    void Test_Gameplay()
    {
        Bubble bubble = this.CreateBubble();


        /// СЕКТОР З БУДИНКОМ
        this.PrintResources("Сектор з будинком.", bubble);


        // будівля з айді 1 стала будинком
        // витратилось 20 їжі і 30 будівельного матеріалу

        Building house = this.house_system.Build(1, bubble);

        this.PrintResources("Будівництво будинку.", bubble);

        // задати максимальне населення 3 людини
        this.house_system.SetCapacity(house, 3);

        // емуляція кнопки "Заселити", потребує 5 їжі, інакше - помилка
        this.house_system.Settle(house, bubble);
        this.house_system.Settle(house, bubble);

        this.PrintResources("Заселення будинку 2 рази.", bubble);

        Debug.Log(this.house_system.BuildingToString(house)); // Будинок<count=2 capacity=3>

        // зламати будівлю(населення збережеться, проте не можна заселяти нових людей)
        this.building_system.BreakBuilding(house);

        Debug.Log(this.house_system.BuildingToString(house)); // (Зламано) Будинок<count=2 capacity=3>
        
        // this.house_system.Settle(house, bubble); // помилка, будинок зламаний

        // відремонтувати будівлю(потребує 2 їжі, інакше - помилка)
        // якщо не передавати аргумент bubble, то їжа не витратиться
        this.house_system.RepairBuilding(house, bubble);

        this.PrintResources("Ремонтування будинку.", bubble);

        this.house_system.Settle(house, bubble);
        // this.house_system.Settle(house, bubble); // помилка, місце закінчилось

        this.PrintResources("Заселення будинку останній раз.", bubble);

        // зруйнувати будинок
        this.house_system.Destroy(house, bubble);

        Building building = bubble.buildings.GetBuilding(1);
        Debug.Log(this.building_system.BuildingToString(building)); // Пусто


        /// СЕКТОР З ШАХТОЮ
        
        
        this.PrintResources("Сектор з шахтою.", bubble);
        
        Building fuel_mine = this.mine_system.Build(2, bubble);
        Building materials_mine = this.mine_system.Build(3, bubble);
        
        this.mine_system.SetMiningMode(fuel_mine, MiningMode.Fuel);
        this.mine_system.SetMiningMode(materials_mine, MiningMode.Materials);

        this.PrintResources("Будівництво шахт.", bubble);
        
        Debug.Log(
            "Шахти: " +
            $"{this.mine_system.BuildingToString(fuel_mine)}, " +
            $"{this.mine_system.BuildingToString(materials_mine)}"
        );

        this.mine_system.Mine(fuel_mine, bubble);
        this.mine_system.Mine(materials_mine, bubble);

        this.PrintResources("Видобування в шахтах.", bubble);
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
