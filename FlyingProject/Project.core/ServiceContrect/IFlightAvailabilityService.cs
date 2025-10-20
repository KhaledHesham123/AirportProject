using FlyingProject.Project.core.DTOS.FlightsDto;
using FlyingProject.Project.core.Entities.main;
using FlyingProject.Shared;

namespace FlyingProject.Project.core.ServiceContrect
{
    public interface IFlightAvailabilityService
    {
        Task<List<AvailableFlightDto>> GetAvailableFlightsAsync(List<Flight> flights);

    }
}
