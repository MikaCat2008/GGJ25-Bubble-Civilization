namespace BubbleApi
{
    public class House_BuildingSystem : BuildingSystem
    {
        public override void Update(Building building)
        {
            this.CheckType(building, BuildingType.House);
            
            House_BuildingData data = (House_BuildingData)building.data;

            data.count = 100;
            data.capacity = 100;
        }

        public override string BuildingToString(Building building)
        {
            this.CheckType(building, BuildingType.House);

            House_BuildingData data = (House_BuildingData)building.data;

            return $"House<count={data.count} capacity={data.capacity}>";
        }
    }
}
