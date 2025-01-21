namespace BubbleApi
{
    public enum MineType
    {
        Free,
        Fuel,
        Materials
    }

    public class Mine_BuildingData : BuildingData
    {
        public MineType type;

        public Mine_BuildingData()
        {
            this.type = MineType.Free;
        }
    }
}
