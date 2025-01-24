using BubbleApi;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubbleUI : MonoBehaviour
{
    [SerializeField] GameObject bubbleManagerObject;

    //Icons would be set in editor
    //[SerializeField] Image populationIcon;
    [SerializeField] TMP_Text populationText;
    //[SerializeField] Image foodIcon;
    [SerializeField] TMP_Text foodText;
    //[SerializeField] Image oxygenIcon;
    [SerializeField] TMP_Text oxygenText;
    //[SerializeField] Image materialsIcon;
    [SerializeField] TMP_Text materialsText;
    //[SerializeField] Image fuelIcon;
    [SerializeField] TMP_Text fuelText;


    private BubbleManager bubbleManager;
    private Bubble currentBubble;

    void Start()
    {
        bubbleManager = bubbleManagerObject.GetComponent<BubbleManager>();
        currentBubble = bubbleManager.GetCurrentBubble();

        Bind();
        UpdateEverything();
    }

    private void Bind()
    {

        currentBubble.populationResource.OnResourceQuantityChanged += UpdatePopulationQuantity;
        currentBubble.foodResource.OnResourceQuantityChanged += UpdateFoodQuantity;
        currentBubble.oxygenResource.OnResourceQuantityChanged += UpdateOxygenQuantity;
        currentBubble.materialsResource.OnResourceQuantityChanged += UpdateMaterialsQuantity;
        currentBubble.fuelResource.OnResourceQuantityChanged += UpdateFuelQuantity;
    }

    private void UnBind()
    {
        currentBubble.populationResource.OnResourceQuantityChanged -= UpdatePopulationQuantity;
        currentBubble.foodResource.OnResourceQuantityChanged -= UpdateFoodQuantity;
        currentBubble.oxygenResource.OnResourceQuantityChanged -= UpdateOxygenQuantity;
        currentBubble.materialsResource.OnResourceQuantityChanged -= UpdateMaterialsQuantity;
        currentBubble.fuelResource.OnResourceQuantityChanged-= UpdateFuelQuantity;
    }

    private void UpdateEverything()
    {
        populationText.text = currentBubble.populationResource.Quantity.ToString();
        foodText.text = currentBubble.foodResource.Quantity.ToString();
        oxygenText.text = currentBubble.oxygenResource.Quantity.ToString();
        materialsText.text = currentBubble.materialsResource.Quantity.ToString();
        fuelText.text = currentBubble.fuelResource.Quantity.ToString();

    }

    private void UpdatePopulationQuantity(int Quantity)
    {
        populationText.text = Quantity.ToString();
    }
    private void UpdateFoodQuantity(int Quantity)
    {
        foodText.text = Quantity.ToString();
    }
    private void UpdateOxygenQuantity(int Quantity)
    {
        oxygenText.text = Quantity.ToString();
    }
    private void UpdateMaterialsQuantity(int Quantity)
    {
        materialsText.text = Quantity.ToString();
    }
    private void UpdateFuelQuantity(int Quantity)
    {
        fuelText.text = Quantity.ToString();
    }

}
