using System.Collections.Generic;


namespace BubbleApi
{
    public class Timer
    {
        public int ticks;
        public int speed;

        public Timer()
        {
            this.ticks = 0;
            this.speed = 0;
        }

        public void Tick()
        {
            this.ticks += this.speed;
        }
    }

    public class Storage
    {
        public Timer timer;
        public Dictionary<int, Bubble> bubbles;

        public Storage()
        {
            this.timer = new Timer();
            this.bubbles = new Dictionary<int, Bubble>();
        }
    }
}
