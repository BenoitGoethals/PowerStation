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
    public class ConfigService
    {
        private readonly string? _fileName;
        private readonly ILogger<ConfigService> _logger;
        private readonly AdConfigurationValidator _adConfigurationValidator = new();

        public ConfigService(string? fileName, ILogger<ConfigService> logger)
        {
            _fileName = fileName;
            _logger = logger;
        }


        public bool Save(AdConfiguration adConfiguration)
        {

            var results = _adConfigurationValidator.Validate(adConfiguration);

            if (results.IsValid)
            {
                try
                {
                    XmlSerializer x = new(typeof(AdConfiguration));
                    if (_fileName != null)
                    {
                        TextWriter writer = new StreamWriter(_fileName);
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
                if (_fileName != null)
                {
                    using Stream reader = new FileStream(_fileName, FileMode.Open);
                    // Call the Deserialize method to restore the object's state.
                    AdConfiguration? adConfiguration = (AdConfiguration?)serializer.Deserialize(reader);
                    return adConfiguration;
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
