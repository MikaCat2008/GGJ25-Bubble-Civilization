namespace BubbleApi
{
    public enum ResourceType
    {
        Food,
        Fuel,
        Energy,
        Oxygen,
        Materials,
        Pollution,
        Population
    }

    public struct ResourcesContainer
    {
        public int food;
        public int fuel;
        public int energy;
        public int oxygen;
        public int materials;
        public int pollution;
        public int population;
        public int freePopulation;

        public ResourcesContainer(int food, int fuel, int materials)
        {
            this.food = food;
            this.fuel = fuel;
            this.energy = 0;
            this.oxygen = 100;
            this.materials = materials;
            this.pollution = 0;
            this.population = 0;
            this.freePopulation = 0;
        }
    }
}