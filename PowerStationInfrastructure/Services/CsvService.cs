using CsvHelper;
using Microsoft.Extensions.Logging;
using PowerStationInfrastructure.helpers;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace PowerStationInfrastructure.Services;

public class CsvService
{
    private readonly ILogger<CsvService> _logger;

    public CsvService(ILogger<CsvService> logger)
    {
        _logger = logger;
    }

    public List<DtoUser>? LoadCsv(string filename)
    {
        try
        {
            using var stream = ResourceHelper.GetEmbeddedResourceStream(filename);
            {
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var records = csv.GetRecords<DtoUser>();
                return records.ToList();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return default;
    }
}