using System;
using DatabaseAccess.Entities.Common;

namespace DatabaseAccess.Entities
{
    public class CityInfo : BaseEntity
    {
        public string Name { get; set; }

        public string Lat { get; set; }

        public string Lon { get; set; }
    }


    public class WeatherInfo : BaseEntity
    {
        public Guid CityId { get; set; }

        public string Temperature { get; set; }

        public DateTime DataTime { get; set; }
    }
}