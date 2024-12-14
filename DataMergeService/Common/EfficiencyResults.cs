namespace Gems.DataMergeServices.Common
{
    public class EfficiencyResults
    {
        public int CountOsmAddresses { get; set; }
        public int CountFiasAddresses { get; set; }
        public int CountAddressesWithGeometry { get; set; }
        public double AlgorithmEfficiency { get; set; }
        public double OverallEfficiency { get; set; }
        public double OsmToFiasCoverage { get; set; }

        public EfficiencyResults(int countOsmAddresses, int countFiasAddresses, int countAddressesWithGeometry, double algorithmEfficiency, double overallEfficiency, double osmToFiasCoverage)
        {
            CountOsmAddresses = countOsmAddresses;
            CountFiasAddresses = countFiasAddresses;
            CountAddressesWithGeometry = countAddressesWithGeometry;
            AlgorithmEfficiency = algorithmEfficiency;
            OverallEfficiency = overallEfficiency;
            OsmToFiasCoverage = osmToFiasCoverage;
        }
    }
}
