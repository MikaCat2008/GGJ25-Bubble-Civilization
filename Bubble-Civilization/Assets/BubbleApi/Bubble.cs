namespace BubbleApi
{
    public class Bubble
    {
        public ResourcesContainer resources;
        public BuildingsContainer buildings;

        public Bubble(ResourcesContainer resources, BuildingsContainer buildings)
        {
            this.resources = resources;
            this.buildings = buildings;
        }
    }
}