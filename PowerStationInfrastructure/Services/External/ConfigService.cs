using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PowerStationInfrastructure.Services.AD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PowerStationInfrastructure.Services.External
{
    public class ConfigService : IConfigService
    {
        public string? FileName { get; set; }
        private readonly ILogger<ConfigService> _logger;
        private readonly AdConfigurationValidator _adConfigurationValidator = new();
        private AdConfiguration? _adConfiguration;
        public ConfigService(ILogger<ConfigService> logger,AdConfiguration? adConfiguration)
        {
            FileName = @"c:\power\config.xml";
            _logger = logger;
            _adConfiguration = adConfiguration;
        }


        public bool Save(AdConfiguration adConfiguration)
        {

            var results = _adConfigurationValidator.Validate(adConfiguration);

            if (results.IsValid)
            {
                try
                {
                    XmlSerializer x = new(typeof(AdConfiguration));
                    if (FileName != null)
                    {
                        TextWriter writer = new StreamWriter(FileName);
                        
                        x.Serialize(writer, adConfiguration);
                        writer.Close();
                    }
                }
                catch (IOException e)
                {
                    _logger.LogError(e.Message);
                    return false;

                }
            }
            else
            {
                foreach (var failure in results.Errors)
                {
                    _logger.LogError(failure.ErrorMessage);
                }
                return false;
            }

            return true;

        }

        public AdConfiguration? Load()
        {
            XmlSerializer serializer = new(typeof(AdConfiguration));
            try
            {
                if (FileName != null)
                {
                    if (File.Exists(FileName))
                    {
                        using Stream reader = new FileStream(FileName, FileMode.Open);
                        // Call the Deserialize method to restore the object's state.
                        _adConfiguration = (AdConfiguration?)serializer.Deserialize(reader);
                        return _adConfiguration;
                    }
                    else
                    {
                        Save(AdConfiguration.Default);
                    }
                   
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

            }


            return AdConfiguration.Default;

        }
    }
}
