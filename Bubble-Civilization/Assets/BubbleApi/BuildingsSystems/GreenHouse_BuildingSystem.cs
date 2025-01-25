namespace BubbleApi
{
    public class GreenHouse_BuildingSystem : BuildingSystem
    {
        public override void OnUpdate(Building building, Bubble bubble)
        {
            this.Generate(building, bubble);
        }

        public override void StartBuilding(int id, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.GreenHouse_Build);

            this.buildingUpdater.StartBuilding(id, bubble, BuildingType.GreenHouse);
            bubble.buildings.SetBuildingType(id, BuildingType.Building);
        }

        public override Building Build(int id, Bubble bubble)
        {
            Building greenHouse = this.Build(id, bubble, BuildingType.GreenHouse);

            greenHouse.data = new GreenHouse_BuildingData();

            return greenHouse;
        }

        public void SetGeneratingMode(Building building, GeneratingMode generatingMode)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            data.SetGeneratingMode(generatingMode);
        }

        public void SetCapacity(Building building, int capacity)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            data.capacity = capacity;
        }

        public void Hire(Building building, Bubble bubble)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            if (data.count >= data.capacity)
                throw new BubbleApiException(
                    BubbleApiExceptionType.BuildingIsFull
                );

            this.actionUpdater.ProcessAction(bubble, ActionType.GreenHouse_Hire);

            data.count += 1;
        }

        public void Generate(Building building, Bubble bubble)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            this.actionUpdater.ProcessAction(bubble, ActionType.GreenHouse_Update, building);

            GeneratingMode generatingMode = data.GetGeneratingMode();

            if (generatingMode == GeneratingMode.Food)
            {
                bubble.resources.food += 2 * data.count;
            }
            else if (generatingMode == GeneratingMode.Oxygen)
            {
                bubble.resources.oxygen += 5 * data.count;
            }
            else if (generatingMode == GeneratingMode.Materials)
            {
                bubble.resources.materials += data.count;
            }
        }

        public override void RepairBuilding(Building building, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.GreenHouse_Repair);

            base.RepairBuilding(building, bubble);
        }

        public override void Destroy(Building building, Bubble bubble)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            bubble.resources.freePopulation += data.count;
            base.Destroy(building, bubble);
        }

        public override string BuildingToString(Building building)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            GeneratingMode generatingMode = data.GetGeneratingMode();

            string modeText = this.GeneratingModeToString(generatingMode);
            string brokenText = this.BrokenText(building);

            return brokenText + $"Теплиця[{building.id}]<режим={modeText} працівники={data.count}/{data.capacity}>";
        }

        private string GeneratingModeToString(GeneratingMode generatingMode)
        {
            if (generatingMode == GeneratingMode.Free)
                return "вільна";
            if (generatingMode == GeneratingMode.Food)
                return "їжа";
            if (generatingMode == GeneratingMode.Oxygen)
                return "кисень";
            return "будівельні матеріали";
        }
    }
}
