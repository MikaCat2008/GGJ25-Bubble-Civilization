namespace BubbleApi
{
    public class Mine_BuildingSystem : BuildingSystem
    {
        //public override void Update(Building building)
        //{
        //    Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);


        //}

        public Building Build(int id, Bubble bubble)
        {
            if (bubble.resources.food < 25 || bubble.resources.materials < 40)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            Building mine = this.Build(id, bubble, BuildingType.Mine);

            mine.data = new Mine_BuildingData();
            bubble.resources.food -= 25;
            bubble.resources.materials -= 40;

            return mine;
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
                bubble.resources.fuel += 2;
            }
            else if (miningMode == MiningMode.Materials)
            {
                bubble.resources.materials += 2;
            }

            bubble.resources.food -= 1;
        }

        public void SetMiningMode(Building building, MiningMode miningMode)
        {
            Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);

            data.SetMiningMode(miningMode);
        }

        public override string BuildingToString(Building building)
        {
            Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);

            MiningMode miningMode = data.GetMiningMode();

            string modeText = this.MiningModeToString(miningMode);
            string brokenText = this.BrokenText(building);

            return brokenText + $"Шахта<режим={modeText}>";
        }

        private string MiningModeToString(MiningMode miningMode)
        {
            if (miningMode == MiningMode.Free)
                return "вільна";
            if (miningMode == MiningMode.Fuel)
                return "паливо";
            return "будівальні матеріали";
        }
    }
}
