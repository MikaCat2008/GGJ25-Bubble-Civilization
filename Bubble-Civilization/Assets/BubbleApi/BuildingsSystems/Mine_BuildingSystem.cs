namespace BubbleApi
{
    public class Mine_BuildingSystem : BuildingSystem
    {
        public override void OnUpdate(Building building, Bubble bubble)
        {
            this.Mine(building, bubble);
        }

        public void StartBuilding(int id, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.Mine_Build);

            this.buildingUpdater.StartBuilding(id, bubble, BuildingType.Mine);
            bubble.buildings.SetBuildingType(id, BuildingType.Building);
        }

        public override Building Build(int id, Bubble bubble)
        {
            Building mine = this.Build(id, bubble, BuildingType.Mine);

            mine.data = new Mine_BuildingData();

            return mine;
        }

        public void SetMiningMode(Building building, MiningMode miningMode)
        {
            Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);

            data.SetMiningMode(miningMode);
        }

        public void SetCapacity(Building building, int capacity)
        {
            Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);

            data.capacity = capacity;
        }

        public void Hire(Building building, Bubble bubble)
        {
            Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            if (data.count >= data.capacity)
                throw new BubbleApiException(
                    BubbleApiExceptionType.BuildingIsFull
                );

            this.actionUpdater.ProcessAction(bubble, ActionType.Mine_Hire);

            data.count += 1;
        }

        public void Mine(Building building, Bubble bubble)
        {
            Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            this.actionUpdater.ProcessAction(bubble, ActionType.Mine_Update, building);

            MiningMode miningMode = data.GetMiningMode();

            if (miningMode == MiningMode.Fuel)
            {
                bubble.resources.fuel += 2 * data.count;
            }
            else if (miningMode == MiningMode.Materials)
            {
                bubble.resources.materials += 2 * data.count;
            }
        }

        public void RepairBuilding(Building building, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.Mine_Repair);

            this.RepairBuilding(building);
        }

        public override void Destroy(Building building, Bubble bubble)
        {
            Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);

            bubble.resources.freePopulation += data.count;
            base.Destroy(building, bubble);
        }

        public override string BuildingToString(Building building)
        {
            Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);

            MiningMode miningMode = data.GetMiningMode();

            string modeText = this.MiningModeToString(miningMode);
            string brokenText = this.BrokenText(building);

            return brokenText + $"Шахта[{building.id}]<режим={modeText} працівники={data.count}/{data.capacity}>";
        }

        private string MiningModeToString(MiningMode miningMode)
        {
            if (miningMode == MiningMode.Free)
                return "вільна";
            if (miningMode == MiningMode.Fuel)
                return "паливо";
            return "будівельні матеріали";
        }
    }
}
