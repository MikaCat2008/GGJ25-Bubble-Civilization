using TMPro;
using BubbleApi;
using UnityEngine;


public class BuildingMenu_GreenHouse : BuildingMenu
{
    [SerializeField] TMP_Text modeText;

    public override void Show()
    {
        Building building = WindowManager.GetActiveBuilding();
        GreenHouse_BuildingData data = (GreenHouse_BuildingData)building.data;

        this.modeText.text = this.GeneratingModeToString(data.GetGeneratingMode());
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
            generatingMode = GeneratingMode.Food;
        else if (generatingMode == GeneratingMode.Food)
            generatingMode = GeneratingMode.Materials;
        else if (generatingMode == GeneratingMode.Materials)
            generatingMode = GeneratingMode.Oxygen;
        else if (generatingMode == GeneratingMode.Oxygen)
            generatingMode = GeneratingMode.Free;

        data.SetGeneratingMode(generatingMode);
        this.modeText.text = this.GeneratingModeToString(generatingMode);
    }

    private void UpdateProperties() 
    {
        Building building = WindowManager.GetActiveBuilding();
        GreenHouse_BuildingData data = (GreenHouse_BuildingData)building.data;

        this.SetProperties(
            $"Кількість працівників: {data.count} / {data.capacity}"
        );
    }

    private string GeneratingModeToString(GeneratingMode mode)
    {
        if (mode == GeneratingMode.Food)
            return "Їжа";
        else if (mode == GeneratingMode.Materials)
            return "Матеріали";
        else if (mode == GeneratingMode.Oxygen)
            return "Кисень";
        return "Вільна";
    }
}
