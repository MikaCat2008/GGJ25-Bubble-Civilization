namespace BubbleApi
{
    public enum MiningMode
    {
        Free,
        Fuel,
        Materials
    }

    public class Mine_BuildingData : BuildingData
    {
        private byte miningMode;

        public Mine_BuildingData() : base()
        {
            this.miningMode = (byte)MiningMode.Free;
        }

        public void SetMiningMode(MiningMode miningMode)
        {
            this.miningMode = (byte)miningMode;
        }

        public MiningMode GetMiningMode()
        {
            return (MiningMode)this.miningMode;
        }
    }
}
