namespace BubbleApi
{
    public class BuildingData 
    {
        public int interval;
        public bool requireRepair;

        public BuildingData()
        {
            this.interval = -1;
            this.requireRepair = false;
        }
    }
}
