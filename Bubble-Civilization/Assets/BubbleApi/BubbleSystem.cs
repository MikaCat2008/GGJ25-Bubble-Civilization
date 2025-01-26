﻿namespace BubbleApi
{
    public class BubbleSystem : System
    {
        public Bubble CreateBubble(byte id)
        {
            ResourcesContainer resources = new ResourcesContainer(
                500, 500, 500
            );
            BuildingsContainer buildings = new BuildingsContainer();

            Bubble bubble = new Bubble(resources, buildings);

            this.storage.bubbles.Add(id, bubble);

            House_BuildingSystem houseSystem = this.systems.house;

            //This house isnt present in actual game
            Building house = houseSystem.Build(0, bubble);

            houseSystem.SetCapacity(house, 10);
            houseSystem.Settle(house, bubble);
            houseSystem.Settle(house, bubble);
            houseSystem.Settle(house, bubble);
            houseSystem.Settle(house, bubble);
            houseSystem.Settle(house, bubble);

            this.storage.timer.CreateInterval(5 * 60, () => this.RemoveOxygen(bubble));

            return bubble;
        }

        private void RemoveOxygen(Bubble bubble)
        {
            bubble.resources.oxygen -= bubble.resources.population;
        }

        public Bubble CreateBaseBubble()
        {
            ResourcesContainer resources = new ResourcesContainer(
                500, 500, 500
            );
            BuildingsContainer buildings = new BuildingsContainer();

            Bubble bubble = new Bubble(resources, buildings);

            return bubble;
        }
    }
}
