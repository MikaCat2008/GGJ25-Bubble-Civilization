using TMPro;
using BubbleApi;
using UnityEngine;


public class BuildingMenu_Mine : BuildingMenu
{
    [SerializeField] TMP_Text modeText;

    public override void Show()
    {
        Building building = WindowManager.GetActiveBuilding();
        Mine_BuildingData data = (Mine_BuildingData)building.data;

        this.modeText.text = this.MiningModeToString(data.GetMiningMode());
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
            GlobalStorage.systems.mine.Hire(building, bubble);
        }
        catch (BubbleApiException exception)
        {
            if (exception.type == BubbleApiExceptionType.BuildingIsFull)
                this.SetErrorMessage("В шахті немає місця!");
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
        Mine_BuildingData data = (Mine_BuildingData)building.data;

        MiningMode miningMode = data.GetMiningMode();

        if (miningMode == MiningMode.Free)
            miningMode = MiningMode.Fuel;
        else if (miningMode == MiningMode.Fuel)
            miningMode = MiningMode.Materials;
        else if (miningMode == MiningMode.Materials)
            miningMode = MiningMode.Free;

        data.SetMiningMode(miningMode);
        this.modeText.text = this.MiningModeToString(miningMode);
    }

    private void UpdateProperties() 
    {
        Building building = WindowManager.GetActiveBuilding();
        Mine_BuildingData data = (Mine_BuildingData)building.data;

        this.SetProperties(
            $"Кількість працівників: {data.count} / {data.capacity}"
        );
    }

    private string MiningModeToString(MiningMode mode)
    {
        if (mode == MiningMode.Fuel)
            return "Паливо";
        else if (mode == MiningMode.Materials)
            return "Матеріали";
        return "Вільна";
    }
}
