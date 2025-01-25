using TMPro;
using BubbleApi;
using UnityEngine;


public class ResourcesPanel : MonoBehaviour
{
    [SerializeField] TMP_Text foodText;
    [SerializeField] TMP_Text fuelText;
    [SerializeField] TMP_Text energyText;
    [SerializeField] TMP_Text materialsText;
    [SerializeField] TMP_Text populationText;
    [SerializeField] TMP_Text freePopulationText;
    
    void Start()
    {
        GlobalStorage.resourcesUpdater.OnFoodChanged += (int value) =>
            this.foodText.text = value.ToString();
        GlobalStorage.resourcesUpdater.OnFuelChanged += (int value) =>
            this.fuelText.text = value.ToString();
        GlobalStorage.resourcesUpdater.OnEnergyChanged += (int value) =>
            this.energyText.text = value.ToString();
        GlobalStorage.resourcesUpdater.OnMaterialsChanged += (int value) =>
            this.materialsText.text = value.ToString();
        GlobalStorage.resourcesUpdater.OnPopulationChanged += (int value) =>
            this.populationText.text = value.ToString();
        GlobalStorage.resourcesUpdater.OnFreePopulationChanged += (int value) =>
            this.freePopulationText.text = value.ToString();

        this.foodText.text = GlobalStorage.storage.currentBubble.resources.food.ToString();
        this.fuelText.text = GlobalStorage.storage.currentBubble.resources.fuel.ToString();
        this.energyText.text = GlobalStorage.storage.currentBubble.resources.energy.ToString();
        this.materialsText.text = GlobalStorage.storage.currentBubble.resources.materials.ToString();
        this.populationText.text = GlobalStorage.storage.currentBubble.resources.population.ToString();
        this.freePopulationText.text = GlobalStorage.storage.currentBubble.resources.freePopulation.ToString();
    }

    void Update()
    {
        
    }
}
