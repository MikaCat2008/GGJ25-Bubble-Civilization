using System;
using System.Linq;
using System.Collections.Generic;


namespace BubbleApi
{
    public class BuildingTimeout
    {
        public int id;
        public Bubble bubble;
        public BuildingType type;
        public int timeout;

        public BuildingTimeout(int id, Bubble bubble, BuildingType type, int timeout)
        {
            this.id = id;
            this.bubble = bubble;
            this.type = type;
            this.timeout = timeout;
        }
    }

    public delegate void BuildingEventHandler(Building building, Bubble bubble);

    public class BuildingUpdater
    {
        public SystemsContainer systems;
        public event BuildingEventHandler OnBuildingDone;
        public event BuildingEventHandler OnBreak;
        public List<BuildingTimeout> buildingQueue;

        public BuildingUpdater(SystemsContainer systems)
        {
            this.systems = systems;
            this.buildingQueue = new List<BuildingTimeout>();
        }

        private bool UpdateBreaking(Building building, Bubble bubble)
        {
            bool status = new Random().Next(0, 36000 / GlobalStorage.storage.timer.speed) == 0;

            if (status)
            {
                this.systems.building.BreakBuilding(building);
                this.OnBreak.Invoke(building, bubble);
            }

            return status;
        }

        private void UpdateBuilding(Building building, Bubble bubble)
        {
            BuildingType type = building.GetBuildingType();

            if (type == BuildingType.Empty)
                return;

            if (this.UpdateBreaking(building, bubble))
                return;

            BuildingSystem system = this.systems.GetBuildingSystem(type);
            system.Update(building, bubble);
        }

        public void StartBuilding(int id, Bubble bubble, BuildingType type, int timeout)
        {
            this.buildingQueue.Add(new BuildingTimeout(id, bubble, type, timeout));
        }

        public void Update(Bubble bubble)
        {
            foreach (Building building in bubble.buildings.container.Values)
            {
                try
                {
                   this.UpdateBuilding(building, bubble);
                }
                catch (BubbleApiException exception)
                {
                    if (exception.type == BubbleApiExceptionType.NotEnoughResources)
                    {
                        this.systems.building.BreakBuilding(building);
                    }
                }
            }

            HashSet<int> toDeleteSet = new HashSet<int>();

            for (int i = 0; i < this.buildingQueue.Count; i++)
            {
                BuildingTimeout timeout = buildingQueue[i];

                timeout.timeout -= GlobalStorage.storage.timer.speed;

                if (timeout.timeout <= 0)
                {
                    toDeleteSet.Add(i);

                    BuildingSystem buildingSystem = this.systems.GetBuildingSystem(timeout.type);
                    Building building = buildingSystem.Build(timeout.id, bubble);

                    this.OnBuildingDone.Invoke(building, bubble);
                }
            }

            if (toDeleteSet.Count != 0)
                this.buildingQueue = this.buildingQueue
                    .Where((v, i) => !toDeleteSet.Contains(i))
                    .ToList();

            if (bubble.resources.oxygen <= 0)
                throw new BubbleApiException(
                    BubbleApiExceptionType.LackOfOxygen
                );

            if (bubble.resources.pollution >= 1000)
                throw new BubbleApiException(
                    BubbleApiExceptionType.TooMuchPollution
                );

            if (bubble.resources.population <= 0)
                throw new BubbleApiException(
                    BubbleApiExceptionType.LackOfPopulation
                );
        }
    }
}
