namespace BubbleApi
{
    public enum ResourceType
    {
        Food,
        Fuel,
        Energy,
        Oxygen,
        Happiness,
        Materials,
        Population
    }

    public struct ResourcesContainer
    {
        public int food;
        public int fuel;
        public int energy;
        public int oxygen;
        public int happiness;
        public int materials;
        public int population;

        public ResourcesContainer(int food, int fuel, int energy, int oxygen, int happiness, int materials, int population)
        {
            this.food = food;
            this.fuel = fuel;
            this.energy = energy;
            this.oxygen = oxygen;
            this.happiness = happiness;
            this.materials = materials;
            this.population = population;
        }
    }
}