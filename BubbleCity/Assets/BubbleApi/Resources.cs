using UnityEngine;


namespace BubbleApi
{
    public struct ResourcesContainer
    {
        public int food;
        public int energy;
        public int oxygen;
        public int happiness;
        public int materials;
        public int pollution;
        public int population;

        public ResourcesContainer(int food, int energy, int oxygen, int happiness, int materials, int pollution, int population)
        {
            this.food = food;
            this.energy = energy;
            this.oxygen = oxygen;
            this.happiness = happiness;
            this.materials = materials;
            this.pollution = pollution;
            this.population = population;
        }
    }
}
