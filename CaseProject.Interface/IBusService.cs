using CaseProject.Model.Bus;

namespace CaseProject.Interface
{
    public interface IBusService
    {
        GetBusLocationsResponse GetBusLocations(GetBusLocationsRequest request);

        GetBusJourneysResponse GetBusJourneys(GetBusJourneysRequest request);
    }
}
