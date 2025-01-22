namespace BubbleApi
{
    public class PowerStation_BuildingData : BuildingData
    {
        public int count;
        public int capacity;

        public PowerStation_BuildingData() : base()
        {
            this.count = 0;
            this.capacity = 0;
        }
    }
}
