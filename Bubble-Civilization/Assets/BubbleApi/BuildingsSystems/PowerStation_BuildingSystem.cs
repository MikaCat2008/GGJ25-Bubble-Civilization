namespace BubbleApi
{
    public class PowerStation_BuildingSystem : BuildingSystem
    {
        public override void Update(Building building, Bubble bubble)
        {
            if (this.storage.timer.ticks % 300 == 0)
            {
                this.GenerateElectricity(building, bubble);
            }
        }

        public Building Build(int id, Bubble bubble)
        {
            if (bubble.resources.food < 20 || bubble.resources.materials < 50)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            Building powerStation = this.Build(id, bubble, BuildingType.PowerStation);

            powerStation.data = new PowerStation_BuildingData();
            bubble.resources.food -= 20;
            bubble.resources.materials -= 50;

            return powerStation;
        }

        public void SetCapacity(Building building, int capacity)
        {
            PowerStation_BuildingData data = this.GetData<PowerStation_BuildingData>(building, BuildingType.PowerStation);

            data.capacity = capacity;
        }

        public void Hire(Building building, Bubble bubble)
        {
            PowerStation_BuildingData data = this.GetData<PowerStation_BuildingData>(building, BuildingType.PowerStation);

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

        public void GenerateElectricity(Building building, Bubble bubble)
        {
            PowerStation_BuildingData data = this.GetData<PowerStation_BuildingData>(building, BuildingType.PowerStation);

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

            bubble.resources.fuel += 2 * data.count;
            bubble.resources.food -= 1;
            bubble.resources.pollution += 1;
        }

        public void RepairBuilding(Building building, Bubble bubble)
        {
            if (bubble.resources.food < 10 || bubble.resources.materials < 25)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            this.RepairBuilding(building);
            bubble.resources.food -= 10;
            bubble.resources.materials -= 25;
        }

        public override void Destroy(Building building, Bubble bubble)
        {
            PowerStation_BuildingData data = this.GetData<PowerStation_BuildingData>(building, BuildingType.PowerStation);

            bubble.resources.freePopulation += data.count;
            base.Destroy(building, bubble);
        }

        public override string BuildingToString(Building building)
        {
            PowerStation_BuildingData data = this.GetData<PowerStation_BuildingData>(building, BuildingType.PowerStation);

            string brokenText = this.BrokenText(building);

            return brokenText + $"Електростанція<працівники={data.count}/{data.capacity}>";
        }
    }
}
