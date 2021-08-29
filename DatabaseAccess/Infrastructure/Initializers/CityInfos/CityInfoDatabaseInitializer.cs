using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using DatabaseAccess.Entities;
using Microsoft.Extensions.FileProviders;

namespace DatabaseAccess.Infrastructure.Initializers.CityInfos
{
    public class CityInfoDatabaseInitializer : IDatabaseInitializer
    {
        public async Task Execute(ApplicationContext context)
        {
            if (context.CityInfos.Any())
            {
                return;
            }

            var embeddedProvider = new EmbeddedFileProvider(typeof(IDatabaseInitializer).Assembly);
            await using var stream = embeddedProvider.GetFileInfo("Infrastructure.Initializers.CityInfos.BY.csv").CreateReadStream();
            using var csv = new CsvReader(new StreamReader(stream), new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = "|",
            });

            var records = csv.GetRecords<CityInfoCsvDataItem>();
            var entities = records.Select(x => new CityInfo
            {
                Id = Guid.NewGuid(),
                Lat = x.Lat,
                Lon = x.Lon,
                Name = x.Name,
            });

            await context.CityInfos.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }
    }
}