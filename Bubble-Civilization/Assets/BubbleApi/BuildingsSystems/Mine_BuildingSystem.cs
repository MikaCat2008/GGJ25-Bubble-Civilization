namespace BubbleApi
{
    public class Mine_BuildingSystem : BuildingSystem
    {
        public override void Update(Building building)
        {
            this.CheckType(building, BuildingType.Mine);
            
            Mine_BuildingData data = (Mine_BuildingData)building.data;
        }

        public override string BuildingToString(Building building)
        {
            this.CheckType(building, BuildingType.Mine);

            Mine_BuildingData data = (Mine_BuildingData)building.data;

            return $"Mine<type={data.type}>";
        }
    }
}
