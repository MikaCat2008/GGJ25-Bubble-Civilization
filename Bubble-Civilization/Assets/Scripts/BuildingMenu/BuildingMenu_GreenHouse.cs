using TMPro;
using BubbleApi;
using UnityEngine;


public class BuildingMenu_GreenHouse : BuildingMenu
{
    [SerializeField] TMP_Text modeText;

    public override void Show()
    {
        this.SetErrorMessage("");
        this.UpdateProperties();

        base.Show();
    }

    public void OnHire()
    {
        Bubble bubble = GlobalStorage.storage.currentBubble;
        Building building = WindowManager.GetActiveBuilding();

        try
        {
            GlobalStorage.systems.greenHouse.Hire(building, bubble);
        }
        catch (BubbleApiException exception)
        {
            if (exception.type == BubbleApiExceptionType.BuildingIsFull)
                this.SetErrorMessage("В теплиці немає місця!");
            else if (exception.type == BubbleApiExceptionType.NotEnoughResources)
                this.SetErrorMessage("Не вистачає ресурсів!");
            else
                this.SetErrorMessage(exception.type.ToString());
        }

        this.UpdateProperties();
    }

    public void OnChangeMode()
    {
        Building building = WindowManager.GetActiveBuilding();
        GreenHouse_BuildingData data = (GreenHouse_BuildingData)building.data;

        GeneratingMode generatingMode = data.GetGeneratingMode();

        if (generatingMode == GeneratingMode.Free)
        {
            this.modeText.text = "Їжа";

            generatingMode = GeneratingMode.Food;
        }
        else if (generatingMode == GeneratingMode.Food)
        {
            this.modeText.text = "Матеріали";

            generatingMode = GeneratingMode.Materials;
        }
        else if (generatingMode == GeneratingMode.Materials)
        {
            this.modeText.text = "Кисень";

            generatingMode = GeneratingMode.Oxygen;
        }
        else if (generatingMode == GeneratingMode.Oxygen)
        {
            this.modeText.text = "Вільна";

            generatingMode = GeneratingMode.Free;
        }

        data.SetGeneratingMode(generatingMode);
    }

    private void UpdateProperties() 
    {
        Building building = WindowManager.GetActiveBuilding();
        GreenHouse_BuildingData data = (GreenHouse_BuildingData)building.data;

        this.SetProperties(
            $"Кількість працівників: {data.count} / {data.capacity}"
        );
    }
}
