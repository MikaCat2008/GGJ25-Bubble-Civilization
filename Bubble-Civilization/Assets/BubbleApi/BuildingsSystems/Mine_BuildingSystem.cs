namespace BubbleApi
{
    public class Mine_BuildingSystem : BuildingSystem
    {
        public override void Update(Building building, Bubble bubble)
        {
            if (this.storage.timer.ticks % 600 == 0)
            {
                this.Mine(building, bubble);
            }
        }

        public void StartBuilding(int id, Bubble bubble)
        {
            if (bubble.resources.food < 25 || bubble.resources.materials < 40)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            this.buildingUpdater.StartBuilding(id, bubble, BuildingType.Mine, 300);

            bubble.resources.food -= 25;
            bubble.resources.materials -= 40;
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

            if (bubble.resources.freePopulation < 1)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            data.count += 1;
            bubble.resources.freePopulation -= 1;
        }

        public void Mine(Building building, Bubble bubble)
        {
            Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            if (bubble.resources.food < 1)
            {
                this.BreakBuilding(building);

                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );
            }

            MiningMode miningMode = data.GetMiningMode();

            if (miningMode == MiningMode.Fuel)
            {
                bubble.resources.fuel += 2 * data.count;
            }
            else if (miningMode == MiningMode.Materials)
            {
                bubble.resources.materials += 2 * data.count;
            }

            bubble.resources.food -= 1;
            bubble.resources.pollution += 1;
        }

        public void RepairBuilding(Building building, Bubble bubble)
        {
            if (bubble.resources.food < 12 || bubble.resources.materials < 20)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            this.RepairBuilding(building);
            bubble.resources.food -= 12;
            bubble.resources.materials -= 20;
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

            return brokenText + $"Шахта<режим={modeText} працівники={data.count}/{data.capacity}>";
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
