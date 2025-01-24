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

        private float FoodForPersonPerDay = 0.01f;
        private float OxygenForPersonPerDay = 0.1f;

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


        public void BreathPerDay()
        {
            float breathAmount = OxygenForPersonPerDay * populationResource.Quantity;
            int changeAmount = (int)MathF.Floor(breathAmount);

            if (oxygenResource.Quantity > changeAmount + 1.0f)
            {
                oxygenResource.DecreaseQuantity(changeAmount);
            }
            else
            {
                populationResource.Suffocate();
            }
        }

        public void EatPerDay()
        {
            float foodAmount = FoodForPersonPerDay * populationResource.Quantity;
            int changeAmount = (int)MathF.Floor(foodAmount);

            if (foodResource.Quantity > changeAmount + 1.0f)
            {
                foodResource.DecreaseQuantity(changeAmount);
            }
            else
            {
                populationResource.Starve();
            }
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