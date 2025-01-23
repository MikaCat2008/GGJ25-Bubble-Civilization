namespace BubbleApi
{
    public class System
    {
        public Storage storage 
        {
            get { return GlobalStorage.storage; }
        }
        public SystemsContainer systems
        {
            get { return GlobalStorage.systems; }
        }
    }
}
