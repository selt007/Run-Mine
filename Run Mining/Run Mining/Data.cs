
namespace Run_Mining
{
    class Data
    {
        public int time { get; set; }
        public int lastSeen { get; set; }
        public int reportedHashrate { get; set; }
        public double currentHashrate { get; set; }
        public int validShares { get; set; }
        public int invalidShares { get; set; }
        public int staleShares { get; set; }
        public double averageHashrate { get; set; }
    }
}
