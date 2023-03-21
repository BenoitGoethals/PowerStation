using PowerStationInfrastructure.Services.AD;

namespace PowerStationInfrastructure.Services.External;

public interface IConfigService
{
    bool Save(AdConfiguration adConfiguration);
    AdConfiguration? Load();
}