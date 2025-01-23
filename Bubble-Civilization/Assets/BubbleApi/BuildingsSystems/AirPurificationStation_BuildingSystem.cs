namespace BubbleApi
{
    public class AirPurificationStation_BuildingSystem : BuildingSystem
    {
        public override void Update(Building building, Bubble bubble)
        {
            if (this.storage.timer.ticks % 600 == 0)
            {
                this.Purify(building, bubble);
            }
        }

        public void StartBuilding(int id, Bubble bubble)
        {
            if (bubble.resources.food < 10 || bubble.resources.materials < 15)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            this.buildingUpdater.StartBuilding(id, bubble, BuildingType.AirPurificationStation, 600);

            bubble.resources.food -= 10;
            bubble.resources.materials -= 15;
            bubble.buildings.SetBuildingType(id, BuildingType.Building);
        }

        public override Building Build(int id, Bubble bubble)
        {
            Building airPurificationStation = this.Build(id, bubble, BuildingType.AirPurificationStation);

            airPurificationStation.data = new AirPurificationStation_BuildingData();

            return airPurificationStation;
        }

        public void SetCapacity(Building building, int capacity)
        {
            AirPurificationStation_BuildingData data = this.GetData<AirPurificationStation_BuildingData>(building, BuildingType.AirPurificationStation);

            data.capacity = capacity;
        }

        public void Hire(Building building, Bubble bubble)
        {
            AirPurificationStation_BuildingData data = this.GetData<AirPurificationStation_BuildingData>(building, BuildingType.AirPurificationStation);

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

        public void Purify(Building building, Bubble bubble)
        {
            AirPurificationStation_BuildingData data = this.GetData<AirPurificationStation_BuildingData>(building, BuildingType.AirPurificationStation);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            if (bubble.resources.energy < 1)
            {
                this.BreakBuilding(building);

                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );
            }

            bubble.resources.energy -= 1;
            bubble.resources.pollution -= 5 * data.count;
        }

        public void RepairBuilding(Building building, Bubble bubble)
        {
            if (bubble.resources.food < 5 || bubble.resources.materials < 7)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            this.RepairBuilding(building);
            bubble.resources.food -= 5;
            bubble.resources.materials -= 7;
        }

        public override void Destroy(Building building, Bubble bubble)
        {
            AirPurificationStation_BuildingData data = this.GetData<AirPurificationStation_BuildingData>(building, BuildingType.AirPurificationStation);

            bubble.resources.freePopulation += data.count;
            base.Destroy(building, bubble);
        }

        public override string BuildingToString(Building building)
        {
            AirPurificationStation_BuildingData data = this.GetData<AirPurificationStation_BuildingData>(building, BuildingType.AirPurificationStation);

            string brokenText = this.BrokenText(building);

            return brokenText + $"Очисна споруда<працівники={data.count}/{data.capacity}>";
        }
    }
}
