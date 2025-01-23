using System.Collections.Generic;


namespace BubbleApi
{
    public class Timer
    {
        public int ticks;

        public Timer()
        {
            this.ticks = 0;
        }

        public void Tick()
        {
            this.ticks += 1;
        }
    }

    public class Storage
    {
        public Timer timer;
        public List<Bubble> bubbles;

        public Storage()
        {
            this.timer = new Timer();
            this.bubbles = new List<Bubble>();
        }
    }
}
