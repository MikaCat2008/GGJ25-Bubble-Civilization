using System;

namespace BubbleApi
{
    public class Bubble
    {
        public static event Action OnPlayerEntered;

        public PopulationResource populationResource;
        public FoodResource foodResource;
        public OxygenResource oxygenResource;
        public MaterialsResource materialsResource;
        public FuelResource fuelResource;

        public Bubble()
        {
            populationResource = new PopulationResource();
            foodResource = new FoodResource();
            oxygenResource = new OxygenResource();
            materialsResource = new MaterialsResource();
            fuelResource = new FuelResource();
        }
        public void SetStartResources()
        {
            populationResource.Quantity = 50;
            foodResource.Quantity = 100;
            oxygenResource.Quantity = 1000;
            materialsResource.Quantity = 100;
            fuelResource.Quantity = 100;
        }

        public string ResourcesToDebugString()
        {
            return
                " Ресурси: " +
                populationResource.ToDebugString() + ", " +
                foodResource.ToDebugString() + ", " +
                oxygenResource.ToDebugString() + ", " +
                materialsResource.ToDebugString() + ", " +
                fuelResource.ToDebugString();
        }



        public ResourcesContainer resources;
        public BuildingsContainer buildings;

        public Bubble(ResourcesContainer resources, BuildingsContainer buildings)
        {
            this.resources = resources;
            this.buildings = buildings;
        }


    }
}