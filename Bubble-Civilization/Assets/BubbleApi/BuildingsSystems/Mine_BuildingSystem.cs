namespace BubbleApi
{
    public class Mine_BuildingSystem : BuildingSystem
    {
        //public override void Update(Building building)
        //{
        //    Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);
        
            
        //}

        public override string BuildingToString(Building building)
        {
            this.CheckType(building, BuildingType.Mine);

            Mine_BuildingData data = (Mine_BuildingData)building.data;

            return $"Mine<type={data.type}>";
        }
    }
}
