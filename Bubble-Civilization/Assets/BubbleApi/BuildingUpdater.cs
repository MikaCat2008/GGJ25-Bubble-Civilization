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
        public event BuildingEventHandler OnRepaired;
        public event BuildingEventHandler OnDestroyed;
        public Dictionary<BuildingType, BuildingInfo> buildingsInfo;
        public List<BuildingTimeout> buildingQueue;

        public BuildingUpdater(SystemsContainer systems)
        {
            this.systems = systems;
            this.buildingsInfo = new Dictionary<BuildingType, BuildingInfo>();
            this.buildingQueue = new List<BuildingTimeout>();
        }

        private bool UpdateBreaking(Building building, Bubble bubble)
        {
            if (building.data.requireRepair)
                return false;

            bool status = new Random().Next(0, 108000 / GlobalStorage.storage.timer.speed) == 0;

            if (status)
                this.BreakBuilding(building, bubble);

            return status;
        }

        private void UpdateBuilding(Building building, Bubble bubble)
        {
            BuildingType type = building.GetBuildingType();

            if (type == BuildingType.Empty || type == BuildingType.Building)
                return;

            if (this.UpdateBreaking(building, bubble))
                return;
        }

        public void BreakBuilding(Building building, Bubble bubble)
        {
            this.systems.building.BreakBuilding(building);
            this.OnBreak?.Invoke(building, bubble);
        }

        public void RepairBuilding(Building building, Bubble bubble)
        {
            this.OnRepaired?.Invoke(building, bubble);
        }

        public void DestroyBuilding(Building building, Bubble bubble)
        {
            this.OnDestroyed?.Invoke(building, bubble);
        }

        public void StartBuilding(int id, Bubble bubble, BuildingType type)
        {
            this.buildingQueue.Add(
                new BuildingTimeout(id, bubble, type, this.buildingsInfo[type].buildingTime)
            );
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

                    this.OnBuildingDone?.Invoke(building, bubble);
                }
            }

            if (toDeleteSet.Count != 0)
                this.buildingQueue = this.buildingQueue
                    .Where((v, i) => !toDeleteSet.Contains(i))
                    .ToList();

            if (bubble.resources.food <= 0)
                throw new BubbleApiException(
                    BubbleApiExceptionType.LackOfFood
                );

            if (bubble.resources.oxygen > 100)
                bubble.resources.oxygen = 100;

            if (bubble.resources.oxygen <= 0)
                throw new BubbleApiException(
                    BubbleApiExceptionType.LackOfOxygen
                );

            if (bubble.resources.pollution < 0)
                bubble.resources.pollution = 0;

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
