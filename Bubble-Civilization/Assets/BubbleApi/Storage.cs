using System.Collections.Generic;


namespace BubbleApi
{
    public delegate void OnTimerHandler();
    public delegate void OnTimerErrorHandler(BubbleApiException exception);

    public class Interval
    {
        public int ticks;
        public OnTimerHandler handler;
        public OnTimerErrorHandler? error;

        public Interval(int ticks, OnTimerHandler handler, OnTimerErrorHandler error = null)
        {
            this.ticks = ticks;
            this.handler = handler;
            this.error = error;
        }
    }

    public class Timer
    {
        public int ticks;
        public int speed;

        private Dictionary<int, Interval> intervals;
        private int nextIntervalId;

        public Timer()
        {
            this.ticks = 0;
            this.speed = 0;

            this.intervals = new Dictionary<int, Interval>();
            this.nextIntervalId = 0;
        }

        public void Tick()
        {
            foreach (Interval interval in this.intervals.Values)
            {
                if (this.ticks % interval.ticks < this.speed)
                    try
                    {
                        interval.handler();
                    }
                    catch (BubbleApiException exception)
                    {
                        if (interval.error != null)
                            interval.error(exception);
                    }
            }
            this.ticks += this.speed;
        }

        public int CreateInterval(int ticks, OnTimerHandler handler)
        {
            int intervalId = this.nextIntervalId++;

            this.intervals.Add(intervalId, new Interval(ticks, handler));

            return intervalId;
        }

        public void DeleteInterval(int intervalId)
        {
            this.intervals.Remove(intervalId);
        }
    }

    public class Storage
    {
        public Timer timer;
        public Dictionary<int, Bubble> bubbles;
        public Bubble currentBubble;

        public Storage()
        {
            this.timer = new Timer();
            this.bubbles = new Dictionary<int, Bubble>();
            this.currentBubble = null;
        }
    }
}
