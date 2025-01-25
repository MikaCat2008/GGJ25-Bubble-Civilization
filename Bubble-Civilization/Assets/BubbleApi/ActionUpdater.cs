using System;
using System.Collections.Generic;


namespace BubbleApi
{
    public class ActionUpdater
    {
        public Dictionary<ActionType, ResourcesContainer> actions;

        public ActionUpdater()
        {
            this.actions = new Dictionary<ActionType, ResourcesContainer>();
        }

        public void ProcessAction(Bubble bubble, ActionType action, Building building = null)
        {
            if (this.actions.TryGetValue(action, out ResourcesContainer actionResources))
            {
                int[] resources = actionResources.ToArray();
                int[] bubbleResources = bubble.resources.ToArray();
            
                for (int i = 0; i < bubbleResources.Length; i++)
                {
                    int resource = resources[i];

                    if (resource < 0 && (-resource) > bubbleResources[i])
                    {
                        if (building != null)
                            GlobalStorage.buildingUpdater.BreakBuilding(building, bubble);

                        throw new BubbleApiException(
                            BubbleApiExceptionType.NotEnoughResources
                        );
                    }

                    bubbleResources[i] += resource;
                }

                bubble.resources = new ResourcesContainer(bubbleResources);
            }
        }
    }
}
