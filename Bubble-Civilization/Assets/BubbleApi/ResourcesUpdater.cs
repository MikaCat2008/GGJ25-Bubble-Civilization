namespace BubbleApi
{
    public delegate void ResourceChangeHandler(int value);

    public class ResourcesUpdater
    {
        public event ResourceChangeHandler OnFoodChanged;
        public event ResourceChangeHandler OnFuelChanged;
        public event ResourceChangeHandler OnEnergyChanged;
        public event ResourceChangeHandler OnOxygenChanged;
        public event ResourceChangeHandler OnMaterialsChanged;
        public event ResourceChangeHandler OnPollutionChanged;
        public event ResourceChangeHandler OnPopulationChanged;
        public event ResourceChangeHandler OnFreePopulationChanged;

        private int[] lastResources;

        public ResourcesUpdater()
        {
            this.lastResources = new ResourcesContainer().ToArray();
        }

        public void Update()
        {
            int[] currentResources = GlobalStorage.storage.currentBubble.resources.ToArray();

            for (int i = 0; i < currentResources.Length; i++)
            {
                if (currentResources[i] != this.lastResources[i])
                    this.Change(i, currentResources[i]);
            }

            this.lastResources = currentResources;
        }

        private void Change(int i, int value)
        {
            if (i == 0)
                this.OnFoodChanged?.Invoke(value);
            else if (i == 1)
                this.OnFuelChanged?.Invoke(value);
            else if (i == 2)
                this.OnEnergyChanged?.Invoke(value);
            else if (i == 3)
                this.OnOxygenChanged?.Invoke(value);
            else if (i == 4)
                this.OnMaterialsChanged?.Invoke(value);
            else if (i == 5)
                this.OnPollutionChanged?.Invoke(value);
            else if (i == 6)
                this.OnPopulationChanged?.Invoke(value);
            else if (i == 7)
                this.OnFreePopulationChanged?.Invoke(value);
        }
    }
}
