using CsvHelper.Configuration.Attributes;

namespace DatabaseAccess.Infrastructure.Initializers.CityInfos
{
    public class CityInfoCsvDataItem
    {
        [Index(1)]
        public string Name { get; set; }

        [Index(4)]
        public string Lat { get; set; }

        [Index(5)]
        public string Lon { get; set; }
    }
}