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
            : this(food, fuel, 0, 100, materials, 0, 0, 0) { }

        public ResourcesContainer(int[] resources)
            : this(
                  resources[0],
                  resources[1],
                  resources[2],
                  resources[3],
                  resources[4],
                  resources[5],
                  resources[6],
                  resources[7]
              ) { }

        public ResourcesContainer(
            int food = 0, 
            int fuel = 0, 
            int energy = 0, 
            int oxygen = 0, 
            int materials = 0, 
            int pollution = 0, 
            int population = 0, 
            int freePopulation = 0
        )
        {
            this.food = food;
            this.fuel = fuel;
            this.energy = energy;
            this.oxygen = oxygen;
            this.materials = materials;
            this.pollution = pollution;
            this.population = population;
            this.freePopulation = freePopulation;
        }

        public int[] ToArray()
        {
            return new int[]
            {
                this.food,
                this.fuel,
                this.energy,
                this.oxygen,
                this.materials,
                this.pollution,
                this.population,
                this.freePopulation
            };
        }
    }
}