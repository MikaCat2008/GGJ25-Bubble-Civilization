namespace BubbleApi
{
    public class BuildingInfo
    {
        public int buildingTime;
        public int updateInterval;

        public BuildingInfo(int updateInterval, int buildingTime)
        {
            this.updateInterval = updateInterval;
            this.buildingTime = buildingTime;
        }
    }
}
