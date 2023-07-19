using CaseProject.Model.Bus;

namespace CaseProject.Interface
{
    public interface IBusService
    {
        Task<GetBusLocationsResponse> GetBusLocations(GetBusLocationsRequest request);

        Task<GetBusJourneysResponse> GetBusJourneys(GetBusJourneysRequest request);
    }
}
