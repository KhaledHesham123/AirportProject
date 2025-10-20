using FlyingProject.Project.core.DTOS.FlightsDto;
using FlyingProject.Project.core.DTOS.ViewModles;
using FlyingProject.Project.core.Entities.main;

namespace FlyingProject.CQRS.CQRSContracts
{
    public interface IUpdateFlightsCommandOrchestrator
    {
        Task<Flight> UpdateFlightsAsync(int? id, UpdateFlightviewmodle flightUpdateDto);

    }
}
